var localTimer;
var startLocalTimer;
layui.config({
    base: "/js/"
}).use(['table'], async function () {
    const laytpl = layui.laytpl;
    const util = layui.util;
    const form = layui.form;
    const carousel = layui.carousel;

    let plan = null;
    let curQuestion = null;

    let startTime;
    let timer;

    function startTimer() {
        startTime = new Date();
        timer = setInterval(function () {
            let curTime = new Date();
            if (curTime.getSeconds() == 0) {
                $$.post("/TestPlan/Answer/" + PageInfo.KeyValueStr, {},true).then(function (json) {
                    plan.BusTestPlanUsers[0].Duration = json.data;
                    startTime = new Date();
                });
            }
            var total = plan.Template.Duration * 60 - plan.BusTestPlanUsers[0].Duration;
            total = (total - (curTime - startTime) / 1000) * 1000;
            if (total <= 0) {
                ///超时
                clearInterval(timer);
                $$.post('/TestPlan/ExpireAnswer/' + PageInfo.KeyValueStr, {}).then(function () {
                    layer.success("时间到", { shade: 0.5 }, function () {
                        $$.callback("testover", {});
                        $$.closeThis();
                    });
                });

                return;
            }
            var newDate = new Date(total);
            newDate.setHours(newDate.getHours()-8);
            $("#durationdiv").removeClass("flash").hval(newDate.format("HH:mm:ss"));
            //setTimeout(function () {
            //    $("#durationdiv").addClass("flash")
            //}, 0);
        }, 1000);
    }

    startLocalTimer = function (dur) {
        if (localTimer) {
            clearInterval(localTimer);
        }
        let total = dur;
        localTimer = setInterval(function () {
            var ldur = --total;
            if (ldur < 0) {
                clearInterval(localTimer);
                localTimer = null;
                confirm(null,true);
                return;
            }
            $("#localdurationdiv").hval(ldur+"秒");
        }, 1000);
    }
    async function init() {
        var json = await $$.get(BaseUrl + "/GetTest/" + PageInfo.KeyValueStr);
        const html = laytpl($("#noteTemplate").html()).render(json.data);
        $("#page").html(html);
        plan = json.data;
        window.plan = plan;
        carousel.render({
            elem: '.layui-carousel'
        });
        
    }
    function toQuestion(eid, qid) {
        let index = plan.Questions.findIndex(function (a) { return a.ExampleId == eid && a.QuestionId == qid });
        toIndex(index);
    }
    function nextQuestion(eid, qid) {
        let q = plan.Questions.find(function (a) { return a.ExampleId == eid && a.QuestionId == qid });
        let qs = plan.Questions.filter(function (a) { return a.ExampleId == eid && a.SortNo == q.SortNo });
        let index = plan.Questions.findIndex(function (a) { return a.ExampleId == eid && a.SortNo == q.SortNo && (a.SortNo2 == 1 || a.SortNo2 == null) }) + qs.length;
        toIndex(index);
    }
    function toIndex(index) {
        if (localTimer) {
            clearInterval(localTimer);
            localTimer = null;
        }
        if (index >= plan.Questions.length) {
            //完成
            $$.post('/TestPlan/EndAnswer/' + PageInfo.KeyValueStr, {}).then(function () {
                clearInterval(timer);
                $$.callback("testover", {});
                let html = $("#overTemplate").html();
                html = laytpl(html).render(plan);
                $("#page").html(html);
            });
            
            return;
        }
        let question = plan.Questions[index];
        let questions = plan.Questions.filter(function (a) {
            return a.ExampleId == question.ExampleId && a.SortNo == question.SortNo;
        }).sort(function (a, b) { return a.SortNo2 - b.SortNo2; });
        if (questions.length > 1) {
            question.isMultiple = true;
            question.Index = (index + questions.length);
            question.Count = plan.Questions.length;
            let uhtml = $("#multipleQuestionTemplate").html();
            uhtml = laytpl(uhtml).render(questions);
            $("#page").html(uhtml);
            form.render();
            curQuestion = question;
            return;
        }
        question.isMultiple = false;
        question.Index = (index+1);
        question.Count = plan.Questions.length;
        let html = $("#singleQuestionTemplate").html();
        html = laytpl(html).render(question);
        $("#page").html(html);
        form.render();
        curQuestion = question;
    }
    function start() {
        plan.Template.BusTestTemplateExamples = plan.Template.BusTestTemplateExamples.sort(function (a, b) { return a.SortNo - b.SortNo; });
        plan.Template.BusTestTemplateExamples.forEach(function (data) {
            data.Example.BusExampleQuestions = data.Example.BusExampleQuestions.sort(function (a, b) { return (a.SortNo - b.SortNo) * 1000 + (a.SortNo2 - b.SortNo2); });
            data.Example.BusExampleQuestions.forEach(function (data2) {
                data2.Question.BusQuestionOptions = data2.Question.BusQuestionOptions.sort(function (a, b) { return a.SortNo - b.SortNo; });
            });
        });
        plan.Questions = plan.Template.BusTestTemplateExamples.reduce(function (total, curr) {
            return total.concat(curr.Example.BusExampleQuestions);
        }, []);
        window.plan = plan;
        const me = plan.BusTestPlanUsers[0];
        let currentExample = me.CurrentExample;
        let next = true;
        if (!currentExample) {
            currentExample = plan.Questions[0].ExampleId;
            next = false;
        }
        let currentQuestion = me.CurrentQuestion;
        if (!currentQuestion) {
            currentQuestion = plan.Questions.find(function (a) { return a.ExampleId == currentExample; }).QuestionId;
            next = false;
        }
        

        $$.post("/TestPlan/StartAnswer/" + PageInfo.KeyValueStr, {}).fail(function (json) {
            //开始失败
            layer.error("开始失败：" + json.msg, {}, function () {
                $$.callback("testover", {});
                $$.closeThis();
            });

        }).then(function (json) {
            plan.BusTestPlanUsers[0].Duration = json.data;
            startTime = new Date();
            if (next) {
                nextQuestion(currentExample, currentQuestion);
            } else {
                toQuestion(currentExample, currentQuestion);
            }
            startTimer();
            if (plan.CanPause) {
                $("#pausediv").show();
            }
        });
    }

    function image() {
        var src = $(this).attr("src");
        if (!src) return;

        layer.image("预览", src);
    }
    function getOptions(igore) {
        let formData = $$.getFormData("form");
        let options = $("input[type=checkbox]:checked").map(function (i, a) {
                return { ExampleId: formData.ExampleId, QuestionId:$(a).attr("name"),OptionId:$(a).val() };
        }).toArray();
        let all = $("input[type=checkbox]").toArray().reduce(function (source, cur) {
            if (source.indexOf($(cur).attr("name")) >= 0) {
                return source;
            }
            source.push($(cur).attr("name"));
            return source;
        }, []);
        let my = options.reduce(function (source, cur) {
            if (source.indexOf(cur.QuestionId) >= 0) {
                return source;
            }
            source.push(cur.QuestionId);
            return source;
        }, []);
        if (igore) {
            let reslist = all.reduce(function (r, cur) {
                var larr = options.filter(function (a, i) {
                    return a.QuestionId == cur;
                });
                if (larr.length <= 0) {
                    larr = [{ ExampleId: formData.ExampleId, QuestionId: cur, OptionId:0 }];
                }
                return r.concat(larr);
            }, []);
            return { list: reslist };
        }
        if (my.length != all.length) {
            return "请完整答题";
        }
        return { list: options };
    }
    window.getOptions = getOptions;

    async function answer(igore) {
        var options = getOptions(igore);
        if (typeof options == "string") {
            layer.error(options);
            return new Promise(function (succ, err) {
                succ(false);
            });
        }
        var json = await $$.post("/TestPlan/Answer/" + PageInfo.KeyValueStr, options);
        plan.BusTestPlanUsers[0].Duration = json.data;
        startTime = new Date();
        return true;
    }
    async function option(e) {
        if (!$(e.elem).is("[multiple]")) {
            $("input[name=" + $(e.elem).attr("name") + "]").prop("checked", false);
            $(e.elem).prop("checked", true);
            form.render();
        }

        if (curQuestion.Question.Type == 2 || curQuestion.isMultiple) {
            return;
        }
        if (await answer()) {
            toIndex(curQuestion.Index);
        }
        
    }
    async function confirm(ele,igore) {
        if (await answer(igore)) {
            toIndex(curQuestion.Index);
        }
    }

    async function pause(e) {
        if (e.text().indexOf("暂停") >= 0) {
            var json = await $$.post("/TestPlan/PauseAnswer/" + PageInfo.KeyValueStr, {});
            clearInterval(timer);
            layer.success("暂停成功", {}, function () {
                $$.callback("testover", {});

            });
            e.html('<i class="layui-icon layui-icon-play"></i> 继续答题');
        } else {
            var json = await $$.post("/TestPlan/StartAnswer/" + PageInfo.KeyValueStr, {});
            startTimer();
            e.html('<i class="layui-icon layui-icon-pause"></i> 暂停答题');
        }
        
    }
    function close() {
        
        $$.closeThis();
    }
    util.on("layon", {
        start: start,
        image: image,
        confirm: confirm,
        pause: pause,
        close:close
    });
    form.on("checkbox", function (e) {
        option.call(this, e);
    })
    //$(document).on("click", function () {
    //    if (window.fullScreen) return;

    //    window.fullScreen = true;

    //    $$.fullScreen();
    //});
    top.$(".layui-layout-admin").eq(0).addClass("showMenu");
    //if (top.$(window.frameElement).parent()[0].tagName != "BODY") {
    //    top.$(window.frameElement).appendTo("body").css({ position: "fixed", top: 0, left: 0, zIndex: 999999 }).animate({width:"100%",height:"100%"});
    //    return;
    //}
    
    init();
});
