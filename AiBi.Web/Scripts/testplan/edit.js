layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;

    var upload = layui.upload;

    callback.templateselectok = function (data) {
        $("[name=TemplateName]").val(data[0].Title);
        $("[name=TemplateId]").val(data[0].Id);
    }
    callback.userinfoselectok = function (data) {
        var old = table.cache.table_user;
        data.forEach(function (a) {
            if (old.findIndex(function (b) { return b.UserId == a.UserId; }) >= 0) {
                return;
            }
            old.push(a);
        });
        setTimeout(function () {
            table.reloadData("table_user", {
                data: old
            });
        });
    }
    function initValidate() {
        $("#form").validate({
            submitHandler: function (form) {

                var postData = $$.getFormData("#form");
                postData.BusTestPlanUsers = table.cache.table_user.map(function (a) {
                    return { UserId : a.UserId };
                });
                var callbackstr;
                var addoreditFunc = PageInfo.KeyValueStr ? (callbackstr = "testplaneditok", $$.common.edit.req) : (callbackstr = "testplanaddok", $$.common.add.req);
                addoreditFunc(postData).then(function (json) {

                    $$.callback(callbackstr, json);
                    $$.closeThis();

                });
                return false;
            },
            rules: {
                Name: {
                    required: true
                },
                StartTime: {
                    required: true
                },
                EndTime: {
                    required: true
                },
                TemplateName: {
                    required: true
                },

            },
            messages: {
                Name: {
                    required: "请输入任务名称"
                },
                StartTime: {
                    required: "请输入开始时间"
                },
                EndTime: {
                    required: "请输入结束时间"
                },
                TemplateName: {
                    required: "请选择任务分类"
                },
                
            }
        });
    }
    function renderDetail() {
        return $$.common.getDetail.req().then(function (json) {
            $$.setFormData("#form", json.data);
            $("[name=TemplateName]").val(json.data.Template.Title);
            table.reloadData("table_user", {
                data: json.data.BusTestPlanUsers.map(function (a) { return a; })
            });
            form.render();
            return json;
        });
    }

    $("#btnsave").on("click", function () {
        $("#form").submit();
    });
    

    let cols = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'RealName', title: '姓名' },
        { field: 'Mobile', title: '手机号', templet: function (d) { return d.User.Mobile; } },
        {
            field: 'Sex', title: '性别', templet: function (d) {
                return EnumSex[d.Sex];
            }
        },
        { field: 'UnitName', title: '单位' },
        { field: 'IdCardNo', title: '身份证号' },
        { field: 'Birthday', title: '生日' },
        { field: 'Action', title: '操作', templet: function (d) { return '<button type="button" class="layui-btn layui-btn-xs" lay-event="delete">删除</button>'} },
    ]];
    table.render({
        elem: '#table_user',
        data: [],
        cols: cols,
        height: 155,
        size:"sm"
    });
    table.on("tool(table_user)", function (e) {
        table.reloadData("table_user", {
            data: table.cache.table_user.filter(function (a) { return a.Id != e.data.Id })
        });
    });
    $("#btnDeleteUser").on("click", function () {
        table.reloadData("table_user", {
            data: table.cache.table_user.filter(function (a) { return !a.LAY_CHECKED; })
        });
    });
    
    if (PageInfo.KeyValueStr) {
        renderDetail();
    }
    initValidate();
});
