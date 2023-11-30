layui.config({
    base: "/js/"
}).use(['ztree','table'], async function () {
    let seletedData = [];
    let ztree;
    
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
    const cols2 = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'Title', title: '量表名称' },
        { field: 'Action', title: '操作', fixed: "right", templet: "#selectActionTemplate" ,width:80 },

    ]];
    callback.exampleaddok = function (json) {
        layer.success(json.msg);
        getList();
    }
    callback.exampleeditok = function (json) {
        layer.success(json.msg);
        getList();
    }
    callback.exampleresulteditok = function (json) {
        layer.success(json.msg);
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
    table.render({
        elem: '#select_table',
        data:[],
        cols: cols2,
        height: "350",
        size: "sm",

    });

    $("#searchForm").on("submit", function () {
        getList(1);
        return false;
    });

    function getList(page,size) {
        var postdata = $.extend($$.common.getPageList.paramArr, $$.getFormData("#searchForm"));
        if (ztree) {
            var nodes = ztree.getSelectedNodes();
            var node = {};
            if (nodes.length > 0) {
                node = nodes[0];
            }
            if (node.ParentId) {
                postdata.SubClassifyId = node.Id;
            }
            if (!node.ParentId && node.Id) {
                postdata.ClassifyId = node.Id;
            }
        }
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
            let removeIds = arr.map(function (a) { return a.Id});
            old = old.filter(function (a) {
                return removeIds.indexOf(a.Id)<0;
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
        $$.callback("exampleselectok", table.cache.select_table);
        $$.closeThis();
    });
    function treeInit() {
        $("#tree").height($(document).outerHeight() - 125-12);
        function dataFilter(treeId, parentNode, responseData) {
            responseData.data.forEach(function (a) {
                a.isParent = a.ParentId == null;
            });
            return responseData.data;
        }
        var setting = {
            async: {
                enable: true,
                url: "/Classify/GetPageList",
                otherParam: function (treeId, treeNode) {
                    res = {
                        page: 1,
                        size: 10000,
                        where: {
                            ParentId: (treeNode || {}).Id || null,
                        }
                    };
                    return res;
                },
                dataFilter: dataFilter
            },
            data: {
                key: {
                    name: "Name"
                },
                simpleData: {
                    enable: true,
                    idKey: "Id",
                    pIdKey: "ParentId",
                }
            },
            view: {
                selectedMulti: false
            },
            callback: {
                onClick: function (event, treeId, treeNode, clickFlag) {
                    getList(1);
                }
            }
        };
        var nodes = [{ Name: "全部", isParent: true, Id: null, ParentId: null }];

        return $.fn.zTree.init($("#tree"), setting, nodes);
    }
    ztree = treeInit();
});
