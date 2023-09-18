layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;
    const cols = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'Name', title: '用户名' },
        { field: 'Account', title: '登录名' },
        { field: 'Mobile', title: '手机号' },
        { field: 'Status', title: '状态', templet: function (d) { return EnumStatus[d.Status]; } },
        { field: 'ObjectTag.UserInfo.RealName', title: '真实姓名', templet: function (d) { return (d.ObjectTag.UserInfo||{}).RealName||""; } },
        { field: 'ObjectTag.UserInfo.Sex', title: '性别', templet: function (d) { return (d.ObjectTag.UserInfo||{}).Sex && EnumSex[(d.ObjectTag.UserInfo||{}).Sex]||""; } },
        { field: 'ObjectTag.RoleNames', title: '角色', width: 150, templet: function (d) { return d.ObjectTag.Roles.map(function (a) { return a.Name}).join(",")} },
        { field: 'ObjectTag.UserInfo.UnitName', title: '单位', width: 150, templet: function (d) { return (d.ObjectTag.UserInfo||{}).UnitName||""; } },
        { field: 'ObjectTag.UserInfo.IdCardNo', title: '身份证号',width:150, templet: function (d) { return (d.ObjectTag.UserInfo||{}).IdCardNo||""; } },
        { field: 'ObjectTag.UserInfo.Birth', title: '生日', templet: function (d) { return (d.ObjectTag.UserInfo||{}).Birthday && new Date((d.ObjectTag.UserInfo||{}).Birthday).format("yyyy-MM-dd")||""; } },
        { field: 'Action', title: '操作',fixed:"right", templet: "#actionTemplate",width:210 },

    ]];
    table.render({
        elem: '#table',
        url: BaseUrl+ $$.common.getPageList.url, // 此处为静态模拟数据，实际使用时需换成真实接口
        cols: cols,
        page: true,
        limits: [10, 50, 1000],
        limit: 10,
        request: {
            pageName: 'page', // 页码的参数名称，默认：page
            limitName: 'size' // 每页数据条数的参数名，默认：limit
        },
        parseData: function (json) {
            json.code = json.code > 0 ? 0 : json.code;
            return json;
        },
        height: "full-60",
        size: "sm",
        method:"post"
    });

    $("#searchForm").on("submit", function () {
        getList(1);
        return false;
    });

    function getList(page) {
        var postdata = $$.getFormData("#searchForm");
        table.reloadData("table", {
            where: postdata,
            page: {
                curr:page,
            }
        });
    }
    callback.useraddok = function (json) {
        getList(1, 10);
    }
    callback.usereditok = function (json) {
        getList(1, 10);
    }
    getList(1,10);
});
