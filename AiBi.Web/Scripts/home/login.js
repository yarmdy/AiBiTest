

layui.use(function () {
    $("#showpwdtag").on("click", function (e) {
        e.preventDefault();
        var ele = $("input[name=password]");
        var type = ele.attr("type").toLowerCase() == "password" ? "text" : "password";
        ele.attr("type", type).next("span.input-group-text").hide();
    });

    if ($("input[name=Error]").val()) {
        layer.error($("input[name=Error]").val());
    }
});