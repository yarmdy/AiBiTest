layui.config({
    base: "/js/"
}).use(['table'], async function () {
    const table = layui.table;
    const cols = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'Title', title: '标题' },
        {
            field: 'CanPause', title: '可中断', templet: function (d) {
                return d.CanPause?"可以":"不可以"
            }
        },
        { field: 'Duration', title: '时长(分钟)' },
        { field: 'ExampleNum', title: '量表数量' },
        { field: 'QuestionNum', title: '问题数' },
        { field: 'Keys', title: '关键字' },
        
        { field: 'Action', title: '操作',fixed:"right", templet: "#actionTemplate",width:210 },

    ]];
    
    callback.templateaddok = function (json) {
        layer.success(json.msg);
        getList();
    }
    callback.templateeditok = function (json) {
        layer.success(json.msg);
        getList();
    }
    callback.templateselectok = function (data) {
        //layer.success(data.length);
        //getList();
        $$.post(BaseUrl + "/AddToIt/" + PageInfo.KeyValueStr, { ids: data.map(function (a) { return a.Id }) }).then(function (json) {
            layer.success(json.msg);
            getList();
        });
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
        where: $$.common.getPageList.paramArr,
        parseData: function (json) {
            var old = table.cache.select_table;
            var oldIds = old.map(function (a) { return a.Id });
            json.code = json.code > 0 ? 0 : json.code;
            json.data.forEach(function (a) {
                a.LAY_CHECKED = (oldIds.indexOf(a.Id) >= 0);
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
        postdata = $.extend(postdata, $$.common.getPageList.paramArr);
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
            case "remove": {
                layer.confirm("确定要移除这条量表组合吗？<br />移除后用户将无法使用", {icon:3}, function (l) {
                    layer.close(l);
                    $$.post(BaseUrl + "/Remove/" + PageInfo.KeyValueStr, { ids: [e.data.Id] }).then(function (json) {
                        layer.success(json.msg);
                        getList();
                    });
                })
                
            } break;
        }
    });



    const cols2 = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'Title', title: '标题' },
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
                var old = table.cache.select_table.filter(a => a.Id != e.data.Id);
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
            var oldIds = old.map(function (a) { return a.Id });
            var newArr = arr.filter(function (a) {
                a.LAY_CHECKED = false;
                return oldIds.indexOf(a.Id) < 0;
            });
            old = old.concat(newArr);
        } else {
            let removeIds = arr.map(function (a) { return a.Id });
            old = old.filter(function (a) {
                return removeIds.indexOf(a.Id) < 0;
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
        $$.callback("templateselectok", table.cache.select_table);
        $$.closeThis();
    });
    
});
