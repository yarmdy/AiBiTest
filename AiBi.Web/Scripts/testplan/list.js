var callback = {
    testplanadd: function (id, obj) {
        layer.loadEx(0);
        console.log(id,obj);
    }
};
$(function () {
    //$(document).on("click", "a[planid]", function (e) {
    //    e.preventDefault();
    //    var planid = $(this).attr("planid");
    //});
    $("#searchform").on("submit", function () {
        getPlan(1,10);
        return false;
    });

    //$("#keyword").on("input", function () {
    //    getPlan(1, 10);
    //})
    function getPlan(page, size) {
        $("#list").html("");
        var postdata = $$.getFormData("#searchform");
        $.extend(postdata, {
            page: page,
            size: size,
        });
        $$.common.getPageList.req(postdata).then(function (json) {
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