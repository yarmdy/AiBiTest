using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AiBi.Dal.Model
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BusClassify> BusClassifies { get; set; } = null!;
        public virtual DbSet<BusExample> BusExamples { get; set; } = null!;
        public virtual DbSet<BusExampleOption> BusExampleOptions { get; set; } = null!;
        public virtual DbSet<BusExampleQuestion> BusExampleQuestions { get; set; } = null!;
        public virtual DbSet<BusExampleResult> BusExampleResults { get; set; } = null!;
        public virtual DbSet<BusQuestion> BusQuestions { get; set; } = null!;
        public virtual DbSet<BusQuestionOption> BusQuestionOptions { get; set; } = null!;
        public virtual DbSet<BusTestPlan> BusTestPlans { get; set; } = null!;
        public virtual DbSet<BusTestPlanExample> BusTestPlanExamples { get; set; } = null!;
        public virtual DbSet<BusTestPlanUser> BusTestPlanUsers { get; set; } = null!;
        public virtual DbSet<BusTestPlanUserExample> BusTestPlanUserExamples { get; set; } = null!;
        public virtual DbSet<BusUserClassify> BusUserClassifies { get; set; } = null!;
        public virtual DbSet<BusUserExample> BusUserExamples { get; set; } = null!;
        public virtual DbSet<SysAttachment> SysAttachments { get; set; } = null!;
        public virtual DbSet<SysFunc> SysFuncs { get; set; } = null!;
        public virtual DbSet<SysRole> SysRoles { get; set; } = null!;
        public virtual DbSet<SysRoleFunc> SysRoleFuncs { get; set; } = null!;
        public virtual DbSet<SysUser> SysUsers { get; set; } = null!;
        public virtual DbSet<SysUserRole> SysUserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=123.56.118.205;Initial Catalog=AiBi_Test;User=test;Password=tesT!@#4");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusClassifyCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Classify_sys_User2");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusClassifyDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_Classify_sys_User");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusClassifyModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_Classify_sys_User1");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_bus_Classify_bus_Classify");
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
                    .HasColumnType("decimal(18, 2)")
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

                entity.Property(e => e.Ncontent)
                    .HasMaxLength(4000)
                    .HasColumnName("NContent")
                    .HasComment("备注  给组织测试者");

                entity.Property(e => e.Note)
                    .HasMaxLength(4000)
                    .HasComment("说明  给被测者");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("价格");

                entity.Property(e => e.QuestionNum).HasComment("题数");

                entity.Property(e => e.Status).HasComment("0 创建中 1 创建完成 2已上架");

                entity.Property(e => e.SubClassifyId).HasComment("子类");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasComment("标题");

                entity.HasOne(d => d.Classify)
                    .WithMany(p => p.BusExampleClassifies)
                    .HasForeignKey(d => d.ClassifyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Example_bus_Classify");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusExampleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Example_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusExampleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_Example_sys_User2");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.BusExamples)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_bus_Example_sys_Attachment");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusExampleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_Example_sys_User1");

                entity.HasOne(d => d.SubClassify)
                    .WithMany(p => p.BusExampleSubClassifies)
                    .HasForeignKey(d => d.SubClassifyId)
                    .HasConstraintName("FK_bus_Example_bus_Classify1");
            });

            modelBuilder.Entity<BusExampleOption>(entity =>
            {
                entity.HasKey(e => new { e.ExampleId, e.OptionId })
                    .HasName("PK_bus_ExampleOption");

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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusExampleOptionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Example_Option_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusExampleOptionDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_Example_Option_sys_User2");

                entity.HasOne(d => d.Example)
                    .WithMany(p => p.BusExampleOptions)
                    .HasForeignKey(d => d.ExampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Example_Option_bus_Example");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusExampleOptionModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_Example_Option_sys_User1");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.BusExampleOptions)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Example_Option_bus_Question_Option");
            });

            modelBuilder.Entity<BusExampleQuestion>(entity =>
            {
                entity.HasKey(e => new { e.ExampleId, e.QuestionId })
                    .HasName("PK_bus_ExampleQuestion");

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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusExampleQuestionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Example_Question_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusExampleQuestionDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_Example_Question_sys_User2");

                entity.HasOne(d => d.Example)
                    .WithMany(p => p.BusExampleQuestions)
                    .HasForeignKey(d => d.ExampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Example_Question_bus_Example");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusExampleQuestionModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_Example_Question_sys_User1");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.BusExampleQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Example_Question_bus_Question");
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

                entity.Property(e => e.Ncontent)
                    .HasMaxLength(4000)
                    .HasColumnName("NContent")
                    .HasComment("内容");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasComment("标题");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusExampleResultCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Example_Result_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusExampleResultDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_Example_Result_sys_User2");

                entity.HasOne(d => d.Example)
                    .WithMany(p => p.BusExampleResults)
                    .HasForeignKey(d => d.ExampleId)
                    .HasConstraintName("FK_bus_Example_Result_bus_Example");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusExampleResultModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_Example_Result_sys_User1");
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

                entity.Property(e => e.Type).HasComment("1单选题 2多选题 3判断题");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusQuestionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Question_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusQuestionDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_Question_sys_User2");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.BusQuestions)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_bus_Question_sys_Attachment");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusQuestionModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_Question_sys_User1");
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusQuestionOptionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_Question_Option_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusQuestionOptionDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_Question_Option_sys_User2");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusQuestionOptionModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_Question_Option_sys_User1");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.BusQuestionOptions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_bus_Question_Option_bus_Question");
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusTestPlanCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusTestPlanDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_TestPlan_sys_User2");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusTestPlanModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_TestPlan_sys_User1");
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusTestPlanExampleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_Example_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusTestPlanExampleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_TestPlan_Example_sys_User2");

                entity.HasOne(d => d.Example)
                    .WithMany(p => p.BusTestPlanExamples)
                    .HasForeignKey(d => d.ExampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_Example_bus_Example");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusTestPlanExampleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_TestPlan_Example_sys_User1");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.BusTestPlanExamples)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_Example_bus_TestPlan");
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusTestPlanUserCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_User_sys_User1");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusTestPlanUserDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_TestPlan_User_sys_User3");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusTestPlanUserModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_TestPlan_User_sys_User2");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.BusTestPlanUsers)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_User_bus_TestPlan");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BusTestPlanUserUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_User_sys_User");
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusTestPlanUserExampleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_User_Example_sys_User1");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusTestPlanUserExampleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_TestPlan_User_Example_sys_User3");

                entity.HasOne(d => d.Example)
                    .WithMany(p => p.BusTestPlanUserExamples)
                    .HasForeignKey(d => d.ExampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_User_Example_bus_Example");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusTestPlanUserExampleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_TestPlan_User_Example_sys_User2");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.BusTestPlanUserExamples)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_User_Example_bus_TestPlan");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.BusTestPlanUserExamples)
                    .HasForeignKey(d => d.ResultId)
                    .HasConstraintName("FK_bus_TestPlan_User_Example_bus_Example_Result");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BusTestPlanUserExampleUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_TestPlan_User_Example_sys_User");
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

                entity.HasOne(d => d.Classify)
                    .WithMany(p => p.BusUserClassifies)
                    .HasForeignKey(d => d.ClassifyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_User_Classify_bus_Classify");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusUserClassifyCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_User_Classify_sys_User1");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusUserClassifyDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_User_Classify_sys_User3");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusUserClassifyModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_User_Classify_sys_User2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BusUserClassifyUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_User_Classify_sys_User");
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BusUserExampleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_User_Example_sys_User1");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.BusUserExampleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_bus_User_Example_sys_User3");

                entity.HasOne(d => d.Example)
                    .WithMany(p => p.BusUserExamples)
                    .HasForeignKey(d => d.ExampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_User_Example_bus_Example");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BusUserExampleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_bus_User_Example_sys_User2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BusUserExampleUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bus_User_Example_sys_User");
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.SysAttachmentCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sys_Attachment_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.SysAttachmentDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_sys_Attachment_sys_User2");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.SysAttachmentModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_sys_Attachment_sys_User1");
            });

            modelBuilder.Entity<SysFunc>(entity =>
            {
                entity.ToTable("sys_Func");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.SysFuncCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sys_Func_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.SysFuncDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_sys_Func_sys_User2");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.SysFuncModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_sys_Func_sys_User1");
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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.SysRoleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sys_Role_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.SysRoleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_sys_Role_sys_User2");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.SysRoleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_sys_Role_sys_User1");
            });

            modelBuilder.Entity<SysRoleFunc>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FuncId })
                    .HasName("PK_sys_RoleFunc");

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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.SysRoleFuncCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sys_Role_Func_sys_User");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.SysRoleFuncDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_sys_Role_Func_sys_User2");

                entity.HasOne(d => d.Func)
                    .WithMany(p => p.SysRoleFuncs)
                    .HasForeignKey(d => d.FuncId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sys_Role_Func_sys_Func");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.SysRoleFuncModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_sys_Role_Func_sys_User1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.SysRoleFuncs)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sys_Role_Func_sys_Role");
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
            });

            modelBuilder.Entity<SysUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_sys_UserRole");

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

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.SysUserRoleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sys_User_Role_sys_User1");

                entity.HasOne(d => d.DelUser)
                    .WithMany(p => p.SysUserRoleDelUsers)
                    .HasForeignKey(d => d.DelUserId)
                    .HasConstraintName("FK_sys_User_Role_sys_User3");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.SysUserRoleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_sys_User_Role_sys_User2");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.SysUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sys_User_Role_sys_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SysUserRoleUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sys_User_Role_sys_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
