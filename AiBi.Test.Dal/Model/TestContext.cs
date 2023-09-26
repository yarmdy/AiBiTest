using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace AiBi.Test.Dal.Model
{
    public partial class TestContext : DbContext
    {
        static TestContext()
        {
            Database.SetInitializer<TestContext>(null);
        }
        public TestContext()
            : base("Name=TestEntities")
        {
            // 关闭语义可空判断
            Configuration.UseDatabaseNullSemantics = true;

            //与变量的值为null比较
            //ef判断为null的时候，不能用变量比较：https://stackoverflow.com/questions/682429/how-can-i-query-for-null-values-in-entity-framework?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
            (this as IObjectContextAdapter).ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior = true;
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public TestContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public virtual DbSet<BusClassify> BusClassifies { get; set; }
        public virtual DbSet<BusExample> BusExamples { get; set; }
        public virtual DbSet<BusExampleOption> BusExampleOptions { get; set; }
        public virtual DbSet<BusExampleQuestion> BusExampleQuestions { get; set; }
        public virtual DbSet<BusExampleResult> BusExampleResults { get; set; }
        public virtual DbSet<BusQuestion> BusQuestions { get; set; }
        public virtual DbSet<BusQuestionOption> BusQuestionOptions { get; set; }
        public virtual DbSet<BusTestPlan> BusTestPlans { get; set; }
        public virtual DbSet<BusTestPlanExample> BusTestPlanExamples { get; set; }
        public virtual DbSet<BusTestPlanUser> BusTestPlanUsers { get; set; }
        public virtual DbSet<BusTestPlanUserExample> BusTestPlanUserExamples { get; set; }
        public virtual DbSet<BusUserClassify> BusUserClassifies { get; set; }
        public virtual DbSet<BusUserExample> BusUserExamples { get; set; }
        public virtual DbSet<SysAttachment> SysAttachments { get; set; }
        public virtual DbSet<SysFunc> SysFuncs { get; set; }
        public virtual DbSet<SysRole> SysRoles { get; set; }
        public virtual DbSet<SysRoleFunc> SysRoleFuncs { get; set; }
        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<SysUserRole> SysUserRoles { get; set; }

        public virtual DbSet<BusTestTemplate> BusTestTemplates { get; set; }
        public virtual DbSet<BusTestTemplateExample> BusTestTemplateExamples { get; set; }
        public virtual DbSet<BusUserTestTemplate> BusUserTestTemplates { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("test");

            modelBuilder.Entity<BusClassify>(entity =>
            {
                entity.ToTable("bus_Classify");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("名称");

                entity.Property(e => e.ParentId).HasComment("上级分类");

                entity.Property(e => e.SortNo).HasComment("排序");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusClassifyCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusClassifyDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusClassifyModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasOptional(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    ;
            });

            modelBuilder.Entity<BusExample>(entity =>
            {
                entity.ToTable("bus_Example");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.ClassifyId).HasComment("分类");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.DiscountPrice)
                    .HasColumnType("decimal")
                    .HasComment("优惠价格");

                entity.Property(e => e.Duration).HasComment("时长（分钟）");

                entity.Property(e => e.ImageId).HasComment("图片附件Id");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.Keys)
                    .HasMaxLength(400)
                    .HasComment("关键字 | 分割");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.NContent)
                    .HasMaxLength(4000)
                    .HasColumnName("NContent")
                    .HasComment("备注  给组织测试者");

                entity.Property(e => e.Note)
                    .HasMaxLength(4000)
                    .HasComment("说明  给被测者");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal")
                    .HasComment("价格");

                entity.Property(e => e.QuestionNum).HasComment("题数");

                entity.Property(e => e.Status).HasComment("0 创建中 1 创建完成 2已上架");

                entity.Property(e => e.SubClassifyId).HasComment("子类");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasComment("标题");

                entity.HasRequired(d => d.Classify)
                    .WithMany(p => p.BusExampleClassifies)
                    .HasForeignKey(d => d.ClassifyId)
                                        ;

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusExampleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusExampleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.Image)
                    .WithMany(p => p.BusExamples)
                    .HasForeignKey(d => d.ImageId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusExampleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasOptional(d => d.SubClassify)
                    .WithMany(p => p.BusExampleSubClassifies)
                    .HasForeignKey(d => d.SubClassifyId)
                    ;
            });

            modelBuilder.Entity<BusExampleOption>(entity =>
            {
                entity.HasKey(e => new { e.ExampleId, e.OptionId });

                entity.ToTable("bus_Example_Option");

                entity.Property(e => e.ExampleId).HasComment("测试");

                entity.Property(e => e.OptionId).HasComment("选项");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.Jump).HasComment("跳转");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.Score).HasComment("分值");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusExampleOptionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusExampleOptionDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasRequired(d => d.Example)
                    .WithMany(p => p.BusExampleOptions)
                    .HasForeignKey(d => d.ExampleId)
                                        ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusExampleOptionModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.Option)
                    .WithMany(p => p.BusExampleOptions)
                    .HasForeignKey(d => d.OptionId)
                                        ;
            });

            modelBuilder.Entity<BusExampleQuestion>(entity =>
            {
                entity.HasKey(e => new { e.ExampleId, e.QuestionId });

                entity.ToTable("bus_Example_Question");

                entity.Property(e => e.ExampleId).HasComment("测试");

                entity.Property(e => e.QuestionId).HasComment("问题");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.SortNo).HasComment("序号");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusExampleQuestionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusExampleQuestionDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasRequired(d => d.Example)
                    .WithMany(p => p.BusExampleQuestions)
                    .HasForeignKey(d => d.ExampleId)
                                        ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusExampleQuestionModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.Question)
                    .WithMany(p => p.BusExampleQuestions)
                    .HasForeignKey(d => d.QuestionId)
                                        ;
            });

            modelBuilder.Entity<BusExampleResult>(entity =>
            {
                entity.ToTable("bus_Example_Result");

                entity.Property(e => e.Id).HasComment("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasComment("代码");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.ExampleId).HasComment("用例Id");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.MaxScore).HasComment("最大分值");

                entity.Property(e => e.MinScore).HasComment("最小分值");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.NContent)
                    .HasMaxLength(4000)
                    .HasColumnName("NContent")
                    .HasComment("内容");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasComment("标题");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusExampleResultCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusExampleResultDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasRequired(d => d.Example)
                    .WithMany(p => p.BusExampleResults)
                    .HasForeignKey(d => d.ExampleId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusExampleResultModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasOptional(d => d.Image).WithMany(p=>p.BusExampleResults).HasForeignKey(d => d.ImageId);
            });

            modelBuilder.Entity<BusQuestion>(entity =>
            {
                entity.ToTable("bus_Question");

                entity.Property(e => e.Id).HasComment("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.ImageId).HasComment("图片 附件id");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.OptionNum).HasComment("选项数");

                entity.Property(e => e.Title)
                    .HasMaxLength(4000)
                    .HasComment("标题题面");
                entity.Property(e => e.NContent)
                    .HasMaxLength(4000)
                    .HasComment("总题面");

                entity.Property(e => e.Type).HasComment("1单选题 2多选题 3判断题");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusQuestionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusQuestionDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.Image)
                    .WithMany(p => p.BusQuestions)
                    .HasForeignKey(d => d.ImageId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusQuestionModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;
            });

            modelBuilder.Entity<BusQuestionOption>(entity =>
            {
                entity.ToTable("bus_Question_Option");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasComment("按钮文字");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.QuestionId).HasComment("问题Id");

                entity.Property(e => e.Remark)
                    .HasMaxLength(400)
                    .HasComment("简介");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusQuestionOptionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusQuestionOptionDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusQuestionOptionModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.Question)
                    .WithMany(p => p.BusQuestionOptions)
                    .HasForeignKey(d => d.QuestionId)
                    ;
            });

            modelBuilder.Entity<BusTestPlan>(entity =>
            {
                entity.ToTable("bus_TestPlan");

                entity.Property(e => e.Id).HasComment("id");

                entity.Property(e => e.CanPause).HasComment("可以暂停");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasComment("结束时间");

                entity.Property(e => e.ExampleNum).HasComment("用例数量");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("计划名称");

                entity.Property(e => e.QuestionNum).HasComment("题数");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasComment("开始时间");

                entity.Property(e => e.Status).HasComment("0 创建中 1 已创建 2 已发布");

                entity.Property(e => e.UserNum).HasComment("学员数");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusTestPlanCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusTestPlanDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusTestPlanModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;
                entity.HasRequired(d => d.Template)
                    .WithMany(p => p.BusTestPlans)
                    .HasForeignKey(d=>d.TemplateId)
                    ;
            });

            modelBuilder.Entity<BusTestPlanExample>(entity =>
            {
                entity.HasKey(e => new { e.PlanId, e.ExampleId });

                entity.ToTable("bus_TestPlan_Example");

                entity.Property(e => e.PlanId).HasComment("计划id");

                entity.Property(e => e.ExampleId).HasComment("用例id");

                entity.Property(e => e.CanPause).HasComment("可以暂停");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.Duration).HasComment("时长（分钟） 0不限");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.SortNo).HasComment("排序号");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusTestPlanExampleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusTestPlanExampleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasRequired(d => d.Example)
                    .WithMany(p => p.BusTestPlanExamples)
                    .HasForeignKey(d => d.ExampleId)
                                        ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusTestPlanExampleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.Plan)
                    .WithMany(p => p.BusTestPlanExamples)
                    .HasForeignKey(d => d.PlanId)
                                        ;
            });

            modelBuilder.Entity<BusTestPlanUser>(entity =>
            {
                entity.HasKey(e => new { e.PlanId, e.UserId });

                entity.ToTable("bus_TestPlan_User");

                entity.Property(e => e.PlanId).HasComment("计划id");

                entity.Property(e => e.UserId).HasComment("用户id");

                entity.Property(e => e.BeginTime)
                    .HasColumnType("datetime")
                    .HasComment("开始时间");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.CurrentExample).HasComment("当前实例");

                entity.Property(e => e.CurrentQuestion).HasComment("当前问题");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.Duration).HasComment("已用时长");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasComment("结束时间");

                entity.Property(e => e.FinishExample).HasComment("完成的实例");

                entity.Property(e => e.FinishQuestion).HasComment("完成的问题");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.Remark)
                    .HasMaxLength(4000)
                    .HasComment("备注");

                entity.Property(e => e.ResultCode)
                    .HasMaxLength(100)
                    .HasComment("结果代码 | 分割");

                entity.Property(e => e.Status).HasComment("进度 0 创建 1 进入系统 2 开始答题 3 离开 4 答题完成");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusTestPlanUserCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusTestPlanUserDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusTestPlanUserModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.Plan)
                    .WithMany(p => p.BusTestPlanUsers)
                    .HasForeignKey(d => d.PlanId)
                                        ;

                entity.HasRequired(d => d.User)
                    .WithMany(p => p.BusTestPlanUserUsers)
                    .HasForeignKey(d => d.UserId)
                                        ;
            });

            modelBuilder.Entity<BusTestPlanUserExample>(entity =>
            {
                entity.HasKey(e => new { e.PlanId, e.UserId, e.ExampleId });

                entity.ToTable("bus_TestPlan_User_Example");

                entity.Property(e => e.PlanId).HasComment("计划id");

                entity.Property(e => e.UserId).HasComment("用户id");

                entity.Property(e => e.ExampleId).HasComment("实例id");

                entity.Property(e => e.BeginTime)
                    .HasColumnType("datetime")
                    .HasComment("开始时间");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.CurrentQuestion).HasComment("当前问题");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.Duration).HasComment("已用时长");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasComment("结束时间");

                entity.Property(e => e.FinishQuestion).HasComment("完成的问题");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.Remark)
                    .HasMaxLength(4000)
                    .HasComment("备注");

                entity.Property(e => e.ResultCode)
                    .HasMaxLength(10)
                    .HasComment("结果代码");

                entity.Property(e => e.ResultId).HasComment("结果id");

                entity.Property(e => e.Status).HasComment("进度 0 未答 1 正在答题 2 答完");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusTestPlanUserExampleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusTestPlanUserExampleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasRequired(d => d.Example)
                    .WithMany(p => p.BusTestPlanUserExamples)
                    .HasForeignKey(d => d.ExampleId)
                                        ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusTestPlanUserExampleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.Plan)
                    .WithMany(p => p.BusTestPlanUserExamples)
                    .HasForeignKey(d => d.PlanId)
                                        ;

                entity.HasOptional(d => d.Result)
                    .WithMany(p => p.BusTestPlanUserExamples)
                    .HasForeignKey(d => d.ResultId)
                    ;

                entity.HasRequired(d => d.User)
                    .WithMany(p => p.BusTestPlanUserExampleUsers)
                    .HasForeignKey(d => d.UserId)
                                        ;
            });

            modelBuilder.Entity<BusUserClassify>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ClassifyId });

                entity.ToTable("bus_User_Classify");

                entity.Property(e => e.UserId).HasComment("用户");

                entity.Property(e => e.ClassifyId).HasComment("分类");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.ExpireTime)
                    .HasColumnType("datetime")
                    .HasComment("过期时间");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.HasRequired(d => d.Classify)
                    .WithMany(p => p.BusUserClassifies)
                    .HasForeignKey(d => d.ClassifyId)
                                        ;

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusUserClassifyCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusUserClassifyDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusUserClassifyModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.User)
                    .WithMany(p => p.BusUserClassifyUsers)
                    .HasForeignKey(d => d.UserId)
                                        ;
            });

            modelBuilder.Entity<BusUserExample>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ExampleId });

                entity.ToTable("bus_User_Example");

                entity.Property(e => e.UserId).HasComment("用户");

                entity.Property(e => e.ExampleId).HasComment("用例");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.ExpireTime)
                    .HasColumnType("datetime")
                    .HasComment("过期时间");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusUserExampleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusUserExampleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasRequired(d => d.Example)
                    .WithMany(p => p.BusUserExamples)
                    .HasForeignKey(d => d.ExampleId)
                                        ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusUserExampleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.User)
                    .WithMany(p => p.BusUserExampleUsers)
                    .HasForeignKey(d => d.UserId)
                                        ;
            });

            modelBuilder.Entity<SysAttachment>(entity =>
            {
                entity.ToTable("sys_Attachment");

                entity.Property(e => e.Id).HasComment("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.Ext)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("扩展名");

                entity.Property(e => e.FileName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasComment("文件名");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.Module).HasComment("0 未知 100 问题图片 200");

                entity.Property(e => e.Name)
                    .HasMaxLength(400)
                    .HasComment("名称");

                entity.Property(e => e.Path)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasComment("路径");

                entity.Property(e => e.Status).HasComment("0创建 1应用 每天状态为0的会被自动清掉以释放空间");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.SysAttachmentCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.SysAttachmentDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.SysAttachmentModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;
            });

            modelBuilder.Entity<SysFunc>(entity =>
            {
                entity.ToTable("sys_Func");

                entity.Property(e => e.Id)
                    .HasComment("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("功能代码");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("功能名称");

                entity.Property(e => e.ParentId).HasComment("上级");

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .HasComment("备注");

                entity.Property(e => e.Route)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("路由");

                entity.Property(e => e.Type).HasComment("0 大功能 1按钮或小功能");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.SysFuncCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.SysFuncDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.SysFuncModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.ToTable("sys_Role");

                entity.Property(e => e.Id).HasComment("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("角色名称");

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .HasComment("备注");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.SysRoleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.SysRoleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.SysRoleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;
            });

            modelBuilder.Entity<SysRoleFunc>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FuncId });

                entity.ToTable("sys_Role_Func");

                entity.Property(e => e.RoleId).HasComment("角色id");

                entity.Property(e => e.FuncId).HasComment("权限id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.SysRoleFuncCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.SysRoleFuncDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasRequired(d => d.Func)
                    .WithMany(p => p.SysRoleFuncs)
                    .HasForeignKey(d => d.FuncId)
                                        ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.SysRoleFuncModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.Role)
                    .WithMany(p => p.SysRoleFuncs)
                    .HasForeignKey(d => d.RoleId)
                                        ;
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.ToTable("sys_User");

                entity.Property(e => e.Id).HasComment("主键");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .HasComment("账号");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.ExpireTime)
                    .HasColumnType("datetime")
                    .HasComment("失效时间");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.LastIp)
                    .HasColumnName("LastIP")
                    .HasComment("最后登录ip");

                entity.Property(e => e.LastTime)
                    .HasColumnType("datetime")
                    .HasComment("最后登录时间");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("手机号");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("用户名");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("密码");

                entity.Property(e => e.Status).HasComment("1启用 0 禁用");

                entity.Property(e => e.Type).HasComment("1 被测者 2 组织测试者 4 代理商 -2147483648总管理员");

                entity.Property(e => e.AvatarName).HasMaxLength(50).IsUnicode(false);

                entity.HasOptional(a=>a.Avatar).WithMany(a=>a.SysUsers).HasForeignKey(a=>a.AvatarId);
            });

            modelBuilder.Entity<SysUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("sys_User_Role");

                entity.Property(e => e.UserId).HasComment("角色id");

                entity.Property(e => e.RoleId).HasComment("权限id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.SysUserRoleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.SysUserRoleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.SysUserRoleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.Role)
                    .WithMany(p => p.SysUserRoles)
                    .HasForeignKey(d => d.RoleId)
                                        ;

                entity.HasRequired(d => d.User)
                    .WithMany(p => p.SysUserRoleUsers)
                    .HasForeignKey(d => d.UserId)
                                        ;
            });

            modelBuilder.Entity<BusTestTemplate>(entity =>
            {
                entity.ToTable("bus_TestTemplate");

                entity.Property(e => e.Id).HasComment("Id");

                entity.Property(e => e.ClassifyId).HasComment("分类");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.DiscountPrice)
                    .HasColumnType("decimal")
                    .HasComment("优惠价格");

                entity.Property(e => e.Duration).HasComment("时长（分钟）");

                entity.Property(e => e.ImageId).HasComment("图片附件Id");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.Keys)
                    .HasMaxLength(400)
                    .HasComment("关键字 | 分割");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.NContent)
                    .HasMaxLength(4000)
                    .HasColumnName("NContent")
                    .HasComment("备注  给组织测试者");

                entity.Property(e => e.Note)
                    .HasMaxLength(4000)
                    .HasComment("说明  给被测者");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal")
                    .HasComment("价格");

                entity.Property(e => e.ExampleNum).HasComment("实例数");
                entity.Property(e => e.QuestionNum).HasComment("题数");

                entity.Property(e => e.Status).HasComment("0 创建中 1 创建完成 2已上架");

                entity.Property(e => e.SubClassifyId).HasComment("子类");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasComment("标题");

                entity.HasRequired(d => d.Classify)
                    .WithMany(p => p.BusTestTemplateClassifies)
                    .HasForeignKey(d => d.ClassifyId)
                                        ;

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusTestTemplateCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusTestTemplateDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasOptional(d => d.Image)
                    .WithMany(p => p.BusTestTemplates)
                    .HasForeignKey(d => d.ImageId)
                    ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusTestTemplateModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasOptional(d => d.SubClassify)
                    .WithMany(p => p.BusTestTemplateSubClassifies)
                    .HasForeignKey(d => d.SubClassifyId)
                    ;

                
            });
            modelBuilder.Entity<BusTestTemplateExample>(entity =>
            {
                entity.HasKey(e => new { e.TemplateId, e.ExampleId });

                entity.ToTable("bus_TestTemplate_Example");

                entity.Property(e => e.TemplateId).HasComment("模板id");

                entity.Property(e => e.ExampleId).HasComment("用例id");

                entity.Property(e => e.CanPause).HasComment("可以暂停");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.Duration).HasComment("时长（分钟） 0不限");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.Property(e => e.SortNo).HasComment("排序号");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusTestTemplateExampleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusTestTemplateExampleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasRequired(d => d.Example)
                    .WithMany(p => p.BusTestTemplateExamples)
                    .HasForeignKey(d => d.ExampleId)
                                        ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusTestTemplateExampleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.Example)
                    .WithMany(p => p.BusTestTemplateExamples)
                    .HasForeignKey(d => d.ExampleId)
                                        ;
            });

            modelBuilder.Entity<BusUserTestTemplate>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TemplateId });

                entity.ToTable("bus_User_TestTemplate");

                entity.Property(e => e.UserId).HasComment("用户");

                entity.Property(e => e.TemplateId).HasComment("模板");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");

                entity.Property(e => e.ExpireTime)
                    .HasColumnType("datetime")
                    .HasComment("过期时间");

                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusUserTestTemplateCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusUserTestTemplateDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;

                entity.HasRequired(d => d.Template)
                    .WithMany(p => p.BusUserTestTemplates)
                    .HasForeignKey(d => d.TemplateId)
                                        ;

                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusUserTestTemplateModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.User)
                    .WithMany(p => p.BusUserTestTemplateUsers)
                    .HasForeignKey(d => d.UserId)
                                        ;
            });

            modelBuilder.Entity<BusUserInfo>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.OwnerId });
                entity.ToTable("bus_UserInfo");

                entity.Property(e => e.RealName)
                    .HasMaxLength(50)
                    .HasColumnName("RealName");

                entity.Property(e => e.RealName)
                    .HasMaxLength(50)
                    .HasColumnName("RealName");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date");

                entity.Property(e => e.IdCardNo)
                    .HasMaxLength(50)
                    .HasColumnName("IdCardNo");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .HasColumnName("UnitName");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("创建时间");

                entity.Property(e => e.CreateUserId).HasComment("创建人");

                entity.Property(e => e.DelTime)
                    .HasColumnType("datetime")
                    .HasComment("删除时间");

                entity.Property(e => e.DelUserId).HasComment("删除人");


                entity.Property(e => e.IsDel).HasComment("是否删除");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasComment("修改时间");

                entity.Property(e => e.ModifyUserId).HasComment("修改人");

                entity.HasRequired(d => d.CreateUser)
                    .WithMany(p => p.BusUserInfoCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                                        ;

                entity.HasOptional(d => d.DelUser)
                    .WithMany(p => p.BusUserInfoDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    ;


                entity.HasOptional(d => d.ModifyUser)
                    .WithMany(p => p.BusUserInfoModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    ;

                entity.HasRequired(d => d.User).WithMany(p => p.BusUserInfoUsers).HasForeignKey(d => d.UserId);
                entity.HasRequired(d => d.Owner).WithMany(p => p.BusUserInfoOwners).HasForeignKey(d => d.OwnerId);
            });

        }

    }

    public static class DbModelBuilderEx {
        public static EntityTypeConfiguration<T> Entity<T>(this DbModelBuilder builder, Action<EntityTypeConfiguration<T>> func)  where T : class
        { 
            var entity = builder.Entity<T>();
            func(entity);
            return entity;
        }

        public static PrimitivePropertyConfiguration HasComment(this PrimitivePropertyConfiguration prop,string comment)
        {
            return prop;
        }
        public static PrimitivePropertyConfiguration HasDefaultValueSql(this PrimitivePropertyConfiguration prop,string comment)
        {
            return prop;
        }
        public static PrimitivePropertyConfiguration HasName(this PrimitivePropertyConfiguration prop,string comment)
        {
            return prop;
        }
        public static PrimitivePropertyConfiguration HasConstraintName(this PrimitivePropertyConfiguration prop,string comment)
        {
            return prop;
        }

    }
}
