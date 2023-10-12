var plan;
layui.config({
    base: "/js/"
}).use(['table'], async function () {
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

        for (var i = 2; i < 11; i++) {
            chartData.push([i, Math.abs(22 - i), "学员" + (i)]);
        }
        
        
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
            //height: "full-0",
            size: "sm",
            done: function (res, curr, count, origin) {
                layui.element.render('progress')
            },
        });
    }
    async function init() {
        let json = await $$.get("/TestPlan/GetReport/"+PageInfo.KeyValueStr, {});
        plan = json.data;
        
        bingChartInit();
        tiaoChartInit();
        tableInit();
    }
    init();
});
