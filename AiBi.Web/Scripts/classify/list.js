layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    let ztree;
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
        if (ztree) {
            var nodes = ztree.getSelectedNodes();
            var node = {};
            if (nodes.length > 0) {
                node = nodes[0];
            }
            if (node.ParentId) {
                postdata.where = { Id:node.Id };
            }
            if (!node.ParentId && node.Id) {
                postdata.where = { ParentId: node.Id };
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
    callback.classifyaddok = function (json) {
        layer.success(json.msg);
        getList(1, 10);
    }
    callback.classifyeditok = function (json) {
        layer.success(json.msg);
        getList(1, 10);
    }
    function treeInit() {
        $("#tree").height($(document).outerHeight() - 125 - 12);
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
