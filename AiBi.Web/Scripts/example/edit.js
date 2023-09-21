layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;

    var upload = layui.upload;

    
    function initValidate() {
        $("#baseInfo").validate({
            submitHandler: function (form) {
                setChildOptionNum();
                var allvalid = true;
                $("form").not("#baseInfo").each(function (i, a) {
                    allvalid = allvalid && $(a).valid();
                });

                if (!allvalid) return false;

                var postData = $$.getFormData("#baseInfo");
                $.extend(postData, getChildData());
                var callbackstr;
                var addoreditFunc = PageInfo.KeyValueStr ? (callbackstr = "exampleeditok", $$.common.edit.req) : (callbackstr = "exampleaddok", $$.common.add.req);
                addoreditFunc(postData).then(function (json) {
                    
                    $$.callback(callbackstr, json);
                    $$.closeThis();
                    
                });
                return false;
            },
            rules: {
                Title: {
                    required: true
                },
                ClassifyId: {
                    required: true
                },
                Duration: {
                    required: true
                },
                Note: {
                    required: true
                },
                
                
            },
            messages: {
                Title: {
                    required: "请输入标题"
                },
                ClassifyId: {
                    required: "请选择分类"
                },
                Duration: {
                    required: "请输入时长"
                },
                Note: {
                    required: "请输入注意事项"
                },
            }
        });
    }
    function initValidateItems(form) {
        $(form).each(function (i, a) {
            $(a).validate({
                submitHandler: function (form) {
                    return false;
                },
                rules: {
                    Title: {
                        required: true
                    },
                    Code: {
                        required: true
                    },
                    Score: {
                        min: 0,
                        step:1,
                    },
                    OptionNum: {
                        min:2,
                    }

                },
                messages: {
                    Title: {
                        required: "请输入题面"
                    },
                    Code: {
                        required: "请输入选项代码"
                    },
                    Score: {
                        min: "必须大于等于0",
                        step: "必须是整数",
                    },
                    OptionNum: {
                        min: "选项个数不能少于两个",
                    }

                },
                errorPlacement: function (error, element) {
                    if (element.parent().hasClass("layui-input-wrap")) {
                        error.insertAfter(element.parent());
                        return;
                    }
                    error.appendTo(element.parent());
                },
            });
        });
    }
    function setChildOptionNum() {
        $("#questionInfo [question]").each(function (i, a) {
            let forms = $(a).find("form");
            let questionForm = forms.eq(0);
            let optionForms = forms.not(":first");
            questionForm.find("[name=OptionNum]").val(optionForms.length);
            optionForms.each(function (i, a) {
                $(a).find("[name=SortNo]").val((i+1));
            })
        });
    }
    function getChildData() {
        var res = {
            BusExampleQuestions: [],
            BusExampleOptions:[]
        };
        $("#questionInfo [question]").each(function (i, a) {
            let forms = $(a).find("form");
            let questionForm = forms.eq(0);
            let optionForms = forms.not(":first");
            let exampleQuestion = $$.getFormData(questionForm);
            exampleQuestion.Question = $$.getFormData(questionForm);
            exampleQuestion.Question.BusQuestionOptions = optionForms.map(function (i, a) {
                let option = $$.getFormData(a);
                return option;
            }).toArray();
            let exampleOptions = optionForms.map(function (i, a) {
                let option = $$.getFormData(a);
                option.Option = $$.getFormData(a);
                option.Option.Question = $$.getFormData(questionForm);
                option.Option.Question.BusExampleQuestions = [$$.getFormData(questionForm)];
                return option;
            }).toArray();
            res.BusExampleOptions = res.BusExampleOptions.concat(exampleOptions);
            res.BusExampleQuestions.push(exampleQuestion);
        });
        console.log(res);
        return res;
    }
    function getClassifies() {
        return $$.get("/Classify" + $$.common.getPageList.url,{ where: { ParentId: null }, page: 1, size: 1000 }).then(function (json) {
            var template = '<option value="{{Id}}">{{Name}}</option>';
            json.data.forEach(function (a) {
                $("select[name=ClassifyId]").append(template.combineObject(a));
            });
            form.render();
        });
    }
    function getSubClassifies(parentid,thisid) {
        if (!parentid) {
            $("select[name=SubClassifyId] option").not(":first").remove();
            form.render();
            $("select[name=SubClassifyId]").val("");
            return true;
        }
        return $$.get("/Classify" + $$.common.getPageList.url, { where: { ParentId: parentid }, page: 1, size: 1000 }).then(function (json) {
            var template = '<option value="{{Id}}">{{Name}}</option>';
            $("select[name=SubClassifyId] option").not(":first").remove();
            json.data.forEach(function (a) {
                $("select[name=SubClassifyId]").append(template.combineObject(a));
            });
            $("select[name=SubClassifyId]").val(thisid);
            form.render();
            return json;
        });
    }
    function renderDetail() {
        return $$.common.getDetail.req().then(function (json) {
            $$.setFormData("#baseInfo", json.data);
            
            if (json.data.Image) {
                $("#baseInfo [name=ImageFullName]").attr("src", json.data.Image.FullName);
                $("#baseInfo input[name=ImageId]").val(json.data.Image.Id);
            }
            json.data.BusExampleQuestions.forEach(function (a, i) {
                renderQuestion(a,json);
            });
            form.render();
            return json;
        });
    }

    function lastBtnType(type) {
        var btns = $("[addquestion=" + type + "]");
        return btns[btns.length - 1];
    }
    function renderQuestion(data, json) {
        var type = data.Question.Type;
        var elem = addQuestion.call(lastBtnType(type));
        $$.setFormData(elem, data);
        $$.setFormData(elem, data.Question);
        elem.find("[name=ImageFullName]").attr("src", (data.Question.Image || {}).FullName);
        data.Question.BusQuestionOptions.forEach(function (a, i) {
            renderOption(a, json, elem.find(".optionbtn"));
        });
    }
    function renderOption(data, json, btn) {
        var elem = addoption.call(btn);
        $$.setFormData(elem, data);
        var score = json.data.BusExampleOptions.find(function (a) { return a.OptionId == data.Id });
        $$.setFormData(elem, score);
    }

    function sortQuestion() {
        let children = $("#questionInfo").children("[question]");
        children.each(function (i, a) {
            $(a).find(".questionnumber").html((i + 1) + "");
            $(a).find("[name=SortNo]").val((i + 1) + "");
        });
        form.render();
    }
    
    function initUpload(elem,module=200) {
        upload.render({
            elem: elem,
            url: '/Attachment/Add', // 此处配置你自己的上传接口即可
            data: {
                module: module
            },
            size: 4096, // 限制文件大小，单位 KB
            done: function (json) {

                this.elem.closest("div").find("[name=ImageFullName]").attr("src", json.data.FullName);
                this.elem.closest("div").find("input[name=ImageId]").val(json.data.Id);
            }
        });
    }
    function addQuestion(e) {
        var addquestion = $(this).attr("addquestion");
        let uhtml = "<div>";
        switch (addquestion) {
            case "1": {
                uhtml = $("#questionTemplate1").html();
            } break;
            case "2": {
                uhtml = $("#questionTemplate2").html();
            } break;
            case "3": {
                uhtml = $("#questionTemplate3").html();
            } break;
            case "4": { } break;
        }
        let elem = $(uhtml).insertAfter($(this).closest("div.layui-form-item")).after($(this).closest("div.layui-form-item").clone());
        initUpload(elem.find(".upbtn"),100);
        initValidateItems(elem.find("form"));
        sortQuestion();
        return elem;
    }
    function addoption()
    {
        var optionElem = $($("#optionTemplate").html());
        $(this).before(optionElem);
        form.render();
        return optionElem;
    }

    function questionAction() {
        var optionaction = $(this).attr("optionaction");
        switch (optionaction) {
            case "2": {
                let thisquestion = $(this).closest("[question]");
                let thisquestionbtn = thisquestion.next(".questionbtn");
                let clone = thisquestion.clone().insertAfter(thisquestionbtn).after(thisquestionbtn.clone());
                initUpload(clone.find(".upbtn"),100);
                initValidateItems(clone.find("form"));
            } break;
            case "3": {
                $(this).closest("[question]").next(".questionbtn").remove();
                $(this).closest("[question]").remove();
            } break;
            case "-1": {
                let lastElem = $(this).closest("[question]").prevAll("[question]").eq(0);
                if (lastElem.length <= 0) {
                    return;
                }
                let questionbtn = lastElem.next(".questionbtn");
                let myquestionbtn = $(this).closest("[question]").next(".questionbtn");

                $(this).closest("[question]").insertBefore(questionbtn);
                lastElem.insertBefore(myquestionbtn);
            } break;
            case "1": {
                let nextElem = $(this).closest("[question]").nextAll("[question]").eq(0);
                if (nextElem.length <= 0) {
                    return;
                }
                let questionbtn = nextElem.next(".questionbtn");
                let myquestionbtn = $(this).closest("[question]").next(".questionbtn");

                $(this).closest("[question]").insertBefore(questionbtn);
                nextElem.insertBefore(myquestionbtn);
            } break;
        }
        sortQuestion();
    }
    function delOption() {
        $(this).closest(".layui-inline").remove();
    }
    initUpload($("#btnUpload"));

    $("#btnsave").on("click", function () {
        $("#baseInfo").submit();
    });

    //$("#questionInfo").find("form").each(function () {
    //    initValidateItems(this);
    //});
    
    // 渲染


    $(document)
        .on("click", "[name=ImageFullName]", function () {
        var src = $(this).attr("src");
        if (!src) return;

        layer.image("图标",src);
        })
        .on("click", ".optionbtn", addoption)
        .on("click", "button[addquestion]", addQuestion)
        .on("click", "button[optionaction]", questionAction)
        .on("click", "div.btndel", delOption);
    form.on("select(ClassifyId)", function (e) {
        getSubClassifies(e.value);
    });

    var defClassify = getClassifies();
    

    if (PageInfo.KeyValueStr) {
        
        $.when(renderDetail(), defClassify).then(function (json) {
            $("select[name=ClassifyId]").val(json.data.ClassifyId);
            return getSubClassifies(json.data.ClassifyId, json.data.SubClassifyId);
        })
        
    }
    initValidate();
});
