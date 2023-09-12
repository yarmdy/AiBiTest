$(function () {
    function getDetail() {
        function render(str) {
            $("#detail").html(str);
        }
        if (PageInfo.KeyValueStr) {
            $$.common.getDetail.req().then(function (json) {
                json.data.json = json;
                let item = $("#detailtemplate").text().combineObject(json.data);
                render(item);
            });
        } else {
            render($("#emptytemplate").text());
        }
    }
    callback.templateSelectOk = function (obj) {
        $("#detail [name=TemplateId]").val(obj.Id);
        $("#detail [name=TemplateName]").val(obj.Title);
    }
    $(document).on("click.selecttemplate", ".selecttemplate", function () {
        $$.addTab("选择模板","/TestTemplate/MySelect");
    })
    

    getDetail();
});