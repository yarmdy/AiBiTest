layui.config({
    base: "/js/"
}).use(['form','vue', 'ztree', 'layer', 'jquery', 'table','droptree','openauth','utils'], function () {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery;
    var table = layui.table;
    var openauth = layui.openauth;
    var toplayer = (top == undefined || top.layer === undefined) ? layer : top.layer;  //顶层的LAYER
    var currentPage = 1;
    //layui.droptree("/UserSession/GetOrgs", "#Organizations", "#OrganizationIds");
   
    $("#menus").loadMenus("Role");

    //主列表加载，可反复调用进行刷新
    var config= {};  //table的参数，如搜索key，点击tree的id
    var mainList = function (options) {
        if (options != undefined) {
            $.extend(config, options);
        }
        table.reload('mainList', {
            url: '/RoleManager/Load',
            where: config,
            page: {
                curr: currentPage //重新从第 1 页开始
            },
            done: function (res, curr, count) {
                var hasData = res != undefined &&
                    res != null &&
                    res.data != undefined &&
                    res.data != null &&
                    res.data.length > 0;

                var hasCount = res != undefined &&
                    res != null &&
                    res.count != undefined &&
                    res.count != null &&
                    res.count > 0 &&
                    count != undefined &&
                    count != null &&
                    count > 0;

                if (!hasData && hasCount) {
                    currentPage = (count % this.limit == 0 ? count / this.limit : (count / this.limit) + 1);
                    mainList();
                    return false;
                }

                currentPage = curr;
            }
        });
    }
    //左边树状机构列表
    //var ztree = function () {
    //    var url = '/UserSession/GetOrgs';
    //    var zTreeObj;
    //    var setting = {
    //        view: { selectedMulti: false },
    //        data: {
    //            key: {
    //                name: 'Name',
    //                title: 'Name'
    //            },
    //            simpleData: {
    //                enable: true,
    //                idKey: 'Id',
    //                pIdKey: 'ParentId',
    //                rootPId: ""
    //            }
    //        },
    //        callback: {
    //            onClick: function (event, treeId, treeNode) {
    //                mainList({ orgId: treeNode.Id });
    //            }
    //        }
    //    };
    //    var load = function () {
    //        $.getJSON(url, function (json) {
    //            zTreeObj = $.fn.zTree.init($("#tree"), setting);
    //            //var newNode = { Name: "根节点", Id: null,ParentId:"" };
    //            //json.push(newNode);
    //            zTreeObj.addNodes(null, json);
    //            mainList({ orgId: "" });
    //            zTreeObj.expandAll(false);
    //        });
    //    };
    //    load();
    //    return {
    //        reload: load
    //    }
    //}();
    //$("#tree").height($("div.layui-table-view").height());

    //添加（编辑）对话框
    var editDlg = function() {
        var vm = new Vue({
            el: "#formEdit"
        });
        var title = "";
        var show = function (data) {
            var index = layer.open({
                title: title,
                area: ["500px", "400px"],
                type: 1,
                content: $('#divEdit'),
                success: function() {
                    vm.$set('$data', data);

                    $("input:checkbox[name='Status']").prop("checked", data.Status == 1);
                    form.render();
                },
                end: mainList
            });
            var url = "/RoleManager/Add";
            $("#divBtn").show();
            if (title === "编辑") {
                url = "/RoleManager/Update"; 
            }
            else if (title === "查看") {
                $("#divBtn").hide();
            }
            //提交数据
            form.on('submit(formSubmit)',
                function(data) {
                    $.post(url,
                        data.field,
                        function(data) {
                            layer.msg(data.Message, { time: 1000 }, function () {
                                if (data.Code === 200) {
                                    layer.close(index);
                                }
                            });
                        },
                        "json");
                    return false;
                });
        }
        return {
            add: function() { //弹出添加
                title = "添加";
                show({
                    Id: ''
                });
            },
            update: function(data) { //弹出编辑框
                title = "编辑";
                show(data);
            },
            detail: function (data) {
                title = "查看";
                show(data);
            }
        };
    }();
    
    //监听表格内部按钮
    table.on('tool(list)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') {      //查看
            editDlg.detail(data);
        } 
    });


    //监听页面主按钮操作
    var active = {
        btnDel: function () {      //批量删除
            var checkStatus = table.checkStatus('mainList')
                , data = checkStatus.data;
            openauth.del("/RoleManager/Delete",
                data.map(function (e) { return e.Id; }),
                mainList);
        }
        , btnAdd: function () {  //添加
            editDlg.add();
        }
         , btnEdit: function () {  //编辑
             var checkStatus = table.checkStatus('mainList')
               , data = checkStatus.data;
             if (data.length != 1) {
                 layer.msg("请选择编辑的行，且同时只能编辑一行");
                 return;
             }
             editDlg.update(data[0]);
         }

        , search: function () {   //搜索
            currentPage = 1;
            mainList({ key: $('#key').val() });
        }
        , btnRefresh: function() {
            mainList();
        }
        , btnAccessModule: function () {
            var checkStatus = table.checkStatus('mainList')
               , data = checkStatus.data;
            if (data.length != 1) {
                layer.msg("请选择要分配的角色");
                return;
            }

            var index = layer.open({
                title: "为角色【" + data[0].Name + "】分配模块",
                type: 2,
                area: ['750px', '600px'],
                content: "/ModuleManager/Assign?type=RoleModule&menuType=RoleElement&id=" + data[0].Id,
                success: function (layero, index) {

                }
            });
        }
        , btnAssignReource: function () {
            var checkStatus = table.checkStatus('mainList')
                , data = checkStatus.data;
            if (data.length != 1) {
                toplayer.msg("请选择要分配的角色");
                return;
            }

            var index = toplayer.open({
                title: "为角色【" + data[0].Name + "】分配资源",
                type: 2,
                area: ['750px', '600px'],
                content: "/Resources/Assign?type=RoleResource&id=" + data[0].Id,
                success: function (layero, index) {

                }
            });
        }
    };

    $('.toolList .layui-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });

    //监听页面主按钮操作 end

    currentPage = 1;
    mainList();
})