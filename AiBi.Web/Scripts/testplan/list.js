$(function () {
    //$(document).on("click", "a[planid]", function (e) {
    //    e.preventDefault();
    //    var planid = $(this).attr("planid");
    //});
    function getPlan(page, size) {
        $("#list").html("");
        $$.common.getPageList.req({
            page: page,
            size: size,
            keyword: $("#keyword").hval()
        }).then(function (json) {
            $(".pager").setPager(page, size, json.count, function (p) {
                getPlan(p, size);
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
    getPlan(1,10);
});