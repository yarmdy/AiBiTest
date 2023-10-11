layui.config({
    base: "/js/"
}).use(['form',  'upload', 'layer', 'jquery'], function() {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery,
        upload = layui.upload;

    var uploadSystemLogo = upload.render({
        elem: '#btnUploadSystemLogo',
        url: '/SystemConfig/UploadSystemLogo/',
        accept: 'file',
        exts: 'jpg|png',
        size: 4096,
        done: function (res) {
            if (res.Code === 200) {
                $("#SystemLogoPath").val(res.FilePath);
                $("#SystemLogoName").val(res.FileName);
                $("#SystemLogoLink").html(res.FileName);
                $("#SystemLogoLink").attr("href", res.FilePath);
                $("#SystemLogoLink").show();
            } else {
                layer.msg(res.Message);
            }
        },
        error: function () {
            layer.msg("上传失败");
        }
    });

    var uploadSystemIcon = upload.render({
        elem: '#btnUploadSystemIcon',
        url: '/SystemConfig/UploadSystemIcon/',
        accept: 'file',
        exts: 'ico',
        size: 4096,
        done: function (res) {
            if (res.Code === 200) {
                $("#SystemIconPath").val(res.FilePath);
                $("#SystemIconName").val(res.FileName);
                $("#SystemIconLink").html(res.FileName);
                $("#SystemIconLink").attr("href", res.FilePath);
                $("#SystemIconLink").show();
            } else {
                layer.msg(res.Message);
            }
        },
        error: function () {
            layer.msg("上传失败");
        }
    });

    var vm = new Vue({
        el: "#frmSystemConfig"
    });

    var getSystemConfig = function () {
        $.post("/SystemConfig/GetSystemConfig",
            function (data) {
                if (data.Code == 200) {
                    vm.$set('$data', data.Result);
                }
            },
            "json");
    };

    getSystemConfig();

    form.verify({
        SystemName: function (value, item) {
            if (value != undefined &&
                value != null &&
                $.trim(value) != "" &&
                value.length > 20) {
                return "系统名称的长度不能超过20个字符";
            }
        },
        Copyright: function (value, item) {
            if (value != undefined &&
                value != null &&
                $.trim(value) != "" &&
                value.length > 50) {
                return "系统名称的长度不能超过50个字符";
            }
        }
    });

    //提交数据
    form.on('submit(formSubmitSystemConfig)',
        function (data) {
            $.post("/SystemConfig/Save",
                data.field,
                function (data) {
                    layer.msg(data.Message, { time: 1000 }, function () {
                        if (data.Code === 200) {
                            getSystemConfig();
                        }
                    });
                },
                "json");
            return false;
        });
});