layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'element', 'table'], async function () {
    const table = layui.table;
    const form = layui.form;

    function initValidate() {
        $("#form").validate({
            submitHandler: function (form) {
                var postData = $$.getFormData("#form");
                var callbackstr;
                var addoreditFunc = PageInfo.KeyValueStr ? (callbackstr = "classifyeditok", $$.common.edit.req) : (callbackstr = "classifyaddok", $$.common.add.req);
                addoreditFunc(postData).then(function (json) {
                    
                    $$.callback(callbackstr, json);
                    $$.closeThis();
                    
                });
            },
            rules: {
                Name: {
                    required: true
                },
                SortNo: {
                    required: true
                },
                
            },
            messages: {
                Name: {
                    required: "请输入名称"
                },
                SortNo: {
                    required: "请输入排序号"
                },
            }
        });
    }
    function getParents() {
        return $$.common.getPageList.req({ where: { ParentId: null, NotParentId: (PageInfo.KeyValueStr || null) }, page: 1, size: 1000 }).then(function (json) {
            var template = '<option value="{{Id}}">{{Name}}</option>';
            json.data.forEach(function (a) {
                $("select[name=ParentId]").append(template.combineObject(a));
            });
            form.render();
        });
    }
    function renderDetail() {
        $$.common.getDetail.req().then(function (json) {
            $$.setFormData("#form",json.data);
            form.render();
        });
    }

    if (PageInfo.KeyValueStr) {
        $.when(getParents()).then(function () {
            renderDetail(PageInfo.KeyValueStr);
        });

    } else {
        getParents();
    }
    initValidate();
});
