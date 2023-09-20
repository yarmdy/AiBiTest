layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;

    var upload = layui.upload;

    // 渲染
    upload.render({
        elem: '#btnUpload',
        url: '/Attachment/Add', // 此处配置你自己的上传接口即可
        data: {
            module:200
        },
        size: 4096, // 限制文件大小，单位 KB
        done: function (json) {
            $("#ImageFullName").attr("src",json.data.FullName);
            $("input[name=ImageId]").val(json.data.Id);
        }
    });
    function initValidate() {
        $("#form").validate({
            submitHandler: function (form) {
                var postData = $$.getFormData("#form");
                var callbackstr;
                var addoreditFunc = PageInfo.KeyValueStr ? (callbackstr = "exampleeditok", $$.common.edit.req) : (callbackstr = "exampleaddok", $$.common.add.req);
                addoreditFunc(postData).then(function (json) {
                    
                    $$.callback(callbackstr, json);
                    $$.closeThis();
                    
                });
            },
            rules: {
                Title: {
                    required: true
                },
                ClassifyId: {
                    required: true
                },
                Duration: {
                    required: true
                },
                Note: {
                    required: true
                },
                
                
            },
            messages: {
                Title: {
                    required: "请输入标题"
                },
                ClassifyId: {
                    required: "请选择分类"
                },
                Duration: {
                    required: "请输入时长"
                },
                Note: {
                    required: "请输入注意事项"
                },
            }
        });
    }

    function getClassifies() {
        return $$.get("/Classify" + $$.common.getPageList.url,{ where: { ParentId: null }, page: 1, size: 1000 }).then(function (json) {
            var template = '<option value="{{Id}}">{{Name}}</option>';
            json.data.forEach(function (a) {
                $("select[name=ClassifyId]").append(template.combineObject(a));
            });
            form.render();
        });
    }
    function getSubClassifies(parentid,thisid) {
        if (!parentid) {
            $("select[name=SubClassifyId] option").not(":first").remove();
            form.render();
            $("select[name=SubClassifyId]").val("");
            return true;
        }
        return $$.get("/Classify" + $$.common.getPageList.url, { where: { ParentId: parentid }, page: 1, size: 1000 }).then(function (json) {
            var template = '<option value="{{Id}}">{{Name}}</option>';
            $("select[name=SubClassifyId] option").not(":first").remove();
            json.data.forEach(function (a) {
                $("select[name=SubClassifyId]").append(template.combineObject(a));
            });
            $("select[name=SubClassifyId]").val(thisid);
            form.render();
            return json;
        });
    }
    form.on("select(ClassifyId)", function (e) {
        getSubClassifies(e.value);
    });
    var defClassify = getClassifies();
    function renderDetail() {
        return $$.common.getDetail.req().then(function (json) {
            $$.setFormData("#form",json.data);
            form.render();
            if (json.data.Image) {
                $("#ImageFullName").attr("src", json.data.Image.FullName);
                $("input[name=ImageId]").val(json.data.Image.Id);
            }
            return json;
        });
    }

    if (PageInfo.KeyValueStr) {
        
        $.when(renderDetail(), defClassify).then(function (json) {
            $("select[name=ClassifyId]").val(json.data.ClassifyId);
            return getSubClassifies(json.data.ClassifyId, json.data.SubClassifyId);
        })
        
    }
    initValidate();
});
