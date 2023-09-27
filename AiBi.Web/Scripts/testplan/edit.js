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
        console.log(data);
        var old = table.cache.table_user;
        var newd = data.forEach(function (a) {
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
                data: json.data.BusTestTemplateExamples.map(function (a) { return $.extend(a.Example, a),delete a.Example.Example, a.Example; })
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
        { field: 'Title', title: '标题' },
        { field: 'Duration', title: '时长(分钟)' },
        { field: 'QuestionNum', title: '问题数' },
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
