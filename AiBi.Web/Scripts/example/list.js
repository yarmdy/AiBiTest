layui.config({
    base: "/js/"
}).use(['table'], async function () {
    const table = layui.table;
    const cols = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'Title', title: '量表名称' },
        {
            field: 'ClassifyId', title: '分类', templet: function (d) {
                return d.Classify.Name;
            }
        },
        {
            field: 'SubClassifyId', title: '子分类', templet: function (d) {
                return (d.SubClassify || {}).Name||"";
            }
        },
        { field: 'Duration', title: '时长(分钟)' },
        { field: 'QuestionNum', title: '问题数' },
        { field: 'Keys', title: '关键字' },
        
        { field: 'Action', title: '操作',fixed:"right", templet: "#actionTemplate",width:210 },

    ]];
    callback.exampleaddok = function (json) {
        layer.success(json.msg);
        getList();
    }
    callback.exampleeditok = function (json) {
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
    
    table.on("tool(table)", function (e) {
        switch (e.event) {
            
        }
    });
    
});
