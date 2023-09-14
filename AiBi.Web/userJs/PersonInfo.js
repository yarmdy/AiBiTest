var PersonInfo;
var personInfoLayerIndex;

layui.config({
    base: "/js/"
}).use(['form', 'vue', 'ztree', 'layer', 'jquery', 'openauth', 'utils'], function() {
    var layer = layui.layer,
        $ = layui.jquery;

    PersonInfo = function() {
        personInfoLayerIndex = layer.open({
            title: "个人信息",
            area: ["400px", "400px"],
            type: 1,
            content: $("#divPersonInfo"),
            success: function() {
            },
            end: function() {
            }
        });
    };
});