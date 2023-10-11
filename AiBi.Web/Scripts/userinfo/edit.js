layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
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
});
