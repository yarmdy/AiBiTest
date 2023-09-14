layui.config({
    base: "/js/"
}).use(['form', 'laydate', 'vue', 'layer', 'jquery', 'table', 'openauth', 'utils'], function() {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery;
    var table = layui.table;
    var openauth = layui.openauth;
    var laydate = layui.laydate;
    var currentPage = 1;

    var ReportDate = null;

    //日期范围
    laydate.render({
        elem: '#txtRestoreTime',
        range: true,
        done: function(value, date, endDate) {
            ReportDate = value;
        }
    });

    $("#menus").loadMenus("DbRestore");

    //主列表加载，可反复调用进行刷新
    var config = {}; //table的参数，如搜索key，点击tree的id

    var mainList = function(options) {
        if (options !== undefined) {
            $.extend(config, options);
        }
        table.reload('mainList',
            {
                url: '/DbRestore/GetTableData',
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

    //监听页面主按钮操作
    var active = {
        btnSearch: function () {
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
                StartDate: strStartDate,
                EndDate: strEndDate
            });
        }
    };

    $('.toolList .layui-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });

    mainList();
});