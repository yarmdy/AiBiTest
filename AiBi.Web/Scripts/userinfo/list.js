layui.config({
    base: "/js/"
}).use(['table'], async function () {
    const table = layui.table;
    const cols = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'RealName', title: '姓名' },
        { field: 'Account', title: '账号', templet:function(d) { return d.User.Account; } },
        { field: 'Mobile', title: '手机号', templet: function (d) { return d.User.Mobile||""; } },
        {
            field: 'Sex', title: '性别', templet: function (d) {
                return EnumSex[d.Sex]||"";
            }
        },
        { field: 'UnitName', title: '单位' },
        { field: 'IdCardNo', title: '身份证号' },
        { field: 'Birthday', title: '生日' },
        { field: 'Action', title: '操作',fixed:"right", templet: "#actionTemplate",width:210 },

    ]];
    
    callback.userinfoaddok = function (json) {
        layer.success(json.msg);
        getList();
    }
    callback.userinfoeditok = function (json) {
        layer.success(json.msg);
        getList();
    }

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
            var old = table.cache.select_table;
            var oldIds = old.map(function (a) { return a.UserId });
            json.code = json.code > 0 ? 0 : json.code;
            json.data.forEach(function (a) {
                a.LAY_CHECKED = (oldIds.indexOf(a.UserId) >= 0);
            });
            return json;
        },
        height: "full-125",
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
    
    table.on("tool(table)", function (e) {
        switch (e.event) {
            
        }
    });



    const cols2 = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'RealName', title: '姓名' },
        { field: 'Action', title: '操作', fixed: "right", templet: "#selectActionTemplate", width: 80 },

    ]];
    table.render({
        elem: '#select_table',
        data: [],
        cols: cols2,
        height: "350",
        size: "sm",

    });
    table.on("tool(select_table)", function (e) {
        switch (e.event) {
            case "delete": {
                var old = table.cache.select_table.filter(a => a.UserId != e.data.UserId);
                table.reloadData("select_table", { data: old });
                getList();
            } break;
        }
    });

    table.on("checkbox(table)", function (e) {
        var arr = [];
        if (e.type == "one") {
            arr.push(e.data);
        } else if (e.type == "all") {
            arr = arr.concat(table.cache.table);
        }

        var old = table.cache.select_table;
        if (e.checked) {
            var oldIds = old.map(function (a) { return a.UserId });
            var newArr = arr.filter(function (a) {
                a.LAY_CHECKED = false;
                return oldIds.indexOf(a.UserId) < 0;
            });
            old = old.concat(newArr);
        } else {
            let removeIds = arr.map(function (a) { return a.UserId });
            old = old.filter(function (a) {
                return removeIds.indexOf(a.UserId) < 0;
            });
        }
        table.reloadData("select_table", { data: old });
    });
    let selectH = $("#selectH").height();
    $("#minsize").on("click", function () {
        if (this.isMin) {
            $("#selectH").animate({ height: selectH + "px" }, function () {
                $("#selectH").css({ height: "auto" });
            });
            this.isMin = false;
        } else {
            $("#selectH").animate({ height: "0" });
            this.isMin = true;
        }
    });
    $("#selectok").on("click", function () {
        $$.callback("userinfoselectok", table.cache.select_table);
        $$.closeThis();
    });
    
});
