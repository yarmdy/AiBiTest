layui.config({
    base: "/js/"
}).use(['ztree','table'], async function () {
    let ztree;
    const table = layui.table;
    const cols = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'RealName', title: '姓名' },
        { field: 'Account', title: '账号', templet:function(d) { return d.User.Account; } },
        { field: 'Mobile', title: '手机号', templet: function (d) { return d.User.Mobile||""; } },
        {
            field: 'Sex', title: '性别', templet: function (d) {
                return EnumSex[d.Sex]||"";
            }
        },
        { field: 'UnitName', title: '单位' },
        { field: 'IdCardNo', title: '身份证号' },
        { field: 'Birthday', title: '生日' },
        { field: 'Action', title: '操作',fixed:"right", templet: "#actionTemplate",width:210 },

    ]];
    ImportDialog.init(layui);
    layui.util.on("lay-on", {
        import: function () {
            ImportDialog.show();
        }
    });
    callback.userinfoaddok = function (json) {
        layer.success(json.msg);
        getList();
    }
    callback.userinfoeditok = function (json) {
        layer.success(json.msg);
        getList();
    }
    callback.uploadSuccess = function (json) {
        getList(1, 10);
        layer.success(json.msg);
    }

    table.render({
        elem: '#table',
        url: BaseUrl+ $$.common.getPageList.url, // 此处为静态模拟数据，实际使用时需换成真实接口
        cols: cols,
        page: true,
        limits: [10, 50, 1000],
        limit: 10,
        request: {
            pageName: 'page', // 页码的参数名称，默认：page
            limitName: 'size' // 每页数据条数的参数名，默认：limit
        },
        parseData: function (json) {
            var old = table.cache.select_table;
            var oldIds = old.map(function (a) { return a.UserId });
            json.code = json.code > 0 ? 0 : json.code;
            json.data.forEach(function (a) {
                a.LAY_CHECKED = (oldIds.indexOf(a.UserId) >= 0);
            });
            return json;
        },
        height: "full-125",
        size: "sm",
        method: "post",
        
    });
    
    $("#searchForm").on("submit", function () {
        getList(1);
        return false;
    });

    function getList(page,size) {
        var postdata = $$.getFormData("#searchForm");
        if (ztree) {
            var nodes = ztree.getSelectedNodes();
            var node = {};
            if (nodes.length > 0) {
                node = nodes[0];
            }
            if (node.Id) {
                postdata.GroupId = node.Id;
            }
        }
        table.reloadData("table", {
            where: postdata,
            page: page ? {
                curr: page,
                limit: size
            } : {}
        });
    }
    
    table.on("tool(table)", function (e) {
        switch (e.event) {
            
        }
    });



    const cols2 = [[
        { type: 'checkbox', fixed: "left" }, // 单选框
        { field: 'RealName', title: '姓名' },
        { field: 'Action', title: '操作', fixed: "right", templet: "#selectActionTemplate", width: 80 },

    ]];
    table.render({
        elem: '#select_table',
        data: [],
        cols: cols2,
        height: "350",
        size: "sm",

    });
    table.on("tool(select_table)", function (e) {
        switch (e.event) {
            case "delete": {
                var old = table.cache.select_table.filter(a => a.UserId != e.data.UserId);
                table.reloadData("select_table", { data: old });
                getList();
            } break;
        }
    });

    table.on("checkbox(table)", function (e) {
        var arr = [];
        if (e.type == "one") {
            arr.push(e.data);
        } else if (e.type == "all") {
            arr = arr.concat(table.cache.table);
        }

        var old = table.cache.select_table;
        if (e.checked) {
            var oldIds = old.map(function (a) { return a.UserId });
            var newArr = arr.filter(function (a) {
                a.LAY_CHECKED = false;
                return oldIds.indexOf(a.UserId) < 0;
            });
            old = old.concat(newArr);
        } else {
            let removeIds = arr.map(function (a) { return a.UserId });
            old = old.filter(function (a) {
                return removeIds.indexOf(a.UserId) < 0;
            });
        }
        table.reloadData("select_table", { data: old });
    });
    let selectH = $("#selectH").height();
    $("#minsize").on("click", function () {
        if (this.isMin) {
            $("#selectH").animate({ height: selectH + "px" }, function () {
                $("#selectH").css({ height: "auto" });
            });
            this.isMin = false;
        } else {
            $("#selectH").animate({ height: "0" });
            this.isMin = true;
        }
    });
    $("#selectok").on("click", function () {
        $$.callback("userinfoselectok", table.cache.select_table);
        $$.closeThis();
    });
    function treeInit() {
        let lastRenameNode;
        let newCount = 1;
        $("#tree").height($(document).outerHeight() - 125 - 12);
        function dataFilter(treeId, parentNode, responseData) {
            responseData.data.forEach(function (a) {
                a.isParent = (a.ObjectTag || {}).IsParent || false;
            });
            return responseData.data;
        }
        var setting = {
            async: {
                enable: true,
                url: "/UserGroup/GetPageList",
                otherParam: function (treeId, treeNode) {
                    res = {
                        page: 1,
                        size: 10000,
                        where: {
                            ParentId: (treeNode || {}).Id || null,
                        }
                    };
                    return res;
                },
                dataFilter: dataFilter
            },
            data: {
                key: {
                    name: "Name"
                },
                simpleData: {
                    enable: true,
                    idKey: "Id",
                    pIdKey: "ParentId",
                }
            },
            view: {
                selectedMulti: false,
                addHoverDom: addHoverDom,
                removeHoverDom: removeHoverDom
            },
            callback: {
                onClick: function (event, treeId, treeNode, clickFlag) {
                    getList(1);
                },
                beforeRemove: beforeRemove,
                beforeRename: beforeRename,
                onAsyncSuccess: onAsyncSuccess,
                beforeDrag: beforeDrag,
                beforeDrop: beforeDrop,
            },
            edit: {
                enable: true
            }
        };
        function beforeDrag(treeId, treeNodes) {
            var node = treeNodes[0];
            if (node.Id === null) {
                return false;
            }
            return true;
        }
        async function beforeDropAsync(treeId, treeNodes, targetNode, moveType, isCopy) {
            let node = treeNodes[0];
            var parentId = node.ParentId;
            if (moveType == "inner") {
                parentId = targetNode.Id;
            }
            if (moveType == "next" || moveType == "prev") {
                parentId = targetNode.ParentId;
            }
            var postData = $.extend({}, node);
            postData.ParentId = parentId;
            var json = await $$.post("/UserGroup/Modify", postData);
            if (!json) {
                return;
            }
            node.ParentId = json.data.ParentId;
            node.SortNo = json.data.SortNo;
            ztree.moveNode(targetNode, node, moveType, true);
        }
        function beforeDrop(treeId, treeNodes, targetNode, moveType, isCopy) {
            if (targetNode.Id === null && (moveType == "next" || moveType=="prev")) {
                return false;
            }
            beforeDropAsync(treeId, treeNodes, targetNode, moveType, isCopy);
            return false;
        }
        async function delNode(treeId, treeNode) {
            var res = await layer.confirmAsync("确认删除 节点 -- " + treeNode.Name + " 吗？").catch(function (r) { });
            if (!res) {
                return;
            }
            var json = await $$.post("/UserGroup/Delete",{ ids: [[treeNode.Id]] });
            if (!json) {
                return;
            }
            ztree.removeNode(treeNode, false);
            layer.close(res);
        }
        function onAsyncSuccess(event, treeId, treeNode, msg) {
            setTimeout(function () {
                if (!lastRenameNode) {
                    return;
                }
                ztree.editName(lastRenameNode);
                lastRenameNode = null;
            }, 0)
        }
        function beforeRemove(treeId, treeNode) {
            ztree.selectNode(treeNode);
            delNode(treeId, treeNode);
            return false;
        }
        async function beforeRenameAsync(treeId, treeNode, newName, isCancel) {
            
            var url = treeNode.Id > 0 ? "/UserGroup/Modify" : "/UserGroup/Add";
            treeNode.SortNo = treeNode.SortNo || treeNode.getIndex;
            var postData = $.extend({}, treeNode);
            postData.Name = newName;
            var json = await $$.post(url, postData).catch(function () { treeNode.Id > 0 ? ztree.cancelEditName() : ztree.removeNode(treeNode); });
            if (!json) {
                return;
            }
            $.extend(treeNode, json.data);
            ztree.cancelEditName(treeNode.Name);
            
        }
        function beforeRename(treeId, treeNode, newName, isCancel) {
            if (isCancel && treeNode.Id > 0) {
                return true;
            }
            if (isCancel && treeNode.Id <= 0) {
                ztree.removeNode(treeNode);
                return true;
            }
            if (newName.length == 0) {
                return false;
            }
            beforeRenameAsync(treeId, treeNode, newName, isCancel);
            return false;
        }
        
        function addHoverDom(treeId, treeNode) {
            var sObj = $("#" + treeNode.tId + "_span");
            if (treeNode.editNameFlag || $("#addBtn_" + treeNode.tId).length > 0) return;
            var addStr = "<span class='button add' id='addBtn_" + treeNode.tId
                + "' title='add node' onfocus='this.blur();'></span>";
            sObj.after(addStr);
            var btn = $("#addBtn_" + treeNode.tId);
            if (btn[0]) btn.bind("click", function () {
                
                var newnode = ztree.addNodes(treeNode, { Id: 0, ParentId: treeNode.id, Name: "新分组_" + (newCount++) });
                if (treeNode.zAsync) {
                    ztree.editName(newnode[0]);
                } else {
                    lastRenameNode = newnode[0];
                }
                return false;
            });
        };
        function removeHoverDom(treeId, treeNode) {
            $("#addBtn_" + treeNode.tId).unbind().remove();
        };
        var nodes = [{ Name: "全部", isParent: true, Id: null, ParentId: null }];

        return $.fn.zTree.init($("#tree"), setting, nodes);
    }
    ztree = treeInit();
});
