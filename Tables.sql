USE [AiBi_Test]
GO
/****** Object:  Table [test].[bus_Classify]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_Classify](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ParentId] [int] NULL,
	[SortNo] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_Classify] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_Example]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_Example](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[ClassifyId] [int] NOT NULL,
	[SubClassifyId] [int] NULL,
	[Keys] [nvarchar](400) NULL,
	[QuestionNum] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[ImageId] [int] NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DiscountPrice] [decimal](18, 2) NULL,
	[Note] [nvarchar](4000) NULL,
	[NContent] [nvarchar](4000) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_Example] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_Example_Option]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_Example_Option](
	[ExampleId] [int] NOT NULL,
	[OptionId] [int] NOT NULL,
	[Score] [int] NOT NULL,
	[Jump] [int] NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_ExampleOption] PRIMARY KEY CLUSTERED 
(
	[ExampleId] ASC,
	[OptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_Example_Question]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_Example_Question](
	[ExampleId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[SortNo] [int] NOT NULL,
	[SortNo2] [int] NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_ExampleQuestion] PRIMARY KEY CLUSTERED 
(
	[ExampleId] ASC,
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_Example_Result]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_Example_Result](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExampleId] [int] NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[NContent] [nvarchar](4000) NOT NULL,
	[MinScore] [int] NOT NULL,
	[MaxScore] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_Example_Result] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_Question]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](4000) NOT NULL,
	[ImageId] [int] NULL,
	[Type] [int] NOT NULL,
	[OptionNum] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_Question] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_Question_Option]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_Question_Option](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Remark] [nvarchar](400) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_Question_Option] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_TestPlan]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_TestPlan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[TempleteId] [int] NOT NULL,
	[CanPause] [bit] NOT NULL,
	[ExampleNum] [int] NOT NULL,
	[QuestionNum] [int] NOT NULL,
	[UserNum] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_TestPlan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_TestPlan_Example]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_TestPlan_Example](
	[PlanId] [int] NOT NULL,
	[ExampleId] [int] NOT NULL,
	[SortNo] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[CanPause] [bit] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_TestPlan_Example] PRIMARY KEY CLUSTERED 
(
	[PlanId] ASC,
	[ExampleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_TestPlan_User]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_TestPlan_User](
	[PlanId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CurrentExample] [int] NULL,
	[FinishExample] [int] NOT NULL,
	[CurrentQuestion] [int] NULL,
	[FinishQuestion] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[BeginTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[ResultCode] [nvarchar](100) NULL,
	[Remark] [nvarchar](4000) NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_TestPlan_User] PRIMARY KEY CLUSTERED 
(
	[PlanId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_TestPlan_User_Example]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_TestPlan_User_Example](
	[PlanId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ExampleId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CurrentQuestion] [int] NULL,
	[FinishQuestion] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[ResultCode] [nvarchar](10) NULL,
	[ResultId] [int] NULL,
	[BeginTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Remark] [nvarchar](4000) NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_TestPlan_User_Example] PRIMARY KEY CLUSTERED 
(
	[PlanId] ASC,
	[UserId] ASC,
	[ExampleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_TestTemplete]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_TestTemplete](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[ClassifyId] [int] NOT NULL,
	[SubClassifyId] [int] NULL,
	[Keys] [nvarchar](400) NULL,
	[ExampleNum] [int] NOT NULL,
	[QuestionNum] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[ImageId] [int] NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DiscountPrice] [decimal](18, 2) NULL,
	[Note] [nvarchar](4000) NULL,
	[NContent] [nvarchar](4000) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_TestTemplete_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_TestTemplete_Example]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_TestTemplete_Example](
	[TempleteId] [int] NOT NULL,
	[ExampleId] [int] NOT NULL,
	[SortNo] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[CanPause] [bit] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_TestTemplete_Example] PRIMARY KEY CLUSTERED 
(
	[TempleteId] ASC,
	[ExampleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_User_Classify]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_User_Classify](
	[UserId] [int] NOT NULL,
	[ClassifyId] [int] NOT NULL,
	[ExpireTime] [datetime] NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_User_Classify] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ClassifyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_User_Example]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_User_Example](
	[UserId] [int] NOT NULL,
	[ExampleId] [int] NOT NULL,
	[ExpireTime] [datetime] NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_User_Example] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ExampleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_User_TestTemplete]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_User_TestTemplete](
	[UserId] [int] NOT NULL,
	[TempleteId] [int] NOT NULL,
	[ExpireTime] [datetime] NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_User_TestTemplete] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[TempleteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[sys_Attachment]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[sys_Attachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](400) NOT NULL,
	[Path] [varchar](400) NOT NULL,
	[FileName] [varchar](64) NOT NULL,
	[Ext] [varchar](50) NOT NULL,
	[Module] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_sys_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[sys_Func]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[sys_Func](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Route] [varchar](50) NOT NULL,
	[Type] [int] NOT NULL,
	[ParentId] [int] NULL,
	[Remark] [nvarchar](150) NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_sys_Func] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[sys_Role]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[sys_Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Remark] [nvarchar](150) NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_sys_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[sys_Role_Func]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[sys_Role_Func](
	[RoleId] [int] NOT NULL,
	[FuncId] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_sys_RoleFunc] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[FuncId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[sys_User]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[sys_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Account] [nvarchar](50) NOT NULL,
	[Mobile] [varchar](50) NULL,
	[Password] [varchar](50) NOT NULL,
	[AvatarName] [varchar](50) NULL,
	[AvatarId] [int] NULL,
	[Type] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[LastTime] [datetime] NULL,
	[LastIP] [int] NULL,
	[ExpireTime] [datetime] NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_sys_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[sys_User_Role]    Script Date: 2023/9/10 21:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[sys_User_Role](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_sys_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [test].[bus_Classify] ADD  CONSTRAINT [DF_bus_Classify_SortNo]  DEFAULT ((0)) FOR [SortNo]
GO
ALTER TABLE [test].[bus_Classify] ADD  CONSTRAINT [DF_bus_Classify_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_Classify] ADD  CONSTRAINT [DF_bus_Classify_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_Example] ADD  CONSTRAINT [DF_bus_Example_Price]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [test].[bus_Example] ADD  CONSTRAINT [DF_bus_Example_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_Example] ADD  CONSTRAINT [DF_bus_Example_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_Example_Option] ADD  CONSTRAINT [DF_bus_ExampleOption_Score]  DEFAULT ((0)) FOR [Score]
GO
ALTER TABLE [test].[bus_Example_Option] ADD  CONSTRAINT [DF_bus_Example_Option_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_Example_Option] ADD  CONSTRAINT [DF_bus_Example_Option_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_Example_Question] ADD  CONSTRAINT [DF_bus_Example_Question_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_Example_Question] ADD  CONSTRAINT [DF_bus_Example_Question_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_Example_Result] ADD  CONSTRAINT [DF_bus_Example_Result_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_Example_Result] ADD  CONSTRAINT [DF_bus_Example_Result_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_Question] ADD  CONSTRAINT [DF_bus_Question_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_Question] ADD  CONSTRAINT [DF_bus_Question_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_Question_Option] ADD  CONSTRAINT [DF_bus_Question_Option_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_Question_Option] ADD  CONSTRAINT [DF_bus_Question_Option_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_TestPlan] ADD  CONSTRAINT [DF_bus_TestPlan_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_TestPlan] ADD  CONSTRAINT [DF_bus_TestPlan_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_TestPlan_Example] ADD  CONSTRAINT [DF_bus_TestPlan_Example_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_TestPlan_Example] ADD  CONSTRAINT [DF_bus_TestPlan_Example_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_TestPlan_User] ADD  CONSTRAINT [DF_bus_TestPlan_User_FinishExample]  DEFAULT ((0)) FOR [FinishExample]
GO
ALTER TABLE [test].[bus_TestPlan_User] ADD  CONSTRAINT [DF_bus_TestPlan_User_FinishQuestion]  DEFAULT ((0)) FOR [FinishQuestion]
GO
ALTER TABLE [test].[bus_TestPlan_User] ADD  CONSTRAINT [DF_bus_TestPlan_User_Duration]  DEFAULT ((0)) FOR [Duration]
GO
ALTER TABLE [test].[bus_TestPlan_User] ADD  CONSTRAINT [DF_bus_TestPlan_User_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_TestPlan_User] ADD  CONSTRAINT [DF_bus_TestPlan_User_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] ADD  CONSTRAINT [DF_bus_TestPlan_User_Example_FinishQuestion]  DEFAULT ((0)) FOR [FinishQuestion]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] ADD  CONSTRAINT [DF_bus_TestPlan_User_Example_Duration]  DEFAULT ((0)) FOR [Duration]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] ADD  CONSTRAINT [DF_bus_TestPlan_User_Example_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] ADD  CONSTRAINT [DF_bus_TestPlan_User_Example_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_TestTemplete] ADD  CONSTRAINT [DF_bus_TestTemplete_Price_1]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [test].[bus_TestTemplete] ADD  CONSTRAINT [DF_bus_TestTemplete_CreateTime_1]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_TestTemplete] ADD  CONSTRAINT [DF_bus_TestTemplete_IsDel_1]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_TestTemplete_Example] ADD  CONSTRAINT [DF_bus_TestTemplete_Example_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_TestTemplete_Example] ADD  CONSTRAINT [DF_bus_TestTemplete_Example_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_User_Classify] ADD  CONSTRAINT [DF_bus_User_Classify_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_User_Classify] ADD  CONSTRAINT [DF_bus_User_Classify_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_User_Example] ADD  CONSTRAINT [DF_bus_User_Example_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_User_Example] ADD  CONSTRAINT [DF_bus_User_Example_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_User_TestTemplete] ADD  CONSTRAINT [DF_bus_User_TestTemplete_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_User_TestTemplete] ADD  CONSTRAINT [DF_bus_User_TestTemplete_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[sys_Attachment] ADD  CONSTRAINT [DF_sys_Attachment_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[sys_Attachment] ADD  CONSTRAINT [DF_sys_Attachment_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[sys_Func] ADD  CONSTRAINT [DF_sys_Func_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[sys_Func] ADD  CONSTRAINT [DF_sys_Func_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[sys_Role] ADD  CONSTRAINT [DF_sys_Role_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[sys_Role] ADD  CONSTRAINT [DF_sys_Role_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[sys_Role_Func] ADD  CONSTRAINT [DF_sys_Role_Func_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[sys_Role_Func] ADD  CONSTRAINT [DF_sys_Role_Func_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[sys_User] ADD  CONSTRAINT [DF_sys_User_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[sys_User] ADD  CONSTRAINT [DF_sys_User_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[sys_User_Role] ADD  CONSTRAINT [DF_sys_User_Role_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[sys_User_Role] ADD  CONSTRAINT [DF_sys_User_Role_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_Classify]  WITH CHECK ADD  CONSTRAINT [FK_bus_Classify_bus_Classify] FOREIGN KEY([ParentId])
REFERENCES [test].[bus_Classify] ([Id])
GO
ALTER TABLE [test].[bus_Classify] CHECK CONSTRAINT [FK_bus_Classify_bus_Classify]
GO
ALTER TABLE [test].[bus_Classify]  WITH CHECK ADD  CONSTRAINT [FK_bus_Classify_sys_User] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Classify] CHECK CONSTRAINT [FK_bus_Classify_sys_User]
GO
ALTER TABLE [test].[bus_Classify]  WITH CHECK ADD  CONSTRAINT [FK_bus_Classify_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Classify] CHECK CONSTRAINT [FK_bus_Classify_sys_User1]
GO
ALTER TABLE [test].[bus_Classify]  WITH CHECK ADD  CONSTRAINT [FK_bus_Classify_sys_User2] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Classify] CHECK CONSTRAINT [FK_bus_Classify_sys_User2]
GO
ALTER TABLE [test].[bus_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_bus_Classify] FOREIGN KEY([ClassifyId])
REFERENCES [test].[bus_Classify] ([Id])
GO
ALTER TABLE [test].[bus_Example] CHECK CONSTRAINT [FK_bus_Example_bus_Classify]
GO
ALTER TABLE [test].[bus_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_bus_Classify1] FOREIGN KEY([SubClassifyId])
REFERENCES [test].[bus_Classify] ([Id])
GO
ALTER TABLE [test].[bus_Example] CHECK CONSTRAINT [FK_bus_Example_bus_Classify1]
GO
ALTER TABLE [test].[bus_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_sys_Attachment] FOREIGN KEY([ImageId])
REFERENCES [test].[sys_Attachment] ([Id])
GO
ALTER TABLE [test].[bus_Example] CHECK CONSTRAINT [FK_bus_Example_sys_Attachment]
GO
ALTER TABLE [test].[bus_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example] CHECK CONSTRAINT [FK_bus_Example_sys_User]
GO
ALTER TABLE [test].[bus_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example] CHECK CONSTRAINT [FK_bus_Example_sys_User1]
GO
ALTER TABLE [test].[bus_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example] CHECK CONSTRAINT [FK_bus_Example_sys_User2]
GO
ALTER TABLE [test].[bus_Example_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Option_bus_Example] FOREIGN KEY([ExampleId])
REFERENCES [test].[bus_Example] ([Id])
GO
ALTER TABLE [test].[bus_Example_Option] CHECK CONSTRAINT [FK_bus_Example_Option_bus_Example]
GO
ALTER TABLE [test].[bus_Example_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Option_bus_Question_Option] FOREIGN KEY([OptionId])
REFERENCES [test].[bus_Question_Option] ([Id])
GO
ALTER TABLE [test].[bus_Example_Option] CHECK CONSTRAINT [FK_bus_Example_Option_bus_Question_Option]
GO
ALTER TABLE [test].[bus_Example_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Option_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example_Option] CHECK CONSTRAINT [FK_bus_Example_Option_sys_User]
GO
ALTER TABLE [test].[bus_Example_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Option_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example_Option] CHECK CONSTRAINT [FK_bus_Example_Option_sys_User1]
GO
ALTER TABLE [test].[bus_Example_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Option_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example_Option] CHECK CONSTRAINT [FK_bus_Example_Option_sys_User2]
GO
ALTER TABLE [test].[bus_Example_Question]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Question_bus_Example] FOREIGN KEY([ExampleId])
REFERENCES [test].[bus_Example] ([Id])
GO
ALTER TABLE [test].[bus_Example_Question] CHECK CONSTRAINT [FK_bus_Example_Question_bus_Example]
GO
ALTER TABLE [test].[bus_Example_Question]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Question_bus_Question] FOREIGN KEY([QuestionId])
REFERENCES [test].[bus_Question] ([Id])
GO
ALTER TABLE [test].[bus_Example_Question] CHECK CONSTRAINT [FK_bus_Example_Question_bus_Question]
GO
ALTER TABLE [test].[bus_Example_Question]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Question_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example_Question] CHECK CONSTRAINT [FK_bus_Example_Question_sys_User]
GO
ALTER TABLE [test].[bus_Example_Question]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Question_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example_Question] CHECK CONSTRAINT [FK_bus_Example_Question_sys_User1]
GO
ALTER TABLE [test].[bus_Example_Question]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Question_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example_Question] CHECK CONSTRAINT [FK_bus_Example_Question_sys_User2]
GO
ALTER TABLE [test].[bus_Example_Result]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Result_bus_Example] FOREIGN KEY([ExampleId])
REFERENCES [test].[bus_Example] ([Id])
GO
ALTER TABLE [test].[bus_Example_Result] CHECK CONSTRAINT [FK_bus_Example_Result_bus_Example]
GO
ALTER TABLE [test].[bus_Example_Result]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Result_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example_Result] CHECK CONSTRAINT [FK_bus_Example_Result_sys_User]
GO
ALTER TABLE [test].[bus_Example_Result]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Result_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example_Result] CHECK CONSTRAINT [FK_bus_Example_Result_sys_User1]
GO
ALTER TABLE [test].[bus_Example_Result]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Result_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Example_Result] CHECK CONSTRAINT [FK_bus_Example_Result_sys_User2]
GO
ALTER TABLE [test].[bus_Question]  WITH CHECK ADD  CONSTRAINT [FK_bus_Question_sys_Attachment] FOREIGN KEY([ImageId])
REFERENCES [test].[sys_Attachment] ([Id])
GO
ALTER TABLE [test].[bus_Question] CHECK CONSTRAINT [FK_bus_Question_sys_Attachment]
GO
ALTER TABLE [test].[bus_Question]  WITH CHECK ADD  CONSTRAINT [FK_bus_Question_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Question] CHECK CONSTRAINT [FK_bus_Question_sys_User]
GO
ALTER TABLE [test].[bus_Question]  WITH CHECK ADD  CONSTRAINT [FK_bus_Question_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Question] CHECK CONSTRAINT [FK_bus_Question_sys_User1]
GO
ALTER TABLE [test].[bus_Question]  WITH CHECK ADD  CONSTRAINT [FK_bus_Question_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Question] CHECK CONSTRAINT [FK_bus_Question_sys_User2]
GO
ALTER TABLE [test].[bus_Question_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_Question_Option_bus_Question] FOREIGN KEY([QuestionId])
REFERENCES [test].[bus_Question] ([Id])
GO
ALTER TABLE [test].[bus_Question_Option] CHECK CONSTRAINT [FK_bus_Question_Option_bus_Question]
GO
ALTER TABLE [test].[bus_Question_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_Question_Option_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Question_Option] CHECK CONSTRAINT [FK_bus_Question_Option_sys_User]
GO
ALTER TABLE [test].[bus_Question_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_Question_Option_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Question_Option] CHECK CONSTRAINT [FK_bus_Question_Option_sys_User1]
GO
ALTER TABLE [test].[bus_Question_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_Question_Option_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_Question_Option] CHECK CONSTRAINT [FK_bus_Question_Option_sys_User2]
GO
ALTER TABLE [test].[bus_TestPlan]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_bus_TestTemplete] FOREIGN KEY([TempleteId])
REFERENCES [test].[bus_TestTemplete] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan] CHECK CONSTRAINT [FK_bus_TestPlan_bus_TestTemplete]
GO
ALTER TABLE [test].[bus_TestPlan]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan] CHECK CONSTRAINT [FK_bus_TestPlan_sys_User]
GO
ALTER TABLE [test].[bus_TestPlan]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan] CHECK CONSTRAINT [FK_bus_TestPlan_sys_User1]
GO
ALTER TABLE [test].[bus_TestPlan]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan] CHECK CONSTRAINT [FK_bus_TestPlan_sys_User2]
GO
ALTER TABLE [test].[bus_TestPlan_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_Example_bus_Example] FOREIGN KEY([ExampleId])
REFERENCES [test].[bus_Example] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_Example] CHECK CONSTRAINT [FK_bus_TestPlan_Example_bus_Example]
GO
ALTER TABLE [test].[bus_TestPlan_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_Example_bus_TestPlan] FOREIGN KEY([PlanId])
REFERENCES [test].[bus_TestPlan] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_Example] CHECK CONSTRAINT [FK_bus_TestPlan_Example_bus_TestPlan]
GO
ALTER TABLE [test].[bus_TestPlan_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_Example_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_Example] CHECK CONSTRAINT [FK_bus_TestPlan_Example_sys_User]
GO
ALTER TABLE [test].[bus_TestPlan_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_Example_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_Example] CHECK CONSTRAINT [FK_bus_TestPlan_Example_sys_User1]
GO
ALTER TABLE [test].[bus_TestPlan_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_Example_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_Example] CHECK CONSTRAINT [FK_bus_TestPlan_Example_sys_User2]
GO
ALTER TABLE [test].[bus_TestPlan_User]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_bus_TestPlan] FOREIGN KEY([PlanId])
REFERENCES [test].[bus_TestPlan] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User] CHECK CONSTRAINT [FK_bus_TestPlan_User_bus_TestPlan]
GO
ALTER TABLE [test].[bus_TestPlan_User]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_sys_User] FOREIGN KEY([UserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User] CHECK CONSTRAINT [FK_bus_TestPlan_User_sys_User]
GO
ALTER TABLE [test].[bus_TestPlan_User]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_sys_User1] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User] CHECK CONSTRAINT [FK_bus_TestPlan_User_sys_User1]
GO
ALTER TABLE [test].[bus_TestPlan_User]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_sys_User2] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User] CHECK CONSTRAINT [FK_bus_TestPlan_User_sys_User2]
GO
ALTER TABLE [test].[bus_TestPlan_User]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_sys_User3] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User] CHECK CONSTRAINT [FK_bus_TestPlan_User_sys_User3]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Example_bus_Example] FOREIGN KEY([ExampleId])
REFERENCES [test].[bus_Example] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] CHECK CONSTRAINT [FK_bus_TestPlan_User_Example_bus_Example]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Example_bus_Example_Result] FOREIGN KEY([ResultId])
REFERENCES [test].[bus_Example_Result] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] CHECK CONSTRAINT [FK_bus_TestPlan_User_Example_bus_Example_Result]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Example_bus_TestPlan] FOREIGN KEY([PlanId])
REFERENCES [test].[bus_TestPlan] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] CHECK CONSTRAINT [FK_bus_TestPlan_User_Example_bus_TestPlan]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Example_sys_User] FOREIGN KEY([UserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] CHECK CONSTRAINT [FK_bus_TestPlan_User_Example_sys_User]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Example_sys_User1] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] CHECK CONSTRAINT [FK_bus_TestPlan_User_Example_sys_User1]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Example_sys_User2] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] CHECK CONSTRAINT [FK_bus_TestPlan_User_Example_sys_User2]
GO
ALTER TABLE [test].[bus_TestPlan_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Example_sys_User3] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Example] CHECK CONSTRAINT [FK_bus_TestPlan_User_Example_sys_User3]
GO
ALTER TABLE [test].[bus_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_bus_Classify] FOREIGN KEY([ClassifyId])
REFERENCES [test].[bus_Classify] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete] CHECK CONSTRAINT [FK_bus_TestTemplete_bus_Classify]
GO
ALTER TABLE [test].[bus_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_bus_Classify1] FOREIGN KEY([SubClassifyId])
REFERENCES [test].[bus_Classify] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete] CHECK CONSTRAINT [FK_bus_TestTemplete_bus_Classify1]
GO
ALTER TABLE [test].[bus_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_sys_Attachment] FOREIGN KEY([ImageId])
REFERENCES [test].[sys_Attachment] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete] CHECK CONSTRAINT [FK_bus_TestTemplete_sys_Attachment]
GO
ALTER TABLE [test].[bus_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete] CHECK CONSTRAINT [FK_bus_TestTemplete_sys_User]
GO
ALTER TABLE [test].[bus_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete] CHECK CONSTRAINT [FK_bus_TestTemplete_sys_User1]
GO
ALTER TABLE [test].[bus_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete] CHECK CONSTRAINT [FK_bus_TestTemplete_sys_User2]
GO
ALTER TABLE [test].[bus_TestTemplete_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_bus_Example] FOREIGN KEY([ExampleId])
REFERENCES [test].[bus_Example] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_bus_Example]
GO
ALTER TABLE [test].[bus_TestTemplete_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_bus_TestTemplete] FOREIGN KEY([TempleteId])
REFERENCES [test].[bus_TestTemplete] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_bus_TestTemplete]
GO
ALTER TABLE [test].[bus_TestTemplete_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_sys_User]
GO
ALTER TABLE [test].[bus_TestTemplete_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_sys_User1]
GO
ALTER TABLE [test].[bus_TestTemplete_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplete_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_sys_User2]
GO
ALTER TABLE [test].[bus_User_Classify]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Classify_bus_Classify] FOREIGN KEY([ClassifyId])
REFERENCES [test].[bus_Classify] ([Id])
GO
ALTER TABLE [test].[bus_User_Classify] CHECK CONSTRAINT [FK_bus_User_Classify_bus_Classify]
GO
ALTER TABLE [test].[bus_User_Classify]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Classify_sys_User] FOREIGN KEY([UserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_Classify] CHECK CONSTRAINT [FK_bus_User_Classify_sys_User]
GO
ALTER TABLE [test].[bus_User_Classify]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Classify_sys_User1] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_Classify] CHECK CONSTRAINT [FK_bus_User_Classify_sys_User1]
GO
ALTER TABLE [test].[bus_User_Classify]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Classify_sys_User2] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_Classify] CHECK CONSTRAINT [FK_bus_User_Classify_sys_User2]
GO
ALTER TABLE [test].[bus_User_Classify]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Classify_sys_User3] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_Classify] CHECK CONSTRAINT [FK_bus_User_Classify_sys_User3]
GO
ALTER TABLE [test].[bus_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Example_bus_Example] FOREIGN KEY([ExampleId])
REFERENCES [test].[bus_Example] ([Id])
GO
ALTER TABLE [test].[bus_User_Example] CHECK CONSTRAINT [FK_bus_User_Example_bus_Example]
GO
ALTER TABLE [test].[bus_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Example_sys_User] FOREIGN KEY([UserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_Example] CHECK CONSTRAINT [FK_bus_User_Example_sys_User]
GO
ALTER TABLE [test].[bus_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Example_sys_User1] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_Example] CHECK CONSTRAINT [FK_bus_User_Example_sys_User1]
GO
ALTER TABLE [test].[bus_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Example_sys_User2] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_Example] CHECK CONSTRAINT [FK_bus_User_Example_sys_User2]
GO
ALTER TABLE [test].[bus_User_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_Example_sys_User3] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_Example] CHECK CONSTRAINT [FK_bus_User_Example_sys_User3]
GO
ALTER TABLE [test].[bus_User_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_bus_TestTemplete] FOREIGN KEY([TempleteId])
REFERENCES [test].[bus_TestTemplete] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplete] CHECK CONSTRAINT [FK_bus_User_TestTemplete_bus_TestTemplete]
GO
ALTER TABLE [test].[bus_User_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_sys_User] FOREIGN KEY([UserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplete] CHECK CONSTRAINT [FK_bus_User_TestTemplete_sys_User]
GO
ALTER TABLE [test].[bus_User_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_sys_User1] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplete] CHECK CONSTRAINT [FK_bus_User_TestTemplete_sys_User1]
GO
ALTER TABLE [test].[bus_User_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_sys_User2] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplete] CHECK CONSTRAINT [FK_bus_User_TestTemplete_sys_User2]
GO
ALTER TABLE [test].[bus_User_TestTemplete]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_sys_User3] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplete] CHECK CONSTRAINT [FK_bus_User_TestTemplete_sys_User3]
GO
ALTER TABLE [test].[sys_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_sys_Attachment_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Attachment] CHECK CONSTRAINT [FK_sys_Attachment_sys_User]
GO
ALTER TABLE [test].[sys_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_sys_Attachment_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Attachment] CHECK CONSTRAINT [FK_sys_Attachment_sys_User1]
GO
ALTER TABLE [test].[sys_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_sys_Attachment_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Attachment] CHECK CONSTRAINT [FK_sys_Attachment_sys_User2]
GO
ALTER TABLE [test].[sys_Func]  WITH CHECK ADD  CONSTRAINT [FK_sys_Func_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Func] CHECK CONSTRAINT [FK_sys_Func_sys_User]
GO
ALTER TABLE [test].[sys_Func]  WITH CHECK ADD  CONSTRAINT [FK_sys_Func_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Func] CHECK CONSTRAINT [FK_sys_Func_sys_User1]
GO
ALTER TABLE [test].[sys_Func]  WITH CHECK ADD  CONSTRAINT [FK_sys_Func_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Func] CHECK CONSTRAINT [FK_sys_Func_sys_User2]
GO
ALTER TABLE [test].[sys_Role]  WITH NOCHECK ADD  CONSTRAINT [FK_sys_Role_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Role] CHECK CONSTRAINT [FK_sys_Role_sys_User]
GO
ALTER TABLE [test].[sys_Role]  WITH NOCHECK ADD  CONSTRAINT [FK_sys_Role_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Role] CHECK CONSTRAINT [FK_sys_Role_sys_User1]
GO
ALTER TABLE [test].[sys_Role]  WITH NOCHECK ADD  CONSTRAINT [FK_sys_Role_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Role] CHECK CONSTRAINT [FK_sys_Role_sys_User2]
GO
ALTER TABLE [test].[sys_Role_Func]  WITH CHECK ADD  CONSTRAINT [FK_sys_Role_Func_sys_Func] FOREIGN KEY([FuncId])
REFERENCES [test].[sys_Func] ([Id])
GO
ALTER TABLE [test].[sys_Role_Func] CHECK CONSTRAINT [FK_sys_Role_Func_sys_Func]
GO
ALTER TABLE [test].[sys_Role_Func]  WITH CHECK ADD  CONSTRAINT [FK_sys_Role_Func_sys_Role] FOREIGN KEY([RoleId])
REFERENCES [test].[sys_Role] ([Id])
GO
ALTER TABLE [test].[sys_Role_Func] CHECK CONSTRAINT [FK_sys_Role_Func_sys_Role]
GO
ALTER TABLE [test].[sys_Role_Func]  WITH CHECK ADD  CONSTRAINT [FK_sys_Role_Func_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Role_Func] CHECK CONSTRAINT [FK_sys_Role_Func_sys_User]
GO
ALTER TABLE [test].[sys_Role_Func]  WITH CHECK ADD  CONSTRAINT [FK_sys_Role_Func_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Role_Func] CHECK CONSTRAINT [FK_sys_Role_Func_sys_User1]
GO
ALTER TABLE [test].[sys_Role_Func]  WITH CHECK ADD  CONSTRAINT [FK_sys_Role_Func_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_Role_Func] CHECK CONSTRAINT [FK_sys_Role_Func_sys_User2]
GO
ALTER TABLE [test].[sys_User]  WITH CHECK ADD  CONSTRAINT [FK_sys_User_sys_Attachment] FOREIGN KEY([AvatarId])
REFERENCES [test].[sys_Attachment] ([Id])
GO
ALTER TABLE [test].[sys_User] CHECK CONSTRAINT [FK_sys_User_sys_Attachment]
GO
ALTER TABLE [test].[sys_User_Role]  WITH CHECK ADD  CONSTRAINT [FK_sys_User_Role_sys_Role] FOREIGN KEY([RoleId])
REFERENCES [test].[sys_Role] ([Id])
GO
ALTER TABLE [test].[sys_User_Role] CHECK CONSTRAINT [FK_sys_User_Role_sys_Role]
GO
ALTER TABLE [test].[sys_User_Role]  WITH CHECK ADD  CONSTRAINT [FK_sys_User_Role_sys_User] FOREIGN KEY([UserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_User_Role] CHECK CONSTRAINT [FK_sys_User_Role_sys_User]
GO
ALTER TABLE [test].[sys_User_Role]  WITH CHECK ADD  CONSTRAINT [FK_sys_User_Role_sys_User1] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_User_Role] CHECK CONSTRAINT [FK_sys_User_Role_sys_User1]
GO
ALTER TABLE [test].[sys_User_Role]  WITH CHECK ADD  CONSTRAINT [FK_sys_User_Role_sys_User2] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_User_Role] CHECK CONSTRAINT [FK_sys_User_Role_sys_User2]
GO
ALTER TABLE [test].[sys_User_Role]  WITH CHECK ADD  CONSTRAINT [FK_sys_User_Role_sys_User3] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[sys_User_Role] CHECK CONSTRAINT [FK_sys_User_Role_sys_User3]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级分类' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Classify', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'ClassifyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子类' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'SubClassifyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关键字 | 分割' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'Keys'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'题数' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'QuestionNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长（分钟）' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片附件Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'ImageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠价格' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'DiscountPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明  给被测者' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'Note'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注  给组织测试者' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'NContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 创建中 1 创建完成 2已上架' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'测试' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'ExampleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'选项' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'OptionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分值' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'Score'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跳转' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'Jump'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Option', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'测试' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'ExampleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'QuestionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二级序号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'SortNo2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Question', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用例Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'ExampleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代码' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'NContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最小分值' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'MinScore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大分值' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'MaxScore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题题面' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片 附件id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'ImageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1单选题 2多选题 3判断题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'选项数' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'OptionNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'QuestionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮文字' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简介' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划名称' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 创建中 1 已创建 2 已发布' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'TempleteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'可以暂停' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'CanPause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用例数量' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'ExampleNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'题数' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'QuestionNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员数' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'UserNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'PlanId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用例id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'ExampleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长（分钟） 0不限' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'可以暂停' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'CanPause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_Example', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'PlanId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进度 0 创建 1 进入系统 2 开始答题 3 离开 4 答题完成' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前实例' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'CurrentExample'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成的实例' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'FinishExample'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前问题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'CurrentQuestion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成的问题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'FinishQuestion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已用时长' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'BeginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结果代码 | 分割' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'ResultCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'PlanId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实例id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'ExampleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进度 0 未答 1 正在答题 2 答完' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前问题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'CurrentQuestion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成的问题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'FinishQuestion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已用时长' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结果代码' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'ResultCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结果id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'ResultId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'BeginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'ClassifyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子类' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'SubClassifyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关键字 | 分割' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'Keys'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实例数' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'ExampleNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'题数' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'QuestionNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长（分钟）' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片附件Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'ImageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠价格' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'DiscountPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明  给被测者' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'Note'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注  给组织测试者' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'NContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 创建中 1 创建完成 2已上架' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'TempleteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用例id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'ExampleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长（分钟） 0不限' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'可以暂停' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'CanPause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplete_Example', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'ClassifyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'ExpireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Classify', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用例' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'ExampleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'ExpireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_Example', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用例' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'TempleteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'ExpireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplete', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路径' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'Path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件名' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展名' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'Ext'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 未知 100 问题图片 200' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'Module'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0创建 1应用 每天状态为0的会被自动清掉以释放空间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Attachment', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能名称' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能代码' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'Route'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 大功能 1按钮或小功能' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Func', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role_Func', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role_Func', @level2type=N'COLUMN',@level2name=N'FuncId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role_Func', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role_Func', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role_Func', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role_Func', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role_Func', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role_Func', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_Role_Func', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像名称' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'AvatarName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像附件' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'AvatarId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 被测者 2 组织测试者 4 代理商 -2147483648总管理员' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1启用 0 禁用' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'LastTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录ip' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'LastIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'失效时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'ExpireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User_Role', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User_Role', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User_Role', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User_Role', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User_Role', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User_Role', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User_Role', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User_Role', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'sys_User_Role', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
