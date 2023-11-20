layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;

    var upload = layui.upload;

    callback.exampleselectok = function (data) {
        var old = table.cache.table_example;
        data.forEach(function (a) {
            if (old.findIndex(function (b) { return b.Id == a.Id; }) >= 0) {
                return;
            }
            a.Enabled = true;
            old.push(a);
        });
        setTimeout(function () {
            table.reloadData("table_example", {
                data: old
            });
        });
    }
    function initValidate() {
        $("#form").validate({
            submitHandler: function (form) {
                
                var postData = $$.getFormData("#form");
                postData.BusTestTemplateExamples = table.cache.table_example.map(function (a, i) { return a.SortNo = (i + 1), a.ExampleId = a.Id, a.Classify = null, a.SubClassify = null, a; });
                var callbackstr;
                var addoreditFunc = PageInfo.KeyValueStr ? (callbackstr = "templateeditok", $$.common.edit.req) : (callbackstr = "templateaddok", $$.common.add.req);
                addoreditFunc(postData).then(function (json) {

                    $$.callback(callbackstr, json);
                    $$.closeThis();

                });
                return false;
            },
            rules: {
                Title: {
                    required: true
                },
                NContent: {
                    required: true
                },
                Examples: {
                    required: true
                },

            },
            messages: {
                Title: {
                    required: "请输入标题"
                },
                NContent: {
                    required: "请输入摘要"
                },
                Examples: {
                    required: "请选择量表"
                },
            }
        });
    }
    function renderDetail() {
        return $$.common.getDetail.req().then(function (json) {
            $$.setFormData("#form", json.data);

            if (json.data.Image) {
                $("#form [name=ImageFullName]").attr("src", json.data.Image.FullName);
                $("#form input[name=ImageId]").val(json.data.Image.Id);
            }
            table.reloadData("table_example", {
                data: json.data.BusTestTemplateExamples.map(function (a) { return $.extend(a.Example, a),delete a.Example.Example, a.Example; })
            });
            form.render();
            return json;
        });
    }

    layui.util.on("lay-on", {
        sorttemp: async function (a) {
            var arr = table.cache.table_example.map(function (a) {
                return { id: a.Id, name: a.Title };
            });
            var res = await SortItems.sort(arr);
            var data = res.map(function (a,i) {
                var obj = table.cache.table_example.find(function (b) {
                    return b.Id == a;
                });
                obj.SortNo = (i + 1);
                return obj;
            });
            table.reloadData("table_example", {
                data: data
            });
        }
    });
    function initUpload(elem, module = 200) {
        $(elem).each(function (i, a) {
            upload.render({
                elem: $(a),
                url: '/Attachment/Add', // 此处配置你自己的上传接口即可
                data: {
                    module: module
                },
                size: 4096, // 限制文件大小，单位 KB
                done: function (json) {

                    this.elem.closest("div").find("[name=ImageFullName]").attr("src", json.data.FullName);
                    this.elem.closest("div").find("input[name=ImageId]").val(json.data.Id);
                }
            });
        });

    }
    
    initUpload($("#btnUpload"));

    $("#btnsave").on("click", function () {
        $("#form").submit();
    });
    $("[name=Examples]").on("focus", function () {
        $$.addTab("选择量表","/Example/Select");
    });


    let cols = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'Title', title: '标题' },
        {
            field: 'Enabled', title: '默认启用', templet: function (d) {
                return '<input type="checkbox" name="Enabled" value="'+d.Id+'" title="启用|禁用" lay-skin="switch" lay-filter="changeEnabled" '+(d.Enabled?"checked":"")+' />';
            }
        },
        { field: 'Duration', title: '时长(分钟)' },
        { field: 'QuestionNum', title: '问题数' },
        { field: 'Action', title: '操作', templet: function (d) { return '<button type="button" class="layui-btn layui-btn-xs" lay-event="delete">删除</button>'} },
    ]];
    table.render({
        elem: '#table_example',
        data: [],
        cols: cols,
        //height: 155,
        size:"sm"
    });
    table.on("tool(table_example)", function (e) {
        table.reloadData("table_example", {
            data: table.cache.table_example.filter(function (a) { return a.Id != e.data.Id })
        });
    });
    form.on("switch(changeEnabled)", function (e) {
        var checked = e.elem.checked;
        var id = e.value;
        table.cache.table_example.find(a => a.Id == id).Enabled = checked;
        table.reloadData("table_example", { data: table.cache.table_example });
    });
    $("#btnDeleteExample").on("click", function () {
        table.reloadData("table_example", {
            data: table.cache.table_example.filter(function (a) { return !a.LAY_CHECKED; })
        });
    });
    $(document)
        .on("click", "[name=ImageFullName]", function () {
            var src = $(this).attr("src");
            if (!src) return;

            layer.image("图标", src);
        });
    if (PageInfo.KeyValueStr) {

        renderDetail();

    }
    initValidate();
});
