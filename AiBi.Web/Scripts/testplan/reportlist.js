﻿layui.config({
    base: "/js/"
}).use(['table'], async function () {
    const table = layui.table;
    const cols = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'Name', title: '任务名称' },
        { field: 'StartTime', title: '开始时间', width: 150 },
        { field: 'EndTime', title: '结束时间', width: 150 },
        {
            field: 'Template', title: '量表组合', templet: function (d) {
                return d.Template.Title;
            }
        },
        {
            field: 'CanPause', title: '可中断', templet: function (d) {
                return d.CanPause?"可以":"不可以"
            },width:90
        },
        {
            field: 'Duration', title: '时长(分钟)', templet: function (d) {
                return d.Template.Duration
            }, width: 90
        },
        { field: 'ExampleNum', title: '量表数量', width: 90 },
        { field: 'QuestionNum', title: '问题数', width: 90 },
        {
            field: 'UserNum', title: '完成进度', width: 150, templet: "#progressTemplate"
        },
        { field: 'Action', title: '操作',fixed:"right", templet: "#actionTemplate",width:90 },

    ]];
    callback.testplanaddok = function (json) {
        layer.success(json.msg);
        getList();
    }
    callback.testplaneditok = function (json) {
        layer.success(json.msg);
        getList();
    }

    table.render({
        elem: '#table',
        url: BaseUrl + $$.common.getPageList.url, // 此处为静态模拟数据，实际使用时需换成真实接口
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
        done: function (res, curr, count, origin) {
            layui.element.render('progress')
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
    
});
