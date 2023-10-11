layui.config({
    base: "/js/"
}).use(['form', 'ztree', 'layer', 'jquery', 'table','droptree','openauth','utils'], function () {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery;
    var table = layui.table;
    var openauth = layui.openauth;
    var toplayer = (top == undefined || top.layer === undefined) ? layer : top.layer;  //顶层的LAYER
    var currentPage = 1;

    layui.droptree("/UserSession/GetOrgs", "#Organizations", "#OrganizationIds", $("input#hidIsUserBelongsToMultipleOrg").val() == "1");
   
    $("#menus").loadMenus("User");

    //主列表加载，可反复调用进行刷新
    var config= {};  //table的参数，如搜索key，点击tree的id
    var mainList = function (options) {
        if (options != undefined) {
            $.extend(config, options);
        }
        table.reload('mainList', {
            url: '/UserManager/Load',
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
    var ztree = function () {
        var url = '/UserSession/GetOrgs';
        var zTreeObj;
        var setting = {
            view: { selectedMulti: false },
            data: {
                key: {
                    name: 'Name',
                    title: 'Name'
                },
                simpleData: {
                    enable: true,
                    idKey: 'Id',
                    pIdKey: 'ParentId',
                    rootPId: ""
                }
            },
            callback: {
                onClick: function (event, treeId, treeNode) {
                    currentPage = 1;
                    mainList({ OrgId: treeNode.Id });
                }
            }
        };
        var load = function () {
            $.getJSON(url, function (json) {
                zTreeObj = $.fn.zTree.init($("#tree"), setting);
                var newNode = { Name: "全部", Id: null, ParentId: "" };
                json.push(newNode);
                zTreeObj.addNodes(null, json);
                currentPage = 1;
                mainList({ OrgId: "" });
                zTreeObj.expandAll(false);

                var nodes = zTreeObj.getNodes();
                if (nodes.length > 0) {
                    for (var i = 0; i < nodes.length; i++) {
                        zTreeObj.expandNode(nodes[i], true, false, false);
                    }
                }
            });
        };
        load();
        return {
            reload: load
        }
    }();

    $("#tree").height( $("div.layui-table-view").height());

    //添加（编辑）对话框
    var editDlg = function() {
        var vm = new Vue({
            el: "#formEdit"
        });
        var title = "";
        var show = function (data) {
            var index = layer.open({
                title: title,
                area: ["500px", "420px"],
                type: 1,
                content: $('#divEdit'),
                success: function() {
                    vm.$set('$data', data);

                    $(":radio[name='Sex'][value='" + data.Sex + "']").prop("checked", "checked");
                    $("input:checkbox[name='Status']").prop("checked", data.Status == 1);
                    //下面这种方式适合单独开页面，不然上次选中的结果会对本次有影响
                   // $('input:checkbox[name="Status"][value="' + data.Status + '"]').prop('checked', true);
                    form.render();
                },
                end: mainList
            });
            var url = "/UserManager/AddOrUpdate";
            $("#divBtn").show();
            if (title === "编辑") {
                url = "/UserManager/AddOrUpdate"; //暂时和添加一个地址
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
            openauth.del("/UserManager/Delete",
                data.map(function (e) { return e.Id; }),
                mainList);
        },
        btnAdd: function () {  //添加
            editDlg.add();
        },
        btnEdit: function () {  //编辑
             var checkStatus = table.checkStatus('mainList')
               , data = checkStatus.data;
             if (data.length != 1) {
                 layer.msg("请选择编辑的行，且同时只能编辑一行");
                 return;
             }
             editDlg.update(data[0]);
        },
        search: function () {   //搜索
            currentPage = 1;
            mainList({ key: $('#key').val() });
        },
        btnRefresh: function () {
            mainList();
        },
        btnAccessModule: function () {
            var checkStatus = table.checkStatus('mainList')
               , data = checkStatus.data;
            if (data.length != 1) {
                toplayer.msg("请选择要分配的用户");
                return;
            }

            var index = toplayer.open({
                title: "为用户【" + data[0].Name + "】分配模块",
                type: 2,
                area: ['750px', '600px'],
                content: "/ModuleManager/Assign?type=UserModule&menuType=UserElement&id=" + data[0].Id,
                success: function(layero, index) {
                    
                }
            });
        },
        btnAccessRole: function () {
            var checkStatus = table.checkStatus('mainList')
               , data = checkStatus.data;
            if (data.length != 1) {
                toplayer.msg("请选择要分配的用户");
                return;
            }

            var index = toplayer.open({
                title: "为用户【"+ data[0].Name + "】分配角色",
                type: 2,
                area: ['750px', '600px'],
                content: "/RoleManager/Assign?type=UserRole&id=" + data[0].Id,
                success: function (layero, index) {

                }
            });
        },
        btnAssignReource: function () {
            var checkStatus = table.checkStatus('mainList')
                , data = checkStatus.data;
            if (data.length != 1) {
                toplayer.msg("请选择要分配的用户");
                return;
            }

            var index = toplayer.open({
                title: "为用户【" + data[0].Name + "】分配资源",
                type: 2,
                area: ['750px', '600px'],
                content: "/Resources/Assign?type=UserResource&id=" + data[0].Id,
                success: function (layero, index) {

                }
            });
        },
        btnResetPassword: function () {
            var checkStatus = table.checkStatus('mainList')
                , data = checkStatus.data;

            if (data.length <= 0) {
                layer.msg("请选择要重置密码的用户!");
                return;
            }

            layer.confirm('您确定要重置这些用户的密码吗？',
                function (index) {
                    var loadIndex = layer.load();

                    $.post("/UserManager/ResetPassword",
                        {
                            ids: data.map(function (e) { return e.Id; })
                        },
                        function (result) {
                            layer.close(loadIndex);
                            layer.msg(result.Message,
                                { time: 1000 },
                                function () {
                                    if (result.Code === 200) {
                                        mainList();
                                    }
                                });
                        },
                        "json");

                    layer.close(index);
                });
        },
        btnEnable: function () {
            var checkStatus = table.checkStatus('mainList')
                , data = checkStatus.data;

            if (data.length <= 0) {
                layer.msg("请选择要启用的用户!");
                return;
            }

            layer.confirm('您确定要启用这些用户吗？',
                function (index) {
                    var loadIndex = layer.load();

                    $.post("/UserManager/SetUserStatus",
                        {
                            ids: data.map(function (e) { return e.Id; }),
                            status: 1
                        },
                        function (result) {
                            layer.close(loadIndex);
                            layer.msg(result.Message,
                                { time: 1000 },
                                function () {
                                    if (result.Code === 200) {
                                        mainList();
                                    }
                                });
                        },
                        "json");

                    layer.close(index);
                });
        },
        btnDisable: function () {
            var checkStatus = table.checkStatus('mainList')
                , data = checkStatus.data;

            if (data.length <= 0) {
                layer.msg("请选择要禁用的用户!");
                return;
            }

            layer.confirm('您确定要禁用这些用户吗？',
                function (index) {
                    var loadIndex = layer.load();

                    $.post("/UserManager/SetUserStatus",
                        {
                            ids: data.map(function (e) { return e.Id; }),
                            status: 0
                        },
                        function (result) {
                            layer.close(loadIndex);
                            layer.msg(result.Message,
                                { time: 1000 },
                                function () {
                                    if (result.Code === 200) {
                                        mainList();
                                    }
                                });
                        },
                        "json");

                    layer.close(index);
                });
        }
    };

    $('.toolList .layui-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });

    //监听页面主按钮操作 end
})