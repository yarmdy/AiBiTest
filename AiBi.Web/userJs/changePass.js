var ChangePassword;
var changePasswordLayerIndex;

layui.config({
    base: "/js/"
}).use(['form',  'ztree', 'layer', 'jquery', 'openauth', 'utils'], function () {
    var layer = layui.layer,
        $ = layui.jquery;

    ChangePassword = function () {
        changePasswordLayerIndex = layer.open({
            title: "修改密码",
            area: ["400px", "330px"],
            type: 1,
            content: $("#divChangePassword"),
            success: function () {
            },
            end: function () {
            }
        });
    };

    $("#btnChangePassword").bind("click", function () {
        if ($("#oldPass").val() === "") {
            layer.alert("旧密码不能为空", { title: '提示', shadeClose: true });
            return;
        }
        if ($("#newPass").val() === "") {
            layer.alert("新密码不能为空", { title: '提示', shadeClose: true });
            return;
        }
        if ($("#checkPass").val() === "") {
            layer.alert("确认密码不能为空", { title: '提示', shadeClose: true });
            return;
        }
        if ($("#newPass").val() !== $("#checkPass").val()) {
            layer.alert("两次输入的密码不一致", { title: '提示', shadeClose: true });
            return;
        }
        $.ajax({
            url: "/User/ChangePassword",
            data: { oldPassword: $("#oldPass").val(), password: $("#newPass").val() },
            type: 'post',
            dataType: 'json',
            success: function (result) {
                layer.msg(result.msg, { time: 500 }, function () {
                    if (result.code == 200) {
                        layer.close(changePasswordLayerIndex);
                    }
                });
            }
        });
    });
});