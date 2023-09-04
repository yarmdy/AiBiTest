using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class SysUser:IdEntity
    {
        public SysUser()
        {
            BusClassifyCreateUsers = new HashSet<BusClassify>();
            BusClassifyDelUsers = new HashSet<BusClassify>();
            BusClassifyModifyUsers = new HashSet<BusClassify>();
            BusExampleCreateUsers = new HashSet<BusExample>();
            BusExampleDelUsers = new HashSet<BusExample>();
            BusExampleModifyUsers = new HashSet<BusExample>();
            BusExampleOptionCreateUsers = new HashSet<BusExampleOption>();
            BusExampleOptionDelUsers = new HashSet<BusExampleOption>();
            BusExampleOptionModifyUsers = new HashSet<BusExampleOption>();
            BusExampleQuestionCreateUsers = new HashSet<BusExampleQuestion>();
            BusExampleQuestionDelUsers = new HashSet<BusExampleQuestion>();
            BusExampleQuestionModifyUsers = new HashSet<BusExampleQuestion>();
            BusExampleResultCreateUsers = new HashSet<BusExampleResult>();
            BusExampleResultDelUsers = new HashSet<BusExampleResult>();
            BusExampleResultModifyUsers = new HashSet<BusExampleResult>();
            BusQuestionCreateUsers = new HashSet<BusQuestion>();
            BusQuestionDelUsers = new HashSet<BusQuestion>();
            BusQuestionModifyUsers = new HashSet<BusQuestion>();
            BusQuestionOptionCreateUsers = new HashSet<BusQuestionOption>();
            BusQuestionOptionDelUsers = new HashSet<BusQuestionOption>();
            BusQuestionOptionModifyUsers = new HashSet<BusQuestionOption>();
            BusTestPlanCreateUsers = new HashSet<BusTestPlan>();
            BusTestPlanDelUsers = new HashSet<BusTestPlan>();
            BusTestPlanExampleCreateUsers = new HashSet<BusTestPlanExample>();
            BusTestPlanExampleDelUsers = new HashSet<BusTestPlanExample>();
            BusTestPlanExampleModifyUsers = new HashSet<BusTestPlanExample>();
            BusTestPlanModifyUsers = new HashSet<BusTestPlan>();
            BusTestPlanUserCreateUsers = new HashSet<BusTestPlanUser>();
            BusTestPlanUserDelUsers = new HashSet<BusTestPlanUser>();
            BusTestPlanUserExampleCreateUsers = new HashSet<BusTestPlanUserExample>();
            BusTestPlanUserExampleDelUsers = new HashSet<BusTestPlanUserExample>();
            BusTestPlanUserExampleModifyUsers = new HashSet<BusTestPlanUserExample>();
            BusTestPlanUserExampleUsers = new HashSet<BusTestPlanUserExample>();
            BusTestPlanUserModifyUsers = new HashSet<BusTestPlanUser>();
            BusTestPlanUserUsers = new HashSet<BusTestPlanUser>();
            BusUserClassifyCreateUsers = new HashSet<BusUserClassify>();
            BusUserClassifyDelUsers = new HashSet<BusUserClassify>();
            BusUserClassifyModifyUsers = new HashSet<BusUserClassify>();
            BusUserClassifyUsers = new HashSet<BusUserClassify>();
            BusUserExampleCreateUsers = new HashSet<BusUserExample>();
            BusUserExampleDelUsers = new HashSet<BusUserExample>();
            BusUserExampleModifyUsers = new HashSet<BusUserExample>();
            BusUserExampleUsers = new HashSet<BusUserExample>();
            SysAttachmentCreateUsers = new HashSet<SysAttachment>();
            SysAttachmentDelUsers = new HashSet<SysAttachment>();
            SysAttachmentModifyUsers = new HashSet<SysAttachment>();
            SysFuncCreateUsers = new HashSet<SysFunc>();
            SysFuncDelUsers = new HashSet<SysFunc>();
            SysFuncModifyUsers = new HashSet<SysFunc>();
            SysRoleCreateUsers = new HashSet<SysRole>();
            SysRoleDelUsers = new HashSet<SysRole>();
            SysRoleFuncCreateUsers = new HashSet<SysRoleFunc>();
            SysRoleFuncDelUsers = new HashSet<SysRoleFunc>();
            SysRoleFuncModifyUsers = new HashSet<SysRoleFunc>();
            SysRoleModifyUsers = new HashSet<SysRole>();
            SysUserRoleCreateUsers = new HashSet<SysUserRole>();
            SysUserRoleDelUsers = new HashSet<SysUserRole>();
            SysUserRoleModifyUsers = new HashSet<SysUserRole>();
            SysUserRoleUsers = new HashSet<SysUserRole>();

            BusTestTempleteCreateUsers = new HashSet<BusTestTemplete>();
            BusTestTempleteModifyUsers = new HashSet<BusTestTemplete>();
            BusTestTempleteDelUsers = new HashSet<BusTestTemplete>();
            BusTestTempleteExampleCreateUsers = new HashSet<BusTestTempleteExample>();
            BusTestTempleteExampleModifyUsers = new HashSet<BusTestTempleteExample>();
            BusTestTempleteExampleDelUsers = new HashSet<BusTestTempleteExample>();

            BusUserTestTempleteCreateUsers = new HashSet<BusUserTestTemplete>();
            BusUserTestTempleteModifyUsers = new HashSet<BusUserTestTemplete>();
            BusUserTestTempleteDelUsers=new HashSet<BusUserTestTemplete>();
            BusUserTestTempleteUsers = new HashSet<BusUserTestTemplete>();
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 头像名称
        /// </summary>
        public string AvatarName { get; set; }
        /// <summary>
        /// 头像附件
        /// </summary>
        public int? AvatarId { get; set; }
        /// <summary>
        /// 1 被测者 2 组织测试者 4 代理商 -2147483648总管理员
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 1启用 0 禁用
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastTime { get; set; }
        /// <summary>
        /// 最后登录ip
        /// </summary>
        public int? LastIp { get; set; }
        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }
        public virtual SysAttachment Avatar { get; set; }
        public virtual ICollection<BusClassify> BusClassifyCreateUsers { get; set; }
        public virtual ICollection<BusClassify> BusClassifyDelUsers { get; set; }
        public virtual ICollection<BusClassify> BusClassifyModifyUsers { get; set; }
        public virtual ICollection<BusExample> BusExampleCreateUsers { get; set; }
        public virtual ICollection<BusExample> BusExampleDelUsers { get; set; }
        public virtual ICollection<BusExample> BusExampleModifyUsers { get; set; }
        public virtual ICollection<BusExampleOption> BusExampleOptionCreateUsers { get; set; }
        public virtual ICollection<BusExampleOption> BusExampleOptionDelUsers { get; set; }
        public virtual ICollection<BusExampleOption> BusExampleOptionModifyUsers { get; set; }
        public virtual ICollection<BusExampleQuestion> BusExampleQuestionCreateUsers { get; set; }
        public virtual ICollection<BusExampleQuestion> BusExampleQuestionDelUsers { get; set; }
        public virtual ICollection<BusExampleQuestion> BusExampleQuestionModifyUsers { get; set; }
        public virtual ICollection<BusExampleResult> BusExampleResultCreateUsers { get; set; }
        public virtual ICollection<BusExampleResult> BusExampleResultDelUsers { get; set; }
        public virtual ICollection<BusExampleResult> BusExampleResultModifyUsers { get; set; }
        public virtual ICollection<BusQuestion> BusQuestionCreateUsers { get; set; }
        public virtual ICollection<BusQuestion> BusQuestionDelUsers { get; set; }
        public virtual ICollection<BusQuestion> BusQuestionModifyUsers { get; set; }
        public virtual ICollection<BusQuestionOption> BusQuestionOptionCreateUsers { get; set; }
        public virtual ICollection<BusQuestionOption> BusQuestionOptionDelUsers { get; set; }
        public virtual ICollection<BusQuestionOption> BusQuestionOptionModifyUsers { get; set; }
        public virtual ICollection<BusTestPlan> BusTestPlanCreateUsers { get; set; }
        public virtual ICollection<BusTestPlan> BusTestPlanDelUsers { get; set; }
        public virtual ICollection<BusTestPlanExample> BusTestPlanExampleCreateUsers { get; set; }
        public virtual ICollection<BusTestPlanExample> BusTestPlanExampleDelUsers { get; set; }
        public virtual ICollection<BusTestPlanExample> BusTestPlanExampleModifyUsers { get; set; }
        public virtual ICollection<BusTestPlan> BusTestPlanModifyUsers { get; set; }
        public virtual ICollection<BusTestPlanUser> BusTestPlanUserCreateUsers { get; set; }
        public virtual ICollection<BusTestPlanUser> BusTestPlanUserDelUsers { get; set; }
        public virtual ICollection<BusTestPlanUserExample> BusTestPlanUserExampleCreateUsers { get; set; }
        public virtual ICollection<BusTestPlanUserExample> BusTestPlanUserExampleDelUsers { get; set; }
        public virtual ICollection<BusTestPlanUserExample> BusTestPlanUserExampleModifyUsers { get; set; }
        public virtual ICollection<BusTestPlanUserExample> BusTestPlanUserExampleUsers { get; set; }
        public virtual ICollection<BusTestPlanUser> BusTestPlanUserModifyUsers { get; set; }
        public virtual ICollection<BusTestPlanUser> BusTestPlanUserUsers { get; set; }
        public virtual ICollection<BusUserClassify> BusUserClassifyCreateUsers { get; set; }
        public virtual ICollection<BusUserClassify> BusUserClassifyDelUsers { get; set; }
        public virtual ICollection<BusUserClassify> BusUserClassifyModifyUsers { get; set; }
        public virtual ICollection<BusUserClassify> BusUserClassifyUsers { get; set; }
        public virtual ICollection<BusUserExample> BusUserExampleCreateUsers { get; set; }
        public virtual ICollection<BusUserExample> BusUserExampleDelUsers { get; set; }
        public virtual ICollection<BusUserExample> BusUserExampleModifyUsers { get; set; }
        public virtual ICollection<BusUserExample> BusUserExampleUsers { get; set; }
        public virtual ICollection<SysAttachment> SysAttachmentCreateUsers { get; set; }
        public virtual ICollection<SysAttachment> SysAttachmentDelUsers { get; set; }
        public virtual ICollection<SysAttachment> SysAttachmentModifyUsers { get; set; }
        public virtual ICollection<SysFunc> SysFuncCreateUsers { get; set; }
        public virtual ICollection<SysFunc> SysFuncDelUsers { get; set; }
        public virtual ICollection<SysFunc> SysFuncModifyUsers { get; set; }
        public virtual ICollection<SysRole> SysRoleCreateUsers { get; set; }
        public virtual ICollection<SysRole> SysRoleDelUsers { get; set; }
        public virtual ICollection<SysRoleFunc> SysRoleFuncCreateUsers { get; set; }
        public virtual ICollection<SysRoleFunc> SysRoleFuncDelUsers { get; set; }
        public virtual ICollection<SysRoleFunc> SysRoleFuncModifyUsers { get; set; }
        public virtual ICollection<SysRole> SysRoleModifyUsers { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoleCreateUsers { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoleDelUsers { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoleModifyUsers { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoleUsers { get; set; }

        public virtual ICollection<BusTestTemplete> BusTestTempleteCreateUsers { get; set; }
        public virtual ICollection<BusTestTemplete> BusTestTempleteModifyUsers { get; set; }
        public virtual ICollection<BusTestTemplete> BusTestTempleteDelUsers { get; set; }

        public virtual ICollection<BusTestTempleteExample> BusTestTempleteExampleCreateUsers { get; set; }
        public virtual ICollection<BusTestTempleteExample> BusTestTempleteExampleModifyUsers { get; set; }
        public virtual ICollection<BusTestTempleteExample> BusTestTempleteExampleDelUsers { get; set; }

        public virtual ICollection<BusUserTestTemplete> BusUserTestTempleteCreateUsers { get; set; }
        public virtual ICollection<BusUserTestTemplete> BusUserTestTempleteModifyUsers { get; set; }
        public virtual ICollection<BusUserTestTemplete> BusUserTestTempleteDelUsers { get; set; }
        public virtual ICollection<BusUserTestTemplete> BusUserTestTempleteUsers { get; set; }
    }
}
