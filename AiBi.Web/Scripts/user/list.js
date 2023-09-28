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
        { field: 'Action', title: '操作',fixed:"right", templet: "#actionTemplate",width:290 },

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

    function getList(page,size) {
        var postdata = $$.getFormData("#searchForm");
        table.reloadData("table", {
            where: postdata,
            page: page ? {
                curr: page,
                limit: size
            } : {}
        });
    }
    callback.useraddok = function (json) {
        getList(1, 10);
        layer.success(json.msg);
    }
    callback.usereditok = function (json) {
        getList(1, 10);
        layer.success(json.msg);
    }
    table.on("tool(table)", function (e) {
        switch (e.event) {
            case "enable":
                enableUser(e);
                break;
            case "password":
                passwordUser(e);
                break;
        }
    });

    function enableUser(e) {
        var url = BaseUrl + "/EnableUser/" + e.data.Id;
        var tarStatus = e.data.Status == 0 ? 1 : 0;
        $$.post(url, { status: tarStatus }).then(function (json) {
            e.update({ Status: tarStatus },true);
        });
    }
    function passwordUser(e) {
        var url = BaseUrl + "/ShowPassword/" + e.data.Id;
        $$.post(url, { password: e.data.Password }).then(function (json) {
            layer.prompt({
                title: "旧密码:" + json.data, area: ["300px", "200px"], formType: 1,btn:["重置","取消"]
            }, function (pwd, index) {
                url = BaseUrl + "/SetPassword/" + e.data.Id;
                $$.post(url, { password: pwd }).then(function (json) {
                    layer.msg(json.msg);
                    layer.close(index);
                    e.update({ Password: json.data }, true);
                });
            });
        });
    }
});
