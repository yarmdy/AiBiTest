layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    layer = top.layer;
    const table = layui.table;
    const form = layui.form;
    const cols = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'Name', title: '分类名称' },
        { field: 'SortNo', title: '排序号' },
        {
            field: 'Parent', title: '上级', templet: function (d) {
                return ((d.Parent || {}).Name)||"-";
            }
        },
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
        method: "post",
        
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
    callback.classifyaddok = function (json) {
        getList(1, 10);
    }
    callback.classifyeditok = function (json) {
        getList(1, 10);
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
            layer.open({
                title: "密码", area: ["300px", "200px"], content: json.data, btn: ["确定", "重置"],
                btn1: function (e) {
                    layer.close(e);
                },
                btn2: function(){
                    layer.prompt({ title: "请输入密码", formType: 1 }, function (pwd, index) {
                        url = BaseUrl + "/SetPassword/" + e.data.Id;
                        $$.post(url, { password: pwd }).then(function (json) {
                            layer.msg(json.msg);
                            layer.close(index);
                            e.update({ Password: json.data }, true);
                        });
                    });
                }
            });
        });
    }
    getList();
});
