var BuildTopNavBar;
var BuildParentModuleLink;
var BuildLeafModuleLink;
var LoadSubModule;
var LoadCurrentModule;
var LoadSubModuleById;
var LoadCurrentModuleById;
var BuildLeftNavBar;

layui.config({
    base: "/js/"
}).use(['element', 'jquery'], function () {
    var element = layui.element,
        $ = layui.jquery;
    BuildTopNavBar = function (strData) {
        var data;
        if (typeof(strData) == "string") {
            data = JSON.parse(strData); //部分用户解析出来的是字符串，转换一下
        }
        else {
            data = strData;
        }

        var ulHtml = '<ul class="layui-nav">';
        var hasData = data != undefined &&
                      data != null &&
                      data.length > 0;

        if (hasData) {
            for (var i = 0; i < data.length; i++) {
                var hasItem = data[i] != undefined &&
                              data[i] != null &&
                              data[i].Item != undefined &&
                              data[i].Item != null;

                if (!hasItem) {
                    continue;
                }

                if (data[i].Item.Id == $("input#hidTopModuleId").val()) {
                    ulHtml += '<li class="layui-nav-item layui-this">';
                }
                else {
                    ulHtml += '<li class="layui-nav-item">';
                }

                var hasChildren = data[i].Children != undefined &&
                                  data[i].Children != null &&
                                  data[i].Children.length > 0;

                var hasUrl = data[i].Item.Url != undefined &&
                             data[i].Item.Url != null &&
                             $.trim(data[i].Item.Url) != "";

                if (hasChildren ||
                    (!hasChildren &&
                        (!hasUrl || (hasUrl && $.trim(data[i].Item.Url) == "/")))) {
                    ulHtml += BuildParentModuleLink(data[i].Item, false, false);

                    if (hasChildren) {
                        ulHtml += '<dl class="layui-nav-child">';

                        for (var j = 0; j < data[i].Children.length; j++) {
                            hasItem = data[i].Children[j] != undefined &&
                                      data[i].Children[j] != null &&
                                      data[i].Children[j].Item != undefined &&
                                      data[i].Children[j].Item != null;

                            if (!hasItem) {
                                continue;
                            }

                            var isCurrentModule = false;
                            if (data[i].Children[j].Item.Id == $("input#hidTopModuleId").val()) {
                                ulHtml += '<dd class="layui-this">';
                                isCurrentModule = true;
                            }
                            else {
                                ulHtml += '<dd>';
                                isCurrentModule = false;
                            }

                            hasChildren = data[i].Children[j].Children != undefined &&
                                          data[i].Children[j].Children != null &&
                                          data[i].Children[j].Children.length > 0;

                            hasUrl = data[i].Children[j].Item.Url != undefined &&
                                     data[i].Children[j].Item.Url != null &&
                                     $.trim(data[i].Children[j].Item.Url) != "";

                            if (hasChildren ||
                                (!hasChildren &&
                                    (!hasUrl || (hasUrl && $.trim(data[i].Children[j].Item.Url) == "/")))) {
                                ulHtml += BuildParentModuleLink(data[i].Children[j].Item, true, false);
                                if (isCurrentModule) {
                                    LoadSubModuleById(data[i].Children[j].Item.Id);
                                }
                            }
                            else {
                                ulHtml += BuildLeafModuleLink(data[i].Children[j].Item, true);
                                if (isCurrentModule) {
                                    LoadCurrentModuleById(data[i].Children[j].Item.Id);
                                }
                            }

                            ulHtml += '</dd>';
                        }

                        ulHtml += '</dl>';
                    }
                }
                else {
                    ulHtml += BuildLeafModuleLink(data[i].Item, true);
                }

                ulHtml += '</li>';
            }
        }

        ulHtml += '</ul>';
        return ulHtml;
    };

    //构造父模块超链接HTML
    BuildParentModuleLink = function (item, isAddClickEvent, isLeftNavBar) {
        var linkHtml = '';

        if (isAddClickEvent) {
            linkHtml += '<a href="javascript: void(0);" ModuleId="' + item.Id + '" onclick="LoadSubModule(this);">';
        }
        else {
            linkHtml += '<a href="javascript: void(0);" ModuleId="' + item.Id + '">';
        }

        var hasIcon = item.IconName != undefined &&
                      item.IconName != null &&
                      $.trim(item.IconName) != "";

        if (hasIcon) {
            if ($.trim(item.IconName).indexOf("icon-") != -1) {
                linkHtml += '<i class="iconfont ' + $.trim(item.IconName) + '" data-icon="' + $.trim(item.IconName) + '"></i>';
            }
            else {
                linkHtml += '<i class="layui-icon" data-icon="' + $.trim(item.IconName) + '">' + $.trim(item.IconName) + '</i>';
            }
        }

        linkHtml += '<cite>' + $.trim(item.Name) + '</cite>';

        if (isLeftNavBar) {
            linkHtml += '<span class="layui-nav-more"></span>';
        }

        linkHtml += '</a>';

        return linkHtml;
    };

    //构造叶子模块超链接HTML
    BuildLeafModuleLink = function (item, isAddClickEvent) {
        var strUrl = $.trim(item.Url);

        //if (strUrl.indexOf("?") > 0) {
        //    strUrl += "&moduleId=" + escape(item.Id);
        //}
        //else {
        //    strUrl += "?moduleId=" + escape(item.Id);
        //}

        var linkHtml = '';

        if (isAddClickEvent) {
            linkHtml += '<a href="' + strUrl + '" title="' + $.trim(item.Name) +'" target="addtab" data-url="' + strUrl + '" ModuleId="' + item.Id + '" onclick="LoadCurrentModule(this);">';
        }
        else {
            linkHtml += '<a href="' + strUrl + '" title="' + $.trim(item.Name) +'" target="addtab" data-url="' + strUrl + '" ModuleId="' + item.Id + '">';
        }

        var hasIcon = item.IconName != undefined &&
                      item.IconName != null &&
                      $.trim(item.IconName) != "";

        if (hasIcon) {
            if ($.trim(item.IconName).indexOf("icon-") != -1) {
                linkHtml += '<i class="iconfont ' + $.trim(item.IconName) + '" data-icon="' + $.trim(item.IconName) + '"></i>';
            }
            else {
                linkHtml += '<i class="layui-icon" data-icon="' + $.trim(item.IconName) + '">' + $.trim(item.IconName) + '</i>';
            }
        }

        linkHtml += '<cite>' + $.trim(item.Name) + '</cite>';
        linkHtml += '</a>';

        return linkHtml;
    };

    //加载子模块到左侧导航菜单
    LoadSubModule = function (currentLink) {
        var strModuleId = $(currentLink).attr("ModuleId");

        $.get("/UserSession/GetSubModulesTree?parentModuleId=" + escape(strModuleId),
            function (data) {
                $(".navBar").html(BuildLeftNavBar(data, false)).height($(window).height() - 245);
                element.init(); //初始化页面元素
                $(window).resize(function () {
                    $(".navBar").height($(window).height() - 245);
                });
            },
            "json");
    };

    LoadCurrentModule = function (currentLink) {
        var strModuleId = $(currentLink).attr("ModuleId");

        $.get("/UserSession/GetCurrentModulesTree?currentModuleId=" + escape(strModuleId),
            function (data) {
                $(".navBar").html(BuildLeftNavBar(data, true)).height($(window).height() - 245);
                element.init(); //初始化页面元素
                $(window).resize(function () {
                    $(".navBar").height($(window).height() - 245);
                });
            },
            "json");
    };

    //加载子模块到左侧导航菜单
    LoadSubModuleById = function (moduleId) {
        $.get("/UserSession/GetSubModulesTree?parentModuleId=" + escape(moduleId),
            function (data) {
                $(".navBar").html(BuildLeftNavBar(data, false)).height($(window).height() - 245);
                element.init(); //初始化页面元素
                $(window).resize(function () {
                    $(".navBar").height($(window).height() - 245);
                });
            },
            "json");
    };

    LoadCurrentModuleById = function (moduleId) {
        $.get("/UserSession/GetCurrentModulesTree?currentModuleId=" + escape(moduleId),
            function (data) {
                $(".navBar").html(BuildLeftNavBar(data, true)).height($(window).height() - 245);
                element.init(); //初始化页面元素
                $(window).resize(function () {
                    $(".navBar").height($(window).height() - 245);
                });
            },
            "json");
    };

    BuildLeftNavBar = function (strData, isCurrentModule) {
        var data;
        if (typeof (strData) == "string") {
            data = JSON.parse(strData); //部分用户解析出来的是字符串，转换一下
        }
        else {
            data = strData;
        }

        var ulHtml = '<ul class="layui-nav layui-nav-tree">';

        var hasData = data != undefined &&
                      data != null &&
                      data.length > 0;

        if (hasData) {
            for (var i = 0; i < data.length; i++) {
                var hasItem = data[i] != undefined &&
                              data[i] != null &&
                              data[i].Item != undefined &&
                              data[i].Item != null;

                if (!hasItem) {
                    continue;
                }

                var hasChildren = data[i].Children != undefined &&
                    data[i].Children != null &&
                    data[i].Children.length > 0;

                var hasUrl = data[i].Item.Url != undefined &&
                    data[i].Item.Url != null &&
                    $.trim(data[i].Item.Url) != "";

                if (isCurrentModule) {
                    ulHtml += '<li class="layui-nav-item layui-this">';
                }
                else {
                    if (hasChildren &&
                        data[i].Item.Id == $("input#hidLeftParentId").val()) {
                        ulHtml += '<li class="layui-nav-item layui-nav-itemed" func="' + data[i].Item.Code +'">';
                    }
                    else if (!hasChildren &&
                        hasUrl &&
                        $.trim(data[i].Item.Url) != "/" &&
                        data[i].Item.Id == $("input#hidLeftModuleId").val()) {
                        ulHtml += '<li class="layui-nav-item layui-this" func="' + data[i].Item.Code +'">';
                    }
                    else {
                        ulHtml += '<li class="layui-nav-item" func="' + data[i].Item.Code +'">';
                    }
                }

                if (hasChildren ||
                    (!hasChildren &&
                    (!hasUrl || (hasUrl && $.trim(data[i].Item.Url) == "/")))) {
                    ulHtml += BuildParentModuleLink(data[i].Item, false, true);

                    if (hasChildren) {
                        ulHtml += '<dl class="layui-nav-child">';

                        for (var j = 0; j < data[i].Children.length; j++) {
                            hasItem = data[i].Children[j] != undefined &&
                                data[i].Children[j] != null &&
                                data[i].Children[j].Item != undefined &&
                                data[i].Children[j].Item != null;

                            if (!hasItem) {
                                continue;
                            }

                            if (data[i].Children[j].Item.Id == $("input#hidLeftModuleId").val()) {
                                ulHtml += '<dd class="layui-this" func="' + data[i].Children[j].Item.Code +'">';
                            }
                            else {
                                ulHtml += '<dd func="' + data[i].Children[j].Item.Code +'">';
                            }

                            hasChildren = data[i].Children[j].Children != undefined &&
                                data[i].Children[j].Children != null &&
                                data[i].Children[j].Children.length > 0;

                            hasUrl = data[i].Children[j].Item.Url != undefined &&
                                data[i].Children[j].Item.Url != null &&
                                $.trim(data[i].Children[j].Item.Url) != "";

                            if (hasChildren ||
                            (!hasChildren &&
                                (!hasUrl || (hasUrl && $.trim(data[i].Children[j].Item.Url) == "/")))) {
                                ulHtml += BuildParentModuleLink(data[i].Children[j].Item, true, true);
                            }
                            else {
                                ulHtml += BuildLeafModuleLink(data[i].Children[j].Item, false);
                            }

                            ulHtml += '</dd>';
                        }

                        ulHtml += '</dl>';
                    }
                }
                else {
                    ulHtml += BuildLeafModuleLink(data[i].Item, false);
                }

                ulHtml += '</li>';
            }
        }

        ulHtml += '</ul>';
        return ulHtml;
    };
});