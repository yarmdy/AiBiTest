
$(function () {
    //$(document).on("click", "a[planid]", function (e) {
    //    e.preventDefault();
    //    var planid = $(this).attr("planid");
    //});
    $("#searchform").on("submit", function () {
        getList(1,10);
        return false;
    });

    $("#btnSelect").on("click", function (e) {
        e.preventDefault();
        $$.callback("userinfoSelectOk", {});
        $$.closeThis();
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
            $(".selectall").prop("checked", false);
            $$.selectAll(".selectall", ".selectsingle");
            if (json.data.length <= 0) {
                $("#list").html($("#emptytemplate").html());
                return;
            }
            $.each(json.data, function () {
                let item = $("#listtemplate").html().combineObject(this);
                
                $("#list").append(item);
            })
        });
    }
    $$.selectAll(".selectall", ".selectsingle", function (data) {
        console.log(data);
    });
    getList(1,10);
});