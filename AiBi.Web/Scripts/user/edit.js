layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;

    function initValidate() {
        $("#form").validate({
            submitHandler: function (form) {
                var postData = $$.getFormData("#userform");
                var infoData = $$.getFormData("#infoform");
                postData.BusUserInfoUsers = [infoData];

                var callbackstr;
                var addoreditFunc = PageInfo.KeyValueStr ? (callbackstr = "usereditok", $$.common.edit.req) : (callbackstr = "useraddok", $$.common.add.req);
                addoreditFunc(postData).then(function (json) {
                    layer.success(json.msg, null, function () {
                        $$.closeThis();
                    });
                    $$.callback(callbackstr, json);
                    
                });
            },
            rules: {
                Name: {
                    required: true
                },
                Account: {
                    required: true
                },
                Password: {
                    required: true
                },
                Type: {
                    required: true
                },

                
            },
            messages: {
                Name: {
                    required: "请输入用户名"
                },
                Account: {
                    required: "请输入登录名"
                },
                Password: {
                    required: "请输入密码"
                },
                Type: {
                    required: "请选择用户类别"
                },
            }
        });
    }
    function renderDetail() {
        $$.common.getDetail.req().then(function (json) {
            $$.setFormData("#userform",json.data);
            $$.setFormData("#infoform", json.data.BusUserInfoUsers[0]);
            form.render();
        });
    }

    if (PageInfo.KeyValueStr) {
        renderDetail(PageInfo.KeyValueStr);
    }
    initValidate();
});
