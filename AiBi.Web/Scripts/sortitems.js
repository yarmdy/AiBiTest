var SortItems = function () {
    var localdiv;
    var fly;
    var x, y;
    var sx, sy;
    var ex, ey;
    var lx, ly;
    function init(data) {
        $(document).off("mousedown.SortItems mouseup.SortItems mousemove.SortItems ");
        function scrollmoveevent(e) {
            e.stopPropagation();
            if (!localdiv) return;
            //console.log(e);
            var nx = e.clientX || lx; var ny = e.clientY || ly;
            lx = nx; ly = ny;
            var nsx = $(".divSortitems").parent().scrollLeft(); var nsy = $(".divSortitems").parent().scrollTop();

            var ox = nx + nsx - x - sx;
            var oy = ny + nsy - y - sy;

            //fly.css({ left: ox + ex, top: oy + ey });
            fly.css({ top: oy + ey });

            fly.siblings("[dataid]").each(function (i, a) {
                if (a == localdiv) return true;
                var top = $(localdiv).position().top;
                var ttop = $(a).position().top;
                var ftop = fly.position().top;
                if (ftop > ttop && top <= ttop) {
                    $(a).after(localdiv);
                } else if (ftop < ttop && top >= ttop) {
                    $(localdiv).after(a);
                }
            });
        }
        $(".divSortitems").parent().off("scroll.SortItems").on("scroll.SortItems", scrollmoveevent);
        $(document).on("mousedown.SortItems", ".divSortitems .layui-card", function (e) {
            e.stopPropagation();
            var $this = $(this);
            if ($this.hasClass("fly")) {
                return;
            }
            localdiv = this;
            fly = $this.clone();
            fly.addClass("fly");
            ex = $this.position().left;
            ey = $this.position().top;
            fly.css({ position: "absolute", left: ex, top: ey, width: $this.width(), height: $this.height });
            $(".divSortitems").append(fly);
            setTimeout(function () {
                fly.find(".layui-card-body").removeClass("layui-bg-blue").addClass("layui-bg-orange");
            })

            lx=x = e.clientX; ly=y = e.clientY;
            sx = $(".divSortitems").parent().scrollLeft();
            sy = $(".divSortitems").parent().scrollTop();

            $this.css({ "opacity": 0 }).find(".layui-card-body").removeClass("layui-bg-blue").addClass("layui-bg-orange");

            //console.log(x, y, sx, sy, ex, ey);
        }).on("mouseup.SortItems", function () {
            if (!localdiv) return;
            var $fly = fly;
            fly = null;
            var $this = $(localdiv);
            localdiv = null;

            $fly.animate({
                left: $this.position().left,
                top: $this.position().top
            }, 200, function () {
                $fly.remove();
                $this.css("opacity", 1).find(".layui-card-body").addClass("layui-bg-blue").removeClass("layui-bg-orange");
            });
        }).on("mousemove.SortItems", scrollmoveevent);
    }
    function Sort(data) {
        var def = $.Deferred();
        var div = $($("#divSortitemsTemplate").html());
        if (data == null) {
            data = [];
        }
        data.forEach(function (a) {
            var ele = layui.laytpl($("#divSortitemsItemTemplate").html()).render(a);
            div.append(ele);
        });
        layer2.open({
            type: 1, content: div[0].outerHTML, area: ["50%", Math.min($(window).height(), 800) + "px"], title: "排序（<span style='color:red'>拖动以排序</span>）", btn: ["确定", "取消"], btn1: function (r) {
                layer2.close(r);
                def.resolve($(".divSortitems [dataid]").map(function (i,a) {
                    return $(a).attr("dataid");
                }).toArray());
            }, btn2: function () {
                def.reject();
            }
        });
        init(data);
        return def.promise();
    }
    return {
        sort: Sort
    }
}();