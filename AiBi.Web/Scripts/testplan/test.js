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

    function startTimer() {
        startTime = new Date();
        setInterval(function () {
            let curTime = new Date();
            var total = plan.Template.Duration * 60 - plan.BusTestPlanUsers[0].Duration;
            total = (total - (curTime - startTime) / 1000) * 1000;
            var newDate = new Date(total);
            newDate.setHours(newDate.getHours()-8);
            $(".duration").removeClass("flash").hval(newDate.format("HH:mm:ss"));
            setTimeout(function () {
                $(".duration").addClass("flash")
            }, 0);
        }, 1000);
    }
    function init() {
        $$.get(BaseUrl + "/GetTest/" + PageInfo.KeyValueStr).then(function (json) {
            const html = laytpl($("#noteTemplate").html()).render(json.data);
            $("#page").html(html);
            plan = json.data;
            carousel.render({
                elem: '.layui-carousel'
            });
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
        if (index >= plan.Questions.length) {

            $$.post('/TestPlan/EndAnswer/' + PageInfo.KeyValueStr, {}).then(function () {
                layer.success("完成测试", {shade:0.5}, function () {
                    $$.closeThis();
                });
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
        if (next) {
            nextQuestion(currentExample, currentQuestion);
        } else {
            toQuestion(currentExample, currentQuestion);
        }
        startTimer();

        $$.post("/TestPlan/StartAnswer/" + PageInfo.KeyValueStr, {}).fail(function (json) {
            layer.error("开始失败："+json.msg, {}, function () {
                $$.closeThis();
            });
            
        });
    }

    function image() {
        var src = $(this).attr("src");
        if (!src) return;

        layer.image("预览", src);
    }
    function getOptions() {
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
        if (my.length != all.length) {
            return "请完整答题";
        }
        return { list: options };
    }
    window.getOptions = getOptions;

    async function answer() {
        var options = getOptions();
        if (typeof options == "string") {
            layer.error(options);
            return new Promise(function (succ, err) {
                succ(false);
            });
        }
        var json = await $$.post("/TestPlan/Answer/" + PageInfo.KeyValueStr, options);
        if (json.code > 0) {
            plan.BusTestPlanUsers[0].Duration = json.data;
            return true;
        }
        return false;
    }
    async function option(e) {
        if (curQuestion.Question.Type == 2 || curQuestion.isMultiple) {
            return;
        }
        if (await answer()) {
            toIndex(curQuestion.Index);
        }
        
    }
    async function confirm() {
        if (await answer()) {
            toIndex(curQuestion.Index);
        }
    }

    util.on("layon", {
        start: start,
        image: image,
        confirm: confirm,
    });
    form.on("checkbox", function (e) {
        option.call(this, e);
    })
    init();
});
