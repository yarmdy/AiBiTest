layui.config({
    base: "/js/"
}).use(['form', 'laydate',  'layer', 'jquery', 'table', 'openauth', 'utils'], function() {
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
        elem: '#txtBackupTime',
        range: true,
        done: function (value, date, endDate) {
            ReportDate = value;
        }
    });

    $("#menus").loadMenus("DbBackup");

    //主列表加载，可反复调用进行刷新
    var config = {}; //table的参数，如搜索key，点击tree的id

    var mainList = function(options) {
        if (options !== undefined) {
            $.extend(config, options);
        }
        table.reload('mainList',
            {
                url: '/DbBackup/GetTableData',
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
        btnBackup: function () {
            layer.confirm('您确定现在要备份系统的数据库吗?',
                function (index) {
                    var loadIndex = layer.load();
                    $.ajax({
                        url: "/DbBackup/Backup",
                        type: "POST",
                        dataType: "json",
                        timeout: 60 * 6 * 1000,
                        success: function (result) {
                            layer.close(loadIndex);
                            layer.msg(result.Message,
                                { time: 1000 },
                                function () {
                                    if (result.Code === 200) {
                                        mainList();
                                    }
                                });
                        }
                    });
                    //$.post("/DbBackup/Backup",
                    //    function (result) {
                    //        layer.close(loadIndex);
                    //        layer.msg(result.Message,
                    //            { time: 1000 },
                    //            function() {
                    //                if (result.Code === 200) {
                    //                    mainList();
                    //                }
                    //            });
                    //    },
                    //    "json");

                    layer.close(index);
                });
        },
        btnRestore: function () {
            var checkStatus = table.checkStatus('mainList')
                , data = checkStatus.data;
            if (data.length != 1) {
                layer.msg("请选择要还原的数据库备份文件，且同时只能选择一个数据库备份文件!");
                return;
            }

            layer.confirm('您确定现在要还原系统的数据库吗?<br /><span style="color: red;font-weight: bold;">注意: </span>还原后, 备份之后的添加和修改的数据将丢失!',
                function (index) {
                    var loadIndex = layer.load();
                    $.ajax({
                        url: "/DbRestore/Restore",
                        data: {
                            backupId: data[0].Id
                        },
                        type: "POST",
                        dataType: "json",
                        timeout: 60 * 6 * 1000,
                        success: function (result) {
                            layer.close(loadIndex);
                            layer.msg(result.Message,
                                { time: 1000 },
                                function () {
                                    if (result.Code === 200) {
                                        mainList();
                                    }
                                });
                        }
                    });

                    //$.post("/DbRestore/Restore",
                    //    {
                    //        backupId: data[0].Id
                    //    },
                    //    function (result) {
                    //        layer.close(loadIndex);
                    //        layer.msg(result.Message,
                    //            { time: 1000 },
                    //            function () {
                    //                if (result.Code === 200) {
                    //                    mainList();
                    //                }
                    //            });
                    //    },
                    //    "json");

                    layer.close(index);
                });
        },
        btnDel: function () {      //批量删除
            var checkStatus = table.checkStatus('mainList')
                , data = checkStatus.data;
            openauth.del("/DbBackup/Delete",
                data.map(function (e) { return e.Id; }),
                mainList);
        },
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