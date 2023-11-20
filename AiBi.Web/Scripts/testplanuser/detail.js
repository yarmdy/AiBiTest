﻿layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;
    const laydate = layui.laydate;

    var upload = layui.upload;

    var laytpl = layui.laytpl

    layui.util.on("lay-on", {
        export: function () {
            $("#downform").submit();
            layer.msg("已发送导出请求，不要关闭此页，等待下载结束", {
                title: "注意", icon: 1, btn: ["确认"], yes: function (r) {
                    layer.close(r);
                }, time: 0, shade: 0.5
            });
        }
    });
    async function renderDetail() {
        let json = await $$.common.getDetail.req();

        let plan = json.data.Plan;
        var exDic = plan.BusTestPlanExamples.reduce((r, a, i) => {
            return r[a.ExampleId] = a, r;
        }, {});
        let tmpExamples = plan.Template.BusTestTemplateExamples.filter(a => exDic[a.ExampleId]).sort(function (a, b) { return exDic[a.ExampleId].SortNo - exDic[b.ExampleId].SortNo; });

        json.data.Questions = tmpExamples.reduce(function (last, cur) {
            return last.concat(cur.Example.BusExampleQuestions);
        }, []);
        json.data.Results = tmpExamples.reduce(function (last, cur) {
            return last.concat(cur.Example.BusExampleResults);
        }, []);

        let html = laytpl($("#pageTemplate").html()).render(json.data);
        window.PageData = json.data;
        $("#page").html(html);
        $("#pagenum").html(Math.ceil($(document).height() / 794));
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
