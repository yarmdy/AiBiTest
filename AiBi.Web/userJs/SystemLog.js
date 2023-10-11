layui.config({
    base: "/js/"
}).use(['form',  'ztree', 'layer', 'jquery', 'table', 'openauth', 'utils', 'laydate'], function() {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery;
    var table = layui.table;
    var laydate = layui.laydate;
    var currentPage = 1;

    var ReportDate = null;

    //日期范围
    laydate.render({
        elem: '#txtOperationTime',
        range: true,
        done: function (value, date, endDate) {
            ReportDate = value;
        }
    });

    //主列表加载，可反复调用进行刷新
    var config = {};  //table的参数，如搜索key，点击tree的id
    var mainList = function (options) {
        if (options != undefined) {
            $.extend(config, options);
        }
        table.reload('mainList',
            {
                url: '/SystemLog/GetTableData',
                where: config,
                page: {
                    curr: currentPage //重新从第 1 页开始
                },
                done: function (res, curr, count) {
                    var hasData = res != undefined &&
                        res != null &&
                        res.data != undefined &&
                        res.data != null &&
                        res.data.length > 0;

                    var hasCount = res != undefined &&
                        res != null &&
                        res.count != undefined &&
                        res.count != null &&
                        res.count > 0 &&
                        count != undefined &&
                        count != null &&
                        count > 0;

                    if (!hasData && hasCount) {
                        currentPage = (count % this.limit == 0 ? count / this.limit : (count / this.limit) + 1);
                        mainList();
                        return false;
                    }

                    currentPage = curr;
                }
            });
    };
    //左边树状机构列表
    var ztree = function () {
        var url = '/UserSession/GetOrgs';
        var zTreeObj;
        var setting = {
            view: { selectedMulti: false },
            data: {
                key: {
                    name: 'Name',
                    title: 'Name'
                },
                simpleData: {
                    enable: true,
                    idKey: 'Id',
                    pIdKey: 'ParentId',
                    rootPId: 'null'
                }
            },
            callback: {
                onClick: function (event, treeId, treeNode) {
                    currentPage = 1;
                    mainList({ OrgId: treeNode.Id });
                }
            }
        };
        var load = function () {
            $.getJSON(url, function (json) {
                zTreeObj = $.fn.zTree.init($("#tree"), setting);
                var newNode = { Name: "全部", Id: null, ParentId: "" };
                json.push(newNode);
                zTreeObj.addNodes(null, json);
                currentPage = 1;
                mainList({ OrgId: "" });
                zTreeObj.expandAll(false);

                var nodes = zTreeObj.getNodes();
                if (nodes.length > 0) {
                    for (var i = 0; i < nodes.length; i++) {
                        zTreeObj.expandNode(nodes[i], true, false, false);
                    }
                }
            });
        };
        load();
        return {
            reload: load
        }
    }();
    $("#tree").height($("div.layui-table-view").height());

    //查看对话框
    var editDlg = function () {
        var vm = new Vue({
            el: "#formView"
        });
        var title = "";
        var show = function (data) {
            var index = layer.open({
                title: title,
                area: ["700px", "96%"],
                type: 1,
                content: $('#divView'),
                success: function () {
                    vm.$set('$data', data);
                },
                end: mainList
            });
        }
        return {
            detail: function (data) {
                title = "查看";
                show(data);
            }
        };
    }();

    //监听表格内部按钮
    table.on('tool(list)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') {      //查看
            editDlg.detail(data);
        }
    });

    //监听页面主按钮操作
    var active = {
        btnSearch: function () { //搜索
            var strStartDate = "";
            var strEndDate = "";
            var a = new Array();
            if (ReportDate != null) {
                a = ReportDate.split(" - ");
                strStartDate = a[0];
                strEndDate = a[1];
            }

            currentPage = 1;
            mainList({
                ModuleName: $("input#txtModuleName").val(),
                DataIdentify: $("input#txtDataIdentify").val(),
                Operation: $("input#txtOperation").val(),
                Operator: $("input#txtOperator").val(),
                StartDate: strStartDate,
                EndDate: strEndDate
            });
        }
    };

    $('.toolList .layui-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
});