var callback = {
    
};
$(function () {
    //$(document).on("click", "a[planid]", function (e) {
    //    e.preventDefault();
    //    var planid = $(this).attr("planid");
    //});
    $("#searchform").on("submit", function () {
        getList(1,10);
        return false;
    });

    //$("#keyword").on("input", function () {
    //    getList(1, 10);
    //})
    function getList(page, size) {
        $("#list").html("");
        var postdata = $$.getFormData("#searchform");
        $.extend(postdata, {
            page: page,
            size: size,
        });
        $$.common.getPageList.req(postdata).then(function (json) {
            $(".pager").setPager(page, size, json.count, function (p) {
                getList(p, size);
            });
            if (json.data.length <= 0) {
                $("#list").html($("#emptytemplete").html());
                return;
            }
            $.each(json.data, function () {
                let item = $("#listtemplete").html().combineObject(this);
                
                $("#list").append(item);
            })
        });
    }
    getList(1,10);
});