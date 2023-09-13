﻿$(function () {
    function getDetail() {
        function render(str) {
            $("#detail").html(str);
        }
        if (PageInfo.KeyValueStr) {
            $$.common.getDetail.req().then(function (json) {
                json.data.json = json;
                let item = $("#detailtemplate").text().combineObject(json.data);
                render(item);
            });
        } else {
            render($("#emptytemplate").text());
        }
    }
    callback.templateSelectOk = function (obj) {
        $("#detail [name=TemplateId]").val(obj.Id);
        $("#detail [name=TemplateName]").val(obj.Title);
        $("#form").valid();
    }
    callback.userInfoSelectOk = function (obj) {
        console.log(obj);
    }

    $(document).on("click.selecttemplate", ".selecttemplate", function () {
        $$.addTab("选择模板", "/TestTemplate/MySelect");
    }).on("click.selectuserinfo", ".selectuserinfo", function () {
        $$.addTab("选择学员", "/UserInfo/List");
    });
    
    function initValidate() {
        $("#form").validate({
            submitHandler: function (form) {
                
            },
            rules: {
                Name: {
                    required: true
                },
                StartTime: {
                    required: true,
                },
                EndTime: {
                    required: true
                },
                TemplateName: {
                    required: true
                }
            },
            messages: {
                Name: {
                    required: "请输入任务名称"
                }
            }
        });
    }

    initValidate();
    getDetail();
});