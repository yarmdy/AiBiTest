layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;
    const laydate = layui.laydate;
    const transfer = layui.transfer;

    var upload = layui.upload;

    async function getTemplate(id,value) {
        var json = await $.get("/TestTemplate/GetDetail/" + id);
        //console.log(json);
        var list = json.data.BusTestTemplateExamples;
        reloadExample(value == null ? list.map(a => a.Enabled ? a.ExampleId : null) : value, list.map(a => { return { value: a.Example.Id, title: a.Example.Title }; }));
    }

    callback.templateselectok = async function (data) {
        $("[name=TemplateName]").val(data[0].Title);
        $("[name=TemplateId]").val(data[0].Id);
        getTemplate(data[0].Id);
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
                postData.BusTestPlanExamples = transfer.getData("examples").map(a => { return { ExampleId: a.value }; });
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
                    required: "请选择量表组合"
                },
                
            }
        });
    }
    function renderDetail() {
        return $$.common.getDetail.req().then(function (json) {
            $$.setFormData("#form", json.data);
            $("[name=TemplateName]").val(json.data.Template.Title);

            var data = json.data.BusTestPlanUsers.map(function (a) {
                var tmpUser = {};
                $.extend(true, tmpUser, a.User);
                tmpUser.BusUserInfoUsers = null;
                a.User.BusUserInfoUsers[0].User = tmpUser;
                return a.User.BusUserInfoUsers[0];
            });
            table.reloadData("table_user", {
                data: data
            });
            form.render();
            return json;
        });
    }
    function renderExample() {
        // 渲染
        transfer.render({
            elem: '#examples',
            id:"examples",
            data: [],
            height: 250,
            title: ["未选择","已选择"]
        });
    }
    function reloadExample(value,data) {
        transfer.reload("examples", {
            value: value,
            data: data
        });
        if (data) {
            transfer.examples = data;
        }
    }

    layui.util.on("lay-on", {
        sortExample:async function (ele) {
            var selected = transfer.getData("examples");
            console.log(selected);
            if (selected.length <= 0) {
                layer.error("没有选择任何量表，请先选择量表");
                return;
            }
            var arr = selected.map(a => { return { id: a.value, name: a.title }; });

            var res = await SortItems.sort(arr);

            reloadExample(res);
        }
    });
    renderExample();
    $("#btnsave").on("click", function () {
        $("#form").submit();
    });

    laydate.render({
        elem: '#laydaterange',
        range: ['[name=StartTime]', '[name=EndTime]'],
        rangeLinked: true, // 开启日期范围选择时的区间联动标注模式 ---  2.8+ 新增
        type: "datetime",
        fullPanel:true
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
        switch (e.event) {
            case "delete": {
                table.reloadData("table_user", {
                    data: table.cache.table_user.filter(function (a) { return a.UserId != e.data.UserId })
                });
            } break;
        }
    });
    $("#btnDeleteUser").on("click", function () {
        table.reloadData("table_user", {
            data: table.cache.table_user.filter(function (a) { return !a.LAY_CHECKED; })
        });
    });
    
    if (PageInfo.KeyValueStr) {
        let detailData = await renderDetail();
        await getTemplate(detailData.data.TemplateId, detailData.data.BusTestPlanExamples.map(a => a.ExampleId));
    }
    initValidate();
});
