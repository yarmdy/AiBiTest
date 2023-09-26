layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;

    var upload = layui.upload;

    
    function initValidate() {
        $("#baseInfo").validate({
            submitHandler: function (form) {
                var allvalid = true;
                $("form:not(#baseInfo)").each(function (i, a) {
                    allvalid = $(a).valid() && allvalid;
                    $(a).submit();
                });

                if (!allvalid) {
                    return false;
                };

                var postData = {};
                $.extend(postData, getChildData());
                $$.post("/Example/SaveResults/" + PageInfo.KeyValueStr, postData).then(function (json) {
                    $$.callback("exampleresulteditok", json);
                    $$.closeThis();
                });
                return false;
            },
            rules: {
            },
            messages: {
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
                    MinScore: {
                        required: true,
                        min: 0,
                        step:1,
                    },
                    MaxScore: {
                        required: true,
                        min: 0,
                        step:1,
                    },
                    NContent: {
                        required: true,
                    }

                },
                messages: {
                    Title: {
                        required: "请输入答案标题"
                    },
                    Code: {
                        required: "请输入答案代码"
                    },
                    MinScore: {
                        required: "请输入最小分数",
                        min: "不能小于0",
                        step: "必须是整数",
                    },
                    MaxScore: {
                        required: "请输入最大分数",
                        min: "不能小于0",
                        step: "必须是整数",
                    },
                    NContent: {
                        required: "请输入答案详解",
                    }

                },
                errorPlacement: function (error, element) {
                    if (element.parent().hasClass("layui-input-wrap")) {
                        error.insertAfter(element.parent());
                        return;
                    }
                    error.appendTo(element.parent());
                },
                focusInvalid: true
            });
        });
    }
    function getChildData() {
        var res = { BusExampleResults: [] };
        $("form:not(#baseInfo)").each(function (i, a) {
            var item = $$.getFormData(a);
            res.BusExampleResults.push(item);
        });
        return res;
    }
    function renderDetail(json) {
        json.data.BusExampleResults.forEach(function (a, i) {
            var detail = addResult.call(lastBtnAdd());
            $$.setFormData(detail, a);
            detail.find("[name=ImageFullName]").attr("src", (a.Image || {}).FullName);
        });
    }

    function lastBtnAdd() {
        var btns = $("[addresult]");
        return btns[btns.length - 1];
    }
    
    
    function sortQuestion() {
        let children = $("#resultInfo").children("[combine]");
        children.each(function (i, a) {
            $(a).find("[name=SortNo]").val((i + 1) + "");
            $(a).find(".resultnumber").html((i + 1) + "");
        });
        form.render();
    }

    
    function initUpload(elem, module = 200) {
        $(elem).each(function (i,a) {
            upload.render({
                elem: $(a),
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
        });
        
    }
    function addResult(e) {
        let uhtml = $("#resultTemplate").html();
        let elem = $(uhtml).insertAfter($(this).closest("div.layui-form-item")).after($(this).closest("div.layui-form-item").clone());
        initUpload(elem.find(".upbtn"),100);
        initValidateItems(elem.find("form"));
        sortQuestion();
        elem.hide().show(300);
        return elem;
    }
    
    function resultAction() {
        var resultaction = $(this).attr("resultaction");
        switch (resultaction) {
            case "2": {
                let thisquestion = $(this).closest("[combine]");
                let thisresultbtn = thisquestion.next(".resultbtn");
                let clone = thisquestion.clone();
                clone.find("[name=ImageFullName]").attr("src", "");
                clone.find("[name=ImageId]").val("");
                clone.find("[name=Id]").val("");
                clone.insertAfter(thisresultbtn).after(thisresultbtn.clone());
                initUpload(clone.find(".upbtn"),100);
                initValidateItems(clone.find("form"));
                clone.hide().show(300);
            } break;
            case "3": {
                $(this).closest("[combine]").hide(300, function () {
                    $(this).closest("[combine]").next(".resultbtn").remove();
                    $(this).closest("[combine]").remove();
                    sortQuestion();
                });
                
            } break;
            case "-1": {
                let lastElem = $(this).closest("[combine]").prevAll("[combine]").eq(0);
                if (lastElem.length <= 0) {
                    return;
                }
                let resultbtn = lastElem.next(".resultbtn");
                let myresultbtn = $(this).closest("[combine]").next(".resultbtn");
                let myElem = $(this).closest("[combine]");
                

                let myPos = [myElem.offset().left, myElem.offset().top];
                let tarPos = [lastElem.offset().left, lastElem.offset().top];
                myElem.insertBefore(resultbtn).css("visibility", "hidden");
                lastElem.insertBefore(myresultbtn).css("visibility", "hidden");

                myElem.clone().css({ "visibility": "visible",position: "absolute", width: myElem.width() + "px", height: myElem.height() + "px", left: myPos[0] + "px", top: myPos[1] + "px" }).appendTo("body").animate({ left: tarPos[0] + "px", top: tarPos[1] + "px" }, function () {
                    myElem.css("visibility", "visible");
                    lastElem.css("visibility", "visible");
                    $(this).remove();
                });
                lastElem.clone().css({ "visibility": "visible",position: "absolute", width: lastElem.width() + "px", height: lastElem.height() + "px", left: tarPos[0] + "px", top: tarPos[1] + "px" }).appendTo("body").animate({ left: myPos[0] + "px", top: myPos[1] + "px" }, function () {
                    $(this).remove()
                });
                
            } break;
            case "1": {
                let nextElem = $(this).closest("[combine]").nextAll("[combine]").eq(0);
                if (nextElem.length <= 0) {
                    return;
                }
                let resultbtn = nextElem.next(".resultbtn");
                let myresultbtn = $(this).closest("[combine]").next(".resultbtn");
                let myElem = $(this).closest("[combine]");

                let myPos = [myElem.offset().left, myElem.offset().top];
                let tarPos = [nextElem.offset().left, nextElem.offset().top];
                myElem.insertBefore(resultbtn).css("visibility", "hidden");
                nextElem.insertBefore(myresultbtn).css("visibility", "hidden");

                myElem.clone().css({ "visibility": "visible", position: "absolute", width: myElem.width() + "px", height: myElem.height() + "px", left: myPos[0] + "px", top: myPos[1] + "px" }).appendTo("body").animate({ left: tarPos[0] + "px", top: tarPos[1] + "px" }, "linear",function () {
                    myElem.css("visibility", "visible");
                    nextElem.css("visibility", "visible");
                    $(this).remove();
                });
                nextElem.clone().css({ "visibility": "visible", position: "absolute", width: nextElem.width() + "px", height: nextElem.height() + "px", left: tarPos[0] + "px", top: tarPos[1] + "px" }).appendTo("body").animate({ left: myPos[0] + "px", top: myPos[1] + "px" }, "linear",function () {
                    $(this).remove()
                });

            } break;
        }
        sortQuestion();
    }
    
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

            layer.image("图标", src);
        })
        .on("click", "button[addresult]", addResult)
        .on("click", "button[resultaction]", resultAction);
    

    $$.get("/Example/GetResults/" + PageInfo.KeyValueStr).then(function (json) {
        renderDetail(json);
    });
    initValidate();
});
