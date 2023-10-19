layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    let ztree;
    const table = layui.table;
    const form = layui.form;
    const laydate = layui.laydate;

    function initValidate() {
        $("#form").validate({
            submitHandler: function (form) {
                var userData = $$.getFormData("#userform");
                var postData = $$.getFormData("#infoform");
                postData.User = userData;

                var callbackstr;
                var addoreditFunc = PageInfo.KeyValueStr ? (callbackstr = "userinfoeditok", $$.common.edit.req) : (callbackstr = "userinfoaddok", $$.common.add.req);
                addoreditFunc(postData).then(function (json) {
                    
                    $$.callback(callbackstr, json);
                    $$.closeThis();
                    
                });
            },
            rules: {
                Account: {
                    required: true
                },
                Password: {
                    required: true
                },
            },
            messages: {
                Account: {
                    required: "请输入登录名"
                },
                Password: {
                    required: "请输入密码"
                },
            }
        });
    }
    function renderDetail() {
        $$.common.getDetail.req().then(function (json) {
            $$.setFormData("#userform",json.data.User);
            $$.setFormData("#infoform", json.data);
            $("[name=GroupName]").val((json.data.UserGroup || {}).Name||"");
            form.render();
        });
    }
    laydate.render({
        elem: 'input[name=Birthday]',
        type: "date",
        fullPanel: true
    });
    if (PageInfo.KeyValueStr) {
        renderDetail(PageInfo.KeyValueStr);
    }
    initValidate();

    function treeInit() {
        //$("#tree").height($(document).outerHeight() - 125 - 12);
        function dataFilter(treeId, parentNode, responseData) {
            responseData.data.forEach(function (a) {
                a.isParent = (a.ObjectTag || {}).IsParent || false;
            });
            return responseData.data;
        }
        var setting = {
            async: {
                enable: true,
                url: "/UserGroup/GetPageList",
                otherParam: function (treeId, treeNode) {
                    res = {
                        page: 1,
                        size: 10000,
                        where: {
                            ParentId: (treeNode || {}).Id || null,
                        }
                    };
                    return res;
                },
                dataFilter: dataFilter
            },
            data: {
                key: {
                    name: "Name"
                },
                simpleData: {
                    enable: true,
                    idKey: "Id",
                    pIdKey: "ParentId",
                }
            },
            view: {
                selectedMulti: false
            },
            callback: {
                onClick: function (event, treeId, treeNode, clickFlag) {
                    $("#tree").hide(100);
                    //选中
                    if (treeNode.Id == null) {
                        $("[name=GroupName]").val("");
                        $("[name=GroupId]").val("");
                        return;
                    }
                    $("[name=GroupName]").val(treeNode.Name);
                    $("[name=GroupId]").val(treeNode.Id);
                },
                
            },
            edit: {
                enable: false
            }
        };

        $("[name=GroupName]").on("focus click", function (e) {
            var offset = $("#tree").closest(".layui-card-body").offset();
            $("#tree").css({
                top: $(this).offset().top - offset.top + $(this).outerHeight(),
                left: $(this).offset().left - offset.left
            }).show(100);
        });
        $(document).on("click.tree", function () {
            $("#tree").hide(100);
        });
        $("#tree,[name=GroupName]").on("click", function (e) {
            e.stopPropagation();
        });
        
        var nodes = [{ Name: "全部", isParent: true, Id: null, ParentId: null }];

        return $.fn.zTree.init($("#tree"), setting, nodes);
    }
    ztree = treeInit();
});
