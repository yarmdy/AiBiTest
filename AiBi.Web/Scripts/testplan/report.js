var plan;
var tree;
var ztree;
layui.config({
    base: "/js/"
}).use(['table', 'ztree'], async function () {
    const table = layui.table;
    
    function bingChartInit(){
        var chartDom = document.getElementById('bingChart');
        var myChart = echarts.init(chartDom);
        var option;

        option = {
            title: {
                text: '测试完成情况',
                subtext: '学员都在干什么',
                left: 'center'
            },
            tooltip: {
                trigger: 'item'
            },
            legend: {
                orient: 'vertical',
                left: 'left'
            },
            series: [
                {
                    name: '所占比例',
                    type: 'pie',
                    radius: '50%',
                    data: [
                        { value: plan.BusTestPlanUsers.sum(function (i, a) { return a.Status == 4?1:0; }), name: '已完成' },
                        { value: plan.BusTestPlanUsers.sum(function (i, a) { return a.Status != 4 && a.Status != 0?1:0; }), name: '正在测试' },
                        { value: plan.BusTestPlanUsers.sum(function (i, a) { return a.Status == 0?1:0; }), name: '未开始' }
                    ],
                    emphasis: {
                        itemStyle: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };

        option && myChart.setOption(option);
    }
    function tiaoChartInit() {
        var chartDom = document.getElementById('tiaoChart');
        var myChart = echarts.init(chartDom);
        var option;

        var chartData = [['score', '答题数', '姓名']]
        chartData = chartData.concat(plan.BusTestPlanUsers.map(function (a, i) {
            return [i+1, a.FinishQuestion, i+1+"."+a.User.BusUserInfoUsers[0].RealName || a.User.Name];
        }).reverse().slice(0, 10));

        
        option = {
            dataset: {
                source: chartData
            },
            grid: {
                containLabel: true,
                top: "0%",
                left: "0%",
                right: "10%",
                bottom:"10%"
            },
            xAxis: { name: '答题数', max :plan.Template.QuestionNum},
            yAxis: {
                type: 'category',
                axisTick: {
                    interval: 0
                },
                axisLabel: {
                    show: true,
                    interval: 0,
                    showMinLabel: true,
                    showMaxLabel: true,
                    hideOverlap: false,
                }
            },
            visualMap: {
                orient: 'horizontal',
                left: 'center',
                min: 1,
                max: 10,
                text: ['低名次', '高名次'],
                // Map the score column to color
                dimension: 0,
                inRange: {
                    color: ['#65B581', '#FFCE34', '#FD665F']
                }
            },
            series: [
                {
                    type: 'bar',
                    encode: {
                        // Map the "amount" column to X axis.
                        x: '答题数',
                        // Map the "product" column to Y axis
                        y: '姓名'
                    }
                }
            ],
            label: {
                show: true,
                position: 'right'
            },
        };

        option && myChart.setOption(option);
    }
    function tableInit() {
        const cols = [[
            {
                field: 'Name', title: '姓名', templet: function (d) {
                    return d.User.BusUserInfoUsers[0].RealName || d.User.Name;
                }
            },
            {
                field: 'Answer', title: '答题情况', templet: "#progressTemplate"
            },
            {
                field: 'Score', title: '得分情况'
            },
            {
                field: 'ResultCode', title: '结果代码'
            },
            {
                field: 'Actions', title: '操作', fixed: "right", templet: "#actionTemplate"
            }

        ]];

        table.render({
            elem: '#table',
            data: plan.BusTestPlanUsers,
            cols: cols,
            height: "full-150",
            size: "sm",
            done: function (res, curr, count, origin) {
                layui.element.render('progress')
            },
        });
    }
    $("#searchForm").on("submit", function () {
        filterTable();
        return false;
    });
    $("iframe[name=downframe]").on("load error", function (e) {
        layer.closeAll("loading");
        var errorp = this.contentWindow.document.querySelector("p.empty-subtitle.text-secondary");
        if (errorp) {
            layer.error(errorp.innerHTML);
        }
    });
    layui.util.on("lay-on", {
        export: function () {
            var data = filterTable().map(function (a) {
                return a.UserId;
            });
            if (data.length <= 0) {
                layer.error("没有任何数据要导出");
                return;
            }
            $("#UserIds").html("");
            data.forEach(function (a, i) {
                $("#UserIds").append('<input name="UserIds[' + i + ']" value="' + a + '" />');
            });
            $("#downform").submit();
            layer.load(2);
        }
    });
    function filterTable() {
        var data = plan.BusTestPlanUsers.filter(function (a) {
            var keyword = $("#keyword").val().toLowerCase();
            var res = (a.User.BusUserInfoUsers[0].RealName + "").toLowerCase().indexOf(keyword) >= 0 || (a.User.Name + "").toLowerCase().indexOf(keyword) >= 0;
            var sel = ztree.getSelectedNodes();
            if (sel.length>0 && sel[0].Id) {
                var arr = [sel[0].Id];
                if (sel[0].ObjectTag) {
                    arr = arr.concat(sel[0].ObjectTag);
                }
                res = res & (arr.indexOf(a.User.BusUserInfoUsers[0].GroupId) >= 0);
            }
            return res;
        });
        table.reloadData("table",{
            data: data
        });
        return data;
    }
    async function init() {
        let json = await $$.get("/TestPlan/GetReport/"+PageInfo.KeyValueStr, {});
        plan = json.data;
        tree = json.data2;
        
        bingChartInit();
        //tiaoChartInit();
        tableInit();
    }
    

    function treeInit() {
        //$("#tree").height($(document).outerHeight() - 125 - 12);
        var setting = {
            data: {
                key: {
                    name: "Name",
                    children:"Children"
                },
                simpleData: {
                    enable: false,
                    idKey: "Id",
                    pIdKey: "ParentId",
                }
            },
            view: {
                selectedMulti: false
            },
            callback: {
                onClick: function (event, treeId, treeNode, clickFlag) {
                    $("#tree").hide(100);
                    //选中
                    if (treeNode.Id == null) {
                        $("[name=GroupName]").val("");
                        return;
                    }
                    $("[name=GroupName]").val(treeNode.Name);
                },

            },
            edit: {
                enable: false
            }
        };

        $("[name=GroupName]").on("focus click", function (e) {
            //var offset = $("#tree").closest(".layui-card-body").offset();
            var offset = {top:0,left:0};
            $("#tree").css({
                top: $(this).offset().top - offset.top + $(this).outerHeight(),
                left: $(this).offset().left - offset.left
            }).show(100);
        });
        $(document).on("click.tree", function () {
            $("#tree").hide(100);
        });
        $("#tree,[name=GroupName]").on("click", function (e) {
            e.stopPropagation();
        });

        var nodes = [{ Name: "全部", isParent: true, Id: null, ParentId: null, Children: tree, expandFlag: true }];
        

        var ztreeObj = $.fn.zTree.init($("#tree"), setting, nodes);
        ztreeObj.expandAll(true);
        return ztreeObj;
    }
    await init();
    ztree = treeInit();
});
