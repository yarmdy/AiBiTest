layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;
    const laydate = layui.laydate;

    var upload = layui.upload;

    var laytpl = layui.laytpl

    
    async function renderDetail() {
        let json = await $$.common.getDetail.req();
        json.data.Questions = json.data.Plan.Template.BusTestTemplateExamples.reduce(function (last, cur) {
            return last.concat(cur.Example.BusExampleQuestions);
        }, []);
        let html = laytpl($("#pageTemplate").html()).render(json.data);
        $("#page").html(html);
    }
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
    $(document)
        .on("click", "img", function () {
            var src = $(this).attr("src");
            if (!src) return;

            layer.image("图片", src);
        });
    if (PageInfo.KeyValueStr) {
        renderDetail();
    }
});
