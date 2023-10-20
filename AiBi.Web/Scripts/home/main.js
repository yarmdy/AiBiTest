layui.config({
    base: "/js/"
}).use(['table'], async function () {
    callback.testover = function (json) {
        getList();
    }
    let pager = {
        page: 1,
        size:9,
    }
    function getList(page, size) {
        let data = $.extend({}, pager);
        data.page = page || data.page;
        data.size = size || data.size;
        $$.post("/TestPlan/GetMyList", data).then(function (json) {
            var html;
            if (json.data.length <= 0) {

                html = layui.laytpl($("#emptyTemplate").html()).render(json.data);
            } else {
                html = layui.laytpl($("#itemTemplate").html()).render(json.data);
            }
            $("#page").html(html);
        });
    }
    getList();
});
