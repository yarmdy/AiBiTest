const obj = {
    //#region 基础功能
    loadingType:2,//0: 关闭 1: layer自带load 2: loadEx(2) 
    ajax: function (type, url, data, success, dataType, silent) {
        if (typeof data === "function") {
            dataType = dataType || success;
            success = data;
            data = void 0;
        }
        let loadlayer;
        let sendData = { rnd: Math.random() };
        $.extend(sendData, data);
        let ajaxObj = {
            url: url,
            data: sendData,
            type: type,
            beforeSend: function () {
                if (silent) return;
                //loadlayer = layer.load(0,{shade:0.3});
                layer.closeAll("loading");
                //loadlayer = layer.loadEx();
                if (obj.loadingType == 1) {
                    loadlayer = layer.load(2);
                } else if (obj.loadingType == 2) {
                    loadlayer = layer.loadEx(2);
                }
            },
            complete: function () {
                if (silent) return;
                layer.close(loadlayer);
            },
            success: success,
            error: function (a, b, c) {
                layer.msg(a.status + " (" + (a.responseJSON && a.responseJSON.msg ? a.responseJSON.msg : c) + ")", { icon: 2 });
            }
        };
        if (dataType) {
            ajaxObj.dataType = dataType;
        }
        layer.closeAll("loading");
        return $.ajax(ajaxObj);
    },
    //带进度条的 阿贾克斯
    ajaxload: function (type, url, data, success, dataType) {
        if (typeof data === "function") {
            dataType = dataType || success;
            success = data;
            data = void 0;
        }
        let loadlayer;
        let loadlayerTimer;
        let loadlayerTime = 0;
        var progressBarTxt = '<div class="layui-progress layui-progress-big">' +
            '<div class="layui-progress-bar" lay-percent="30%"></div>' +
            '</div>';
        let sendData = { rnd: Math.random() };
        $.extend(sendData, data);
        let ajaxObj = {
            url: url,
            data: sendData,
            type: type,
            beforeSend: function () {
                loadlayer = layer.open({
                    content: progressBarTxt, type: 1, area: ["30%", "150px"], title: false, closeBtn: 0, zIndex: 99999999, success: function (layero, index, that) {
                        loadlayerTimer = setInterval(function () {

                            layero.find("[lay-percent]").attr("lay-percent", (loadlayerTime) + "%")
                            element.render("progress");
                            if (loadlayerTime < 90) {
                                loadlayerTime += 0.5;
                            } else {
                                loadlayerTime += 0.01;
                            }
                            if (loadlayerTime >= 100) {
                                clearInterval(loadlayerTimer);
                                setTimeout(function () {
                                    layer.close(loadlayer);
                                }, 333);
                            }
                        }, 100);
                    }
                });
                layer.style(loadlayer, { background: "transparent", "box-shadow": "none" });
            },
            complete: function () {
                loadlayerTime = 100;
            },
            success: success,
            error: function (a, b, c) {
                layer.msg(a.status + " (" + c + ")", { icon: 2 });
            }
        };
        if (dataType) {
            ajaxObj.dataType = dataType;
        }
        return $.ajax(ajaxObj);
    },
    //带layer load的post
    opost: function (url, data, success, dataType) {
        return obj.ajax("POST", url, data, success, dataType);
    },
    //带layer load的get
    oget: function (url, data, success, dataType) {
        return obj.ajax("GET", url, data, success, dataType);
    },
    //带layer load的 浏览器本地缓存对象，方便的方法
    cache: function (name) {
        var caches = [];
        var func = function (name) {
            this.name = name;
        }
        function getObj(name) {
            var res = window.localStorage.getItem(name);
            if (!res) {
                return {};
            }
            return eval("(" + res + ")");
        }
        func.prototype.get = function (key) {
            if (!window.localStorage) return null;
            return getObj(this.name)[key];
        }
        func.prototype.set = function (key, value) {
            if (!window.localStorage) return null;
            var obj = getObj(this.name);
            obj[key] = value;
            window.localStorage.setItem(this.name, JSON.stringify(obj));
            return this;
        }
        func.prototype.obj = function () {
            return getObj(this.name);
        }
        obj.cache = function (name) {
            if (!caches[name]) {
                caches[name] = new func(name);
            }
            return caches[name];
        }
        return obj.cache(name);
    },
    
    //按集合获取属性
    getAttrs: function (elem, ...attrs) {
        var res = {};
        $.each(attrs, function (i, a) {
            res[a] = $(elem).attr(a);
        });
        return res;
    },
    //按集合设置属性
    setAttrs: function (elem, obj) {
        $.each(obj, function (i, a) {
            $(elem).attr(i, a);
        });
    },
    //获取表单数据 高级
    getFormData: function (elem) {
        var res = {};
        $(elem).find("[name]").each(function (i, a) {
            var tagName = a.tagName;
            var name = $(a).attr("name");
            var hasValue = ["INPUT", "TEXTAREA", "SELECT"].indexOf(tagName) >= 0;
            if (!hasValue) {
                res[name] = $(a).html();
                return true;
            }
            var type = ($(a).attr("type") + "").toLowerCase();

            if (type == "radio" && !$(a).is(":checked")) {

                return true;
            }
            if (type == "checkbox" && !$(a).is(":checked")) {
                return true
            } else if (type == "checkbox" && $(elem).find("[name=" + name + "]").length > 1) {
                if (!res[name]) {
                    res[name] = [];
                }
                res[name].push($(a).val());
                return true;
            }
            res[name] = $(a).val();
        });
        return res;
    },
    //设置表单数据 高级
    setFormData: function (elem, data, excepts,notnull) {
        if (!data) return;
        $(elem).find("[name]").each(function (i, a) {
            var tagName = a.tagName;
            var name = $(a).attr("name");
            if (excepts && excepts.indexOf(name) >= 0) {
                return true;
            }
            var setval = data[name];
            if (setval == null && notnull) {
                return true;
            }
            var hasValue = ["INPUT", "TEXTAREA", "SELECT"].indexOf(tagName) >= 0;
            if (!hasValue) {
                $(a).html(setval);
                return true;
            }
            var type = ($(a).attr("type") + "").toLowerCase();
            if (type == "radio") {
                $(a).prop("checked", $(a).val() == setval);
                return true;
            }
            if (type == "checkbox") {
                if (setval instanceof Array) {
                    $(a).prop("checked", setval.indexOf($(a).val()) >= 0);
                } else {
                    $(a).prop("checked", $(a).val() == setval);
                }

                return true;
            }

            $(a).val(setval);
        });
    },
    //#endregion
    request: function (type, url, data, silent) {
        var def = $.Deferred();
        obj.ajax(type, url, data, null, null, silent).then(function (json) {
            if (json.code >= 0) {

                def.resolve(json);
                return;
            }
            layer.error(json.msg);
            def.reject(json);
        }).fail(function (a, b, c) {
            def.reject({ code: -1, msg: (a.status + " (" + c + ")", { icon: 2 }) });
        });
        return def.promise();
    },
    post: function (url, data, silent) {
        return this.request("POST", url, data, silent);
    },
    get: function (url, data, silent) {
        return this.request("GET", url, data, silent);
    },
    common: {
        getPageList: {
            url: "/GetPageList",
            method:"GET",
            req: function (data) {
                return obj.get(BaseUrl + obj.common.getPageList.url, data);
            }
        },
        getDetail: {
            url: "/GetDetail",
            method: "GET",
            req: function (data) {
                return obj.get(BaseUrl + obj.common.getDetail.url + "/" + (PageInfo && PageInfo.KeyValueStr), data);
            }
        },
        add: {
            url: "/Add",
            method: "POST",
            req: function (data) {
                return obj.post(BaseUrl + obj.common.add.url, data);
            }
        },
        edit: {
            url: "/Modify",
            method: "POST",
            req: function (data) {
                return obj.post(BaseUrl + obj.common.edit.url + "/" + (PageInfo && PageInfo.KeyValueStr), data);
            }
        },
        delete: {
            url: "/Delete",
            method: "POST",
            req: function (ids) {
                let data = {};
                data.ids = ids;
                return obj.post(BaseUrl + obj.common.delete.url, data);
            }
        },
    },
    tabName: function () {
        let dataid = top.$(window.frameElement).attr("data-id");
        return top.$("i[data-id=" + dataid + "]").prev("cite").text();
    },
    closeThis: function () {
        if (!top.tab || !top.tab.closeTab) {
            window.close();
            return;
        }
        var iframe = window.frameElement;
        if (!iframe) return;
        if (!$(iframe).attr("data-id")) return;
        top.tab.closeTab($(iframe).attr("data-id"));
    },
    toOpenUrl: function (url, name) {
        url = obj.toUrl(url);
        if (!url || url.toLowerCase() == "about:blank") {
            return "about:blank";
        }
        var tabname = obj.tabName();
        if (tabname) {
            url = obj.replaceParam(url, "opener", "opener=" + tabname);
        }
        name = name == null ? null : $.trim(name);
        return $$.replaceParam(url, "title", "title=" + name);
    },
    addTab: function (name, url) {
        url = obj.toOpenUrl(url, name);
        if (top === window && typeof window.addTab !== "function") {
            var winhwnd = window.open("about:blank");
            winhwnd.location.href = url;
            return;
        }
        top.addTab(name, url);
    },
    toUrl: function (str) {
        if (!str || str.toLowerCase() == "about:blank") {
            return "about:blank";
        }
        var url = new URL($("<a>").attr("href", str)[0].href);
        return url.pathname + url.search + url.hash;
    },
    //  /(?<=\?|\&)opener(\=.*?|)(?=$|\#|\&)/is
    replaceParam: function (url, name, rep) {
        
        var paramReg = new RegExp("(?<=\\?|\\&)" + name + "(?:\\=.*?)??(?=$|\&|\\#)", "is");
        param = paramReg.exec(url);
        if (param != null) {
            return obj.toUrl(url.replace(paramReg, rep));
        }
        var domainReg = /(?:\?.*?(?=\#|$)|(?=\#|$))/is;
        var domain = domainReg.exec(url)[0];
        if (domain.indexOf("?") < 0) {
            domain = '?' + rep;
        } else {
            domain += ("&" + rep);
        }
        return obj.toUrl(url.replace(domainReg, domain));
    },
    opener: function () {
        if (window.opener) {
            return window.opener;
        }

        if (!PageInfo || !PageInfo.opener || !top.findTab) {
            return top !== window ? top : null;
        }
        let tmptab = top.findTab(PageInfo.opener);
        if (!tmptab) {
            return null;
        }
        return tmptab.page.find("iframe")[0].contentWindow;
    },
    callback: function (name, ...params) {
        let opener = obj.opener();
        if (!opener) {
            return;
        }
        if (typeof opener.callback != "object") {
            return;
        }
        if (typeof opener.callback[name] != "function") {
            return;
        }
        opener.callback[name].apply(window, params);
    },
    serverNow: function () {
        if (!window.TimeInfo) {
            return new Date();
        }
        let offset = window.TimeInfo.Client - window.TimeInfo.Server;
        return new Date(new Date() - offset);
    },
    selectAll: function (all, single,callback) {
        $(document).on("click", all + "," + single, function (e) {
            let $this = $(this);
            if ($this.is(all)) {
                let checked = $this.prop("checked");
                let changeList;
                if (checked) {
                    changeList = $(single).not(":checked").toArray();
                } else {
                    changeList = $(single).filter(":checked").toArray();
                }
                $(single).prop("checked", checked);
                callback && callback({
                    checked: checked,
                    list: changeList,
                });
            } else {
                let checked = $(single).not(":checked").length <= 0;
                
                $(all).prop("checked", checked);
                callback && callback({
                    checked: checked,
                    list: [$this[0]],
                });
            }
        })
    },
    fullScreen: function () {
        let element = document.documentElement;
        if (element.requestFullscreen) {
            element.requestFullscreen();
        } else if (element.mozRequestFullScreen) {   // 兼容火狐
            element.mozRequestFullScreen();
        } else if (element.webkitRequestFullscreen) {    // 兼容谷歌
            element.webkitRequestFullscreen();
        } else if (element.msRequestFullscreen) {   // 兼容IE
            element.msRequestFullscreen();
        }
    }
}

//#region 扩展
Array.prototype.select = function (func) {
    if (typeof func !== "function") {
        return this;
    }
    var res = [];
    $.each(this, function (i, a) {
        var o = func(i, a);
        if (o == null || o == undefined) {
            return true;
        }
        res.push(o);
    });
    return res;
}
//仿 linq sum方法
Array.prototype.sum = function (func) {

    var res = 0;
    $.each(this, function (i, a) {
        var o = a;
        if (typeof func === "function") {
            o = func(i, a);
        }
        if (!o || isNaN(o)) {
            return true;
        }
        res += parseFloat(o);
    });
    return res;
}
//循环正则替换 高级
String.prototype.replaceEach = function (reg, callback, tag) {
    var res = this;
    if (!(reg instanceof RegExp) || typeof callback !== "function") {
        return res;
    }
    var internalIndex = 0;
    while (reg.test(res)) {
        reg.lastIndex = 0;
        var regExec = reg.exec(res);
        reg.lastIndex = 0;
        regExec.runIndex = internalIndex++;
        res = res.replace(reg, callback(regExec));
        reg.lastIndex = 0;
    }
    return res;
}
//padStart
if (!String.prototype.padStart) {
    String.prototype.padStart = function (length, char) {
        var len = length - this.length;
        if (len <= 0) return this;
        return new Array(len + 1).join(char).substring(0, len) + this;
    }
}
//仿.net 日期格式化    
Date.prototype.format = function (fmt) {
    fmt = fmt || "yyyy-MM-dd HH:mm:ss";
    let dic = {
        y: this.getFullYear() + "",
        M: (this.getMonth() + 1) + "",
        d: (this.getDate()) + "",
        H: (this.getHours()) + "",
        h: (this.getHours() % 12 || 12) + "",
        m: (this.getMinutes()) + "",
        s: (this.getSeconds()) + "",
        f: (this.getMilliseconds()) + ""
    };
    $.each(dic, function (i, a) {
        fmt = fmt.replaceEach(new RegExp(i + "+", "s"), function (exec) {
            var r = dic[i].padStart(exec[0].length, '0');
            switch (i) {
                case "y":
                    {
                        r = r.substr(r.length - exec[0].length || 0);
                    } break;
            }

            return r;
        }, "s");
    });

    return fmt + "";
}
Number.prototype.toMoneyString = function () {
    var exec = /^(.*?)(?:\.(.*))?$/.exec((Math.round(this * 100) / 100).toLocaleString());
    var z = exec[1];
    var x = exec[2] || "";
    if (x.length > 2) {
        x = x.substring(0, 2);
    }
    x = x.padEnd(2, '0');
    return z + "." + x;
}
//兼容jquery .html和.val()
$.fn.hval = function (val) {
    var tagName = this.prop("tagName");
    if (!tagName) {
        return val == undefined ? undefined : this;
    }
    if (val != undefined) {
        var hasValue = ["INPUT", "TEXTAREA", "SELECT"].indexOf(tagName) >= 0;
        if (!hasValue) {
            this.html(val);
            return this;
        }
        var type = (this.attr("type") + "").toLowerCase();
        var isSelect = ["radio", "checkbox"].indexOf(type) >= 0;
        if (isSelect) {
            this.filter("[value=" + val + "]").prop("checked");
            return this;
        }
        if (tagName == "IMG") {
            this.attr("src", val);
            return this;
        }
        this.val(val);
        return this;
    } else {
        var hasValue = ["INPUT", "TEXTAREA", "SELECT"].indexOf(tagName) >= 0;
        if (!hasValue) {
            return this.html();
        }
        var type = (this.attr("type") + "").toLowerCase();
        var isSelect = ["radio", "checkbox"].indexOf(type) >= 0;
        if (isSelect) {
            return this.filter(":checked").val();
        }
        if (tagName == "IMG") {
            return this.attr("src");
        }
        return this.val();
    }
}
//弹窗回调
layer.callback = function (opt) {
    opt = opt || {};
    opt.type = 2;
    var tmp = opt.yes;
    opt.yes = function (index, layero, that) {
        var win = layero.find("iframe").prop("contentWindow");


        if (typeof tmp == "function") {
            tmp(index, layero, that, win);
        }
    }
    var tmpSucc = opt.success;
    opt.success = function (layero, index, that) {
        var win = layero.find("iframe").prop("contentWindow");



        win.layui.funcs = opt.funcs || {};

        if (typeof opt.yescallback == "function") {
            win.layui.funcs = win.layui.funcs || {};
            win.layui.funcs["yes"] = function (...params) {
                opt.yescallback.apply(this, [layero, index, that, win].concat(params));
            }
        }

        if (typeof tmpSucc != "function") {
            return;
        }
        win.layui.funcs["load"] = function (...params) {
            tmpSucc.apply(this, [layero, index, that, win].concat(params));
        }
    }
    return layer.open(opt);
}
layer.loadEx = function (icon, opt) {
    icon = icon == null ? 2 : icon;
    var div = '<i class="layui-layer-loading-icon layui-icon layui-icon-loading layui-anim layui-anim-rotate layui-anim-loop"></i>'; 
    
    if (icon == 1) {
        div = '<i class="layui-layer-loading-icon layui-icon layui-icon-loading-1 layui-anim layui-anim-rotate layui-anim-loop"></i>';

    } else if (icon == 2) {
        div = '<div class="layui-layer-loading-2 layui-anim layui-anim-rotate layui-anim-loop"></i>';
    } else if (icon == 3) {
        div = '<div class="spinner-border ' + (opt && opt.color ? opt.color : "text-info") + '" style="width: ' + (opt && opt.area ? opt.area[0] : "50px") + '; height: ' + (opt && opt.area ? opt.area[1] : "50px") + ';border-width: 2px; "></div>';
        icon = 3;
    } else {
        icon = 0;
    }
    var opt = $.extend({
        type: 1,
        title: null,
        closeBtn: 0,
        zIndex: 99999999,
        shade: 0.001,
        content: div,
        area:icon==3?["50px","50px"]:["76px","38px"]
    }, opt);
    var tmpsucc = opt.success;
    opt.success = function (l, id, elem) {
        if (icon == 3) {
            l.css({
                width: "100%", height: "100%", left: 0, top: 0,
                display: "flex",
                justifyContent: "center",
                alignItems: "center"
            });
            l.find("div.layui-layer-content").css({
                width: opt.area[0], height: opt.area[1]
            });
        }
        l.attr("class", "layui-layer layui-layer-loading").find("div.layui-layer-content").attr("class", "layui-layer-content layui-layer-loading" + icon);
        l.find("span.layui-layer-resize").remove();
        tmpsucc && tmpsucc(l, id, elem);
    }
    
    return layer.open(opt);
}
//if (!top.layer.loadEx) {
//    top.layer.loadEx = layer.loadEx;
//}
//弹窗回调辅助
layer.triggerEvent = function (e, ...params) {
    if (!layui.funcs) return;
    if (typeof layui.funcs[e] != "function") {
        return;
    }
    layui.funcs[e].apply(window, params);
}
//统一错误弹窗
layer.error = function (msg, opt, callback) {
    var options = {
        icon: 2,
        //shade: 0.01,
        shift: 6
    };
    $.extend(options, opt);
    layer.msg(msg, options, callback);
}
//统一成功弹窗
layer.success = function (msg, opt, callback) {
    var options = {
        icon: 1,
        //shade: 0.01,
        shift: 5
    };
    $.extend(options, opt);
    layer.msg(msg, options, callback);
}
layer.image = function (title, ...srcs) {
    srcs = srcs || [];
    let data = srcs.map(function (a, i) {
        return {
            "alt": "图片"+(i+1),
            "pid": (i + 1),
            "src": a,
        };
    });
    return layer.photos({
        photos: {
            "title": title,
            "start": 0,
            data: data
        }
    });
}
layer.confirmAsync = async function (msg, opt) {
    var options = {
        icon: 3,
        //shade: 0.01,
        shift: 5,
        title:"提示",
    };
    $.extend(options, opt);
    let def = $.Deferred();
    layer.confirm(msg, options, function (l) {
        def.resolve(l);
    }, function (l) {
        def.reject(l);
    });
    var promise = def.promise();
    return promise;
}
$.fn.hideEx = function () {
    let display = this.css("display");
    this.attr("display", display);
    var styles = (this.attr("style") || "").split(';').filter(function (a) { return a; });
    styles.push("display:none!important");

    this.attr("style", styles.join(";"));
    return this;
}
$.fn.setPager = function (page, size, count, callback, parent) {

    this.each(function () {
        let parentele;
        let curparent = parent || $(this).attr("parent");
        if (curparent) {
            if (typeof curparent == "function") {
                parentele = $(curparent(this));
            } else if (curparent == true) {
                parentele = $(this).parent();
            } else if (curparent == false) {
                parentele = $(this);
            } else if (typeof curparent == "string") {
                parentele = $(this).closest(curparent);
            } else {
                parentele = $(this);
            }
        }
        if (!parentele || parentele.length <= 0) {
            parentele = $(this);
        }
        let pp = $(this).find(">p");
        let ul = $(this).find(">ul");
        ul.find("a.page-link").off("click.pager");
        ul.html("");
        if (count <= 0) {
            parentele.hideEx();
            return true;
        }
        parentele.show();
        const maxcount = 10;
        const isFirst = page == 1;
        const pageCount = parseInt(count / size) + (count % size > 0 ? 1 : 0);
        const isLast = page == pageCount;
        const firstPage = `<li class="page-item ${(isFirst ? "disabled" : "")}">
                          <a class="page-link" href="#" ${(isFirst ? 'tabindex="-1" aria-disabled="true"' : '')}>
                            <svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M15 6l-6 6l6 6" /></svg>
                            首页
                          </a>
                        </li>`;
        var first = $(firstPage);
        ul.append(first);
        if (!isFirst) {
            first.on("click.pager", function (e) {
                e.preventDefault();
                callback && callback(1);
            })
        }

        var showCount = Math.min(maxcount, pageCount);
        var minPage = Math.max(1, page - (parseInt(maxcount / 2)));
        var cha = Math.max(showCount + minPage - 1 - pageCount, 0);
        minPage = minPage - cha;

        if (minPage > 1) {
            ul.append('<li class="page-item disabled">...</li>');
        }

        for (let i = minPage; i < minPage + showCount; i++) {
            const pageHtml = `<li class="page-item ${(i == page ? 'active' : '')}"><a class="page-link" href="#">${i}</a></li>`;
            var pageele = $(pageHtml);
            ul.append(pageele);
            pageele.on("click.pager", function (e) {
                e.preventDefault();
                callback && callback(i);
            })
        }

        if (minPage + showCount - 1 < pageCount) {
            ul.append('<li class="page-item disabled">...</li>');
        }

        const lastPage = `<li class="page-item ${(isLast ? "disabled" : "")}">
                          <a class="page-link" href="#" ${(isLast ? 'tabindex="-1" aria-disabled="true"' : '')}>
                            末页
                            <svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M9 6l6 6l-6 6" /></svg>
                          </a>
                        </li>`;
        var last = $(lastPage);
        ul.append(last);
        if (!isLast) {
            last.on("click.pager", function (e) {
                e.preventDefault();
                callback && callback(pageCount);
            })
        }

        pp.html('当前第 <span>' + ((page - 1) * size + 1) + '</span> 到 <span>' + Math.min((page) * size, count) + '</span> 条，共 <span>' + count + '</span> 条');
    });

    return this;
}
$.fn.getPagerIndex = function () {
    return parseInt(this.find("li.page-item.active a.page-link").eq(0).text());
}

String.prototype.combineObject = function (obj) {
    function getobjVal(obj, path) {
        try {
            if (path.indexOf('=') == 0) {
                return eval('(' + (path.substring(1)) + ')');
            }
            if (path.indexOf('[') == 0) {
                return eval('(obj' + (path) + ')');
            }
            return eval('(obj.' + (path) + ')');
        } catch {
            return undefined;
        }
    }
    return this.replaceEach(/\{\{(.+?)\}\}/, function (exec) {
        var res = getobjVal(obj, exec[1]);
        return res == null ? "" : res;
    });
}
//#endregion


$(function () {
    if ($.validator) {
        $.extend($.validator.messages, {
            required: "这是必填字段",
            remote: "请修正此字段",
            email: "请输入有效的电子邮件地址",
            url: "请输入有效的网址",
            date: "请输入有效的日期",
            dateISO: "请输入有效的日期 (YYYY-MM-DD)",
            number: "请输入有效的数字",
            digits: "只能输入数字",
            equalTo: "你的输入不相同",
            extension: "请输入有效的后缀",
            maxlength: $.validator.format("最多可以输入 {0} 个字符"),
            minlength: $.validator.format("最少要输入 {0} 个字符"),
            rangelength: $.validator.format("请输入长度在 {0} 到 {1} 之间的字符串"),
            range: $.validator.format("请输入范围在 {0} 到 {1} 之间的数值"),
            max: $.validator.format("请输入不大于 {0} 的数值"),
            min: $.validator.format("请输入不小于 {0} 的数值"),
            step: $.validator.format("请输入 {0} 的倍数")
        });
        $.validator.setDefaults({
            errorPlacement:function(error, element) {
                error.appendTo(element.parent());
            },
            ignore: ".ignore",
            errorClass: "layui-font-red layui-form-danger",
            validClass:"layui-font-black layui-border-green",
            errorElement:"em",
        });
    }

    addtabindex = 0;
    $(document).on("click.addtab", "a[target=addtab]", function (e) {
        if (window.callback && (typeof window.callback.beforeClickAddTab == "function") && !window.callback.beforeClickAddTab.call(this,e)) {
            return;
        }
        e.preventDefault();
        obj.addTab($(this).attr("title") || $(this).text(),$(this).attr("href"));
    });
    $(window).on("beforeunload.addtab", function () {
        var tabname = obj.tabName();
        if (!tabname) {
            return;
        }
        top.findTab(tabname).page.find("div.shade").show();
    })
})

window.$$ = obj;
window.callback = {};