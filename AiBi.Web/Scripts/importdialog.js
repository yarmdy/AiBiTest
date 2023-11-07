var ImportDialog = function () {
    var layui = null;
    var importDlg = null;
    var loadLayer;
    function init(lui) {
        layui = lui;
        upload = layui.upload;
        var uploadImportFile = upload.render({
            elem: '#btnUploadImportFile',
            auto: false,
            url: ImportDialogModel.UploadUrl,
            accept: ImportDialogModel.Accept,
            exts: ImportDialogModel.Exts,
            bindAction: "#formImport",
            before: function () {
                loadLayer = layer.loadEx();
            },
            done: function (res) {
                layer.close(loadLayer);
                if (res.code <0) {
                    layer.error(res.msg);
                    return;
                }
                callback && callback.uploadSuccess && callback.uploadSuccess(res);
                importDlg.Close();
            },
            error: function () {
                layer.close(loadLayer);
                layer.error("上传失败");
            },
            data: ImportDialogModel.ParamList
        });
        //导入对话框
        importDlg = function () {
            var title = "";
            var index;
            var show = function () {
                index = layui.layer.open({
                    title: title,
                    area: ["500px", "340px"],
                    type: 1,
                    content: $('#divImport'),
                    success: function () {
                        $("a#ImportFileLink").attr("href", "");
                        $("a#ImportFileLink").html("");
                        $("input#hidImportFilePath").val("");
                        uploadImportFile.reload();
                    },
                    end: function () {
                    }
                });

            };

            return {
                Import: function () {
                    title = ImportDialogModel.Name;
                    show();
                },
                Close: function () {
                    layui.layer.close(index);
                }
            };
        }();
    }
    function show() {
        if (!importDlg) {
            return;
        }
        importDlg.Import();
    }
    return {
        init:init,
        show:show
    }
}();