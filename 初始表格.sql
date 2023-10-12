
/****** Object:  Table [test].[bus_Classify]    Script Date: 2023/10/12 17:33:29 ******/
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
/****** Object:  Table [test].[bus_Example]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[bus_Example_Option]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[bus_Example_Question]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[bus_Example_Result]    Script Date: 2023/10/12 17:33:30 ******/
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
	[ImageId] [int] NULL,
	[MinScore] [int] NOT NULL,
	[MaxScore] [int] NOT NULL,
	[SortNo] [int] NOT NULL,
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
/****** Object:  Table [test].[bus_Question]    Script Date: 2023/10/12 17:33:30 ******/
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
	[NContent] [nvarchar](4000) NULL,
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
/****** Object:  Table [test].[bus_Question_Option]    Script Date: 2023/10/12 17:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_Question_Option](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[SortNo] [int] NOT NULL,
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
/****** Object:  Table [test].[bus_TestPlan]    Script Date: 2023/10/12 17:33:30 ******/
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
	[TemplateId] [int] NOT NULL,
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
/****** Object:  Table [test].[bus_TestPlan_Example]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[bus_TestPlan_User]    Script Date: 2023/10/12 17:33:30 ******/
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
	[Score] [nvarchar](100) NULL,
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
/****** Object:  Table [test].[bus_TestPlan_User_Example]    Script Date: 2023/10/12 17:33:30 ******/
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
	[Score] [int] NULL,
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
/****** Object:  Table [test].[bus_TestPlan_User_Option]    Script Date: 2023/10/12 17:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_TestPlan_User_Option](
	[PlanId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ExampleId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[OptionId] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_TestPlan_User_Option] PRIMARY KEY CLUSTERED 
(
	[PlanId] ASC,
	[UserId] ASC,
	[ExampleId] ASC,
	[QuestionId] ASC,
	[OptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_TestTemplate]    Script Date: 2023/10/12 17:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_TestTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[ClassifyId] [int] NULL,
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
	[CanPause] [bit] NOT NULL,
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
/****** Object:  Table [test].[bus_TestTemplate_Example]    Script Date: 2023/10/12 17:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_TestTemplate_Example](
	[TemplateId] [int] NOT NULL,
	[ExampleId] [int] NOT NULL,
	[SortNo] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[CanPause] [bit] NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_TestTemplete_Example] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC,
	[ExampleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_User_Classify]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[bus_User_Example]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[bus_User_TestTemplate]    Script Date: 2023/10/12 17:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_User_TestTemplate](
	[UserId] [int] NOT NULL,
	[TemplateId] [int] NOT NULL,
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
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[bus_UserInfo]    Script Date: 2023/10/12 17:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [test].[bus_UserInfo](
	[UserId] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[RealName] [nvarchar](50) NULL,
	[Sex] [int] NULL,
	[Birthday] [date] NULL,
	[IdCardNo] [varchar](50) NULL,
	[UnitName] [nvarchar](50) NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUserId] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[IsDel] [bit] NOT NULL,
	[DelUserId] [int] NULL,
	[DelTime] [datetime] NULL,
 CONSTRAINT [PK_bus_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[OwnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [test].[sys_Attachment]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[sys_Func]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[sys_Role]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[sys_Role_Func]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[sys_User]    Script Date: 2023/10/12 17:33:30 ******/
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
/****** Object:  Table [test].[sys_User_Role]    Script Date: 2023/10/12 17:33:30 ******/
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

INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1000, N'会员管理', N'membermanager', N'', 0, NULL, NULL, 1, CAST(N'2023-10-11T09:40:51.400' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1003, N'删除用户', N'deluser', N'', 0, 1000, NULL, 1, CAST(N'2023-10-12T17:12:03.973' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1100, N'代理商管理', N'bussness', N'', 0, 1000, NULL, 1, CAST(N'2023-10-11T09:42:13.020' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1200, N'教师管理', N'teachers', N'', 0, 1000, NULL, 1, CAST(N'2023-10-11T09:42:13.020' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1300, N'会员管理', N'members', N'', 0, 1000, NULL, 1, CAST(N'2023-10-11T09:42:13.020' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1400, N'我的会员', N'mymembers', N'', 0, 1000, NULL, 1, CAST(N'2023-10-11T09:42:13.020' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1500, N'访客管理', N'visitors', N'', 0, 1000, NULL, 1, CAST(N'2023-10-11T09:42:13.020' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2000, N'量表管理', N'examplemanager', N'', 0, NULL, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2100, N'量表分类', N'classifies', N'', 0, 2000, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2200, N'量表管理', N'examples', N'', 0, 2000, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (3000, N'任务类型', N'templatemanager', N'', 0, NULL, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (3100, N'任务类型', N'templates', N'', 0, 3000, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (4000, N'任务管理', N'planmanager', N'', 0, NULL, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (4100, N'任务管理', N'plans', N'', 0, 4000, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (4200, N'测评报告', N'reports', N'', 0, 4000, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (4300, N'我的任务', N'ownplans', N'', 0, 4000, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (4400, N'我的报告', N'ownreports', N'', 0, 4000, NULL, 1, CAST(N'2023-10-11T09:47:24.027' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (5000, N'财务管理', N'financemanager', N'', 0, NULL, NULL, 1, CAST(N'2023-10-11T09:53:27.927' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (5100, N'财务管理', N'finance', N'', 0, 5000, NULL, 1, CAST(N'2023-10-11T09:53:27.927' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (6000, N'系统设置', N'systemsettingmanager', N'', 0, NULL, NULL, 1, CAST(N'2023-10-11T09:53:27.927' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Func] ([Id], [Name], [Code], [Route], [Type], [ParentId], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (6100, N'系统设置', N'systemsettings', N'', 0, 6000, NULL, 1, CAST(N'2023-10-11T09:53:27.927' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [test].[sys_Role] ON 
GO
INSERT [test].[sys_Role] ([Id], [Name], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, N'管理员', N'管理所有问题', 1, CAST(N'2023-09-07T09:53:58.133' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role] ([Id], [Name], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2, N'代理商', N'代理销售', 1, CAST(N'2023-09-07T09:54:16.770' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role] ([Id], [Name], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (3, N'施测人', N'测试别人的人', 1, CAST(N'2023-09-07T09:55:04.403' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role] ([Id], [Name], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (4, N'被测者', N'小白鼠', 1, CAST(N'2023-09-07T09:55:22.813' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role] ([Id], [Name], [Remark], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (5, N'访客', N'来访者', 1, CAST(N'2023-09-15T10:06:42.307' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [test].[sys_Role] OFF
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 1000, 1, CAST(N'2023-10-11T09:55:45.553' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 1003, 1, CAST(N'2023-10-12T17:12:22.447' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 1100, 1, CAST(N'2023-10-11T09:55:54.183' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 1200, 1, CAST(N'2023-10-11T09:56:00.280' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 1300, 1, CAST(N'2023-10-11T09:56:09.833' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 1400, 1, CAST(N'2023-10-11T09:56:15.967' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 1500, 1, CAST(N'2023-10-11T09:56:22.047' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 2100, 1, CAST(N'2023-10-11T09:56:33.860' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 2200, 1, CAST(N'2023-10-11T09:56:40.100' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 3100, 1, CAST(N'2023-10-11T09:56:54.813' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 4100, 1, CAST(N'2023-10-11T09:56:59.447' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 4200, 1, CAST(N'2023-10-11T09:57:07.013' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 5100, 1, CAST(N'2023-10-11T10:43:41.603' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2, 1000, 1, CAST(N'2023-10-11T09:59:35.053' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2, 1200, 1, CAST(N'2023-10-11T09:59:42.987' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2, 1300, 1, CAST(N'2023-10-11T10:00:13.323' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2, 1400, 1, CAST(N'2023-10-11T10:00:21.677' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2, 1500, 1, CAST(N'2023-10-11T10:00:30.090' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2, 4100, 1, CAST(N'2023-10-11T10:00:50.210' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2, 4200, 1, CAST(N'2023-10-11T10:05:33.677' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (2, 5100, 1, CAST(N'2023-10-11T10:43:46.567' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (3, 1000, 1, CAST(N'2023-10-11T10:04:26.490' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (3, 1400, 1, CAST(N'2023-10-11T10:04:38.050' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (3, 4300, 1, CAST(N'2023-10-11T10:05:08.863' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (3, 4400, 1, CAST(N'2023-10-11T10:05:40.120' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [test].[sys_Role_Func] ([RoleId], [FuncId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (3, 5100, 1, CAST(N'2023-10-11T10:43:52.503' AS DateTime), NULL, NULL, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [test].[sys_User] ON 
GO
INSERT [test].[sys_User] ([Id], [Name], [Account], [Mobile], [Password], [AvatarName], [AvatarId], [Type], [Status], [LastTime], [LastIP], [ExpireTime], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, N'管理员', N'admin', N'1101', N'QAZiCt4vIGULS5ws+AJuXg==', N'/static/avatars/029f.jpg', NULL, -2147483648, 1, NULL, NULL, NULL, 1, CAST(N'2023-09-01T08:38:59.700' AS DateTime), 1, CAST(N'2023-10-12T14:58:15.037' AS DateTime), 0, NULL, NULL)
GO
SET IDENTITY_INSERT [test].[sys_User] OFF
GO
INSERT [test].[bus_UserInfo] ([UserId], [OwnerId], [RealName], [Sex], [Birthday], [IdCardNo], [UnitName], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 1, N'管理员X', 2, CAST(N'2000-01-01' AS Date), N'130103200001011230', N'中国爱拜尔科技有限公司', 1, CAST(N'2023-09-11T15:13:15.457' AS DateTime), 1, CAST(N'2023-09-28T15:36:01.630' AS DateTime), 0, NULL, NULL)
GO
INSERT [test].[sys_User_Role] ([UserId], [RoleId], [CreateUserId], [CreateTime], [ModifyUserId], [ModifyTime], [IsDel], [DelUserId], [DelTime]) VALUES (1, 1, 1, CAST(N'2023-09-07T10:08:30.710' AS DateTime), NULL, NULL, 0, NULL, NULL)
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
ALTER TABLE [test].[bus_TestPlan_User_Option] ADD  CONSTRAINT [DF_bus_TestPlan_User_Option_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_TestPlan_User_Option] ADD  CONSTRAINT [DF_bus_TestPlan_User_Option_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_TestTemplate] ADD  CONSTRAINT [DF_bus_TestTemplete_Price_1]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [test].[bus_TestTemplate] ADD  CONSTRAINT [DF_bus_TestTemplete_CreateTime_1]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_TestTemplate] ADD  CONSTRAINT [DF_bus_TestTemplete_IsDel_1]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_TestTemplate_Example] ADD  CONSTRAINT [DF_bus_TestTemplete_Example_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_TestTemplate_Example] ADD  CONSTRAINT [DF_bus_TestTemplete_Example_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_User_Classify] ADD  CONSTRAINT [DF_bus_User_Classify_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_User_Classify] ADD  CONSTRAINT [DF_bus_User_Classify_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_User_Example] ADD  CONSTRAINT [DF_bus_User_Example_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_User_Example] ADD  CONSTRAINT [DF_bus_User_Example_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_User_TestTemplate] ADD  CONSTRAINT [DF_bus_User_TestTemplete_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_User_TestTemplate] ADD  CONSTRAINT [DF_bus_User_TestTemplete_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
ALTER TABLE [test].[bus_UserInfo] ADD  CONSTRAINT [DF_bus_UserInfo_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [test].[bus_UserInfo] ADD  CONSTRAINT [DF_bus_UserInfo_IsDel]  DEFAULT ((0)) FOR [IsDel]
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
ALTER TABLE [test].[bus_Example_Result]  WITH CHECK ADD  CONSTRAINT [FK_bus_Example_Result_sys_Attachment] FOREIGN KEY([ImageId])
REFERENCES [test].[sys_Attachment] ([Id])
GO
ALTER TABLE [test].[bus_Example_Result] CHECK CONSTRAINT [FK_bus_Example_Result_sys_Attachment]
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
ALTER TABLE [test].[bus_TestPlan]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_bus_TestTemplete] FOREIGN KEY([TemplateId])
REFERENCES [test].[bus_TestTemplate] ([Id])
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
ALTER TABLE [test].[bus_TestPlan_User_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Option_bus_Example] FOREIGN KEY([ExampleId])
REFERENCES [test].[bus_Example] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Option] CHECK CONSTRAINT [FK_bus_TestPlan_User_Option_bus_Example]
GO
ALTER TABLE [test].[bus_TestPlan_User_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Option_bus_Question] FOREIGN KEY([QuestionId])
REFERENCES [test].[bus_Question] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Option] CHECK CONSTRAINT [FK_bus_TestPlan_User_Option_bus_Question]
GO
ALTER TABLE [test].[bus_TestPlan_User_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Option_bus_Question_Option] FOREIGN KEY([OptionId])
REFERENCES [test].[bus_Question_Option] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Option] CHECK CONSTRAINT [FK_bus_TestPlan_User_Option_bus_Question_Option]
GO
ALTER TABLE [test].[bus_TestPlan_User_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Option_bus_TestPlan] FOREIGN KEY([PlanId])
REFERENCES [test].[bus_TestPlan] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Option] CHECK CONSTRAINT [FK_bus_TestPlan_User_Option_bus_TestPlan]
GO
ALTER TABLE [test].[bus_TestPlan_User_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Option_sys_User] FOREIGN KEY([UserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Option] CHECK CONSTRAINT [FK_bus_TestPlan_User_Option_sys_User]
GO
ALTER TABLE [test].[bus_TestPlan_User_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Option_sys_User1] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Option] CHECK CONSTRAINT [FK_bus_TestPlan_User_Option_sys_User1]
GO
ALTER TABLE [test].[bus_TestPlan_User_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Option_sys_User2] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Option] CHECK CONSTRAINT [FK_bus_TestPlan_User_Option_sys_User2]
GO
ALTER TABLE [test].[bus_TestPlan_User_Option]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestPlan_User_Option_sys_User3] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestPlan_User_Option] CHECK CONSTRAINT [FK_bus_TestPlan_User_Option_sys_User3]
GO
ALTER TABLE [test].[bus_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_bus_Classify] FOREIGN KEY([ClassifyId])
REFERENCES [test].[bus_Classify] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate] CHECK CONSTRAINT [FK_bus_TestTemplete_bus_Classify]
GO
ALTER TABLE [test].[bus_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_bus_Classify1] FOREIGN KEY([SubClassifyId])
REFERENCES [test].[bus_Classify] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate] CHECK CONSTRAINT [FK_bus_TestTemplete_bus_Classify1]
GO
ALTER TABLE [test].[bus_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_sys_Attachment] FOREIGN KEY([ImageId])
REFERENCES [test].[sys_Attachment] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate] CHECK CONSTRAINT [FK_bus_TestTemplete_sys_Attachment]
GO
ALTER TABLE [test].[bus_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate] CHECK CONSTRAINT [FK_bus_TestTemplete_sys_User]
GO
ALTER TABLE [test].[bus_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate] CHECK CONSTRAINT [FK_bus_TestTemplete_sys_User1]
GO
ALTER TABLE [test].[bus_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate] CHECK CONSTRAINT [FK_bus_TestTemplete_sys_User2]
GO
ALTER TABLE [test].[bus_TestTemplate_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_bus_Example] FOREIGN KEY([ExampleId])
REFERENCES [test].[bus_Example] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_bus_Example]
GO
ALTER TABLE [test].[bus_TestTemplate_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_bus_TestTemplete] FOREIGN KEY([TemplateId])
REFERENCES [test].[bus_TestTemplate] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_bus_TestTemplete]
GO
ALTER TABLE [test].[bus_TestTemplate_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_sys_User] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_sys_User]
GO
ALTER TABLE [test].[bus_TestTemplate_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_sys_User1] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_sys_User1]
GO
ALTER TABLE [test].[bus_TestTemplate_Example]  WITH CHECK ADD  CONSTRAINT [FK_bus_TestTemplete_Example_sys_User2] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_TestTemplate_Example] CHECK CONSTRAINT [FK_bus_TestTemplete_Example_sys_User2]
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
ALTER TABLE [test].[bus_User_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_bus_TestTemplete] FOREIGN KEY([TemplateId])
REFERENCES [test].[bus_TestTemplate] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplate] CHECK CONSTRAINT [FK_bus_User_TestTemplete_bus_TestTemplete]
GO
ALTER TABLE [test].[bus_User_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_sys_User] FOREIGN KEY([UserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplate] CHECK CONSTRAINT [FK_bus_User_TestTemplete_sys_User]
GO
ALTER TABLE [test].[bus_User_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_sys_User1] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplate] CHECK CONSTRAINT [FK_bus_User_TestTemplete_sys_User1]
GO
ALTER TABLE [test].[bus_User_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_sys_User2] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplate] CHECK CONSTRAINT [FK_bus_User_TestTemplete_sys_User2]
GO
ALTER TABLE [test].[bus_User_TestTemplate]  WITH CHECK ADD  CONSTRAINT [FK_bus_User_TestTemplete_sys_User3] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_User_TestTemplate] CHECK CONSTRAINT [FK_bus_User_TestTemplete_sys_User3]
GO
ALTER TABLE [test].[bus_UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_bus_UserInfo_sys_User] FOREIGN KEY([UserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_UserInfo] CHECK CONSTRAINT [FK_bus_UserInfo_sys_User]
GO
ALTER TABLE [test].[bus_UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_bus_UserInfo_sys_User1] FOREIGN KEY([OwnerId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_UserInfo] CHECK CONSTRAINT [FK_bus_UserInfo_sys_User1]
GO
ALTER TABLE [test].[bus_UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_bus_UserInfo_sys_User2] FOREIGN KEY([CreateUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_UserInfo] CHECK CONSTRAINT [FK_bus_UserInfo_sys_User2]
GO
ALTER TABLE [test].[bus_UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_bus_UserInfo_sys_User3] FOREIGN KEY([ModifyUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_UserInfo] CHECK CONSTRAINT [FK_bus_UserInfo_sys_User3]
GO
ALTER TABLE [test].[bus_UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_bus_UserInfo_sys_User4] FOREIGN KEY([DelUserId])
REFERENCES [test].[sys_User] ([Id])
GO
ALTER TABLE [test].[bus_UserInfo] CHECK CONSTRAINT [FK_bus_UserInfo_sys_User4]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'ImageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最小分值' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'MinScore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大分值' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'MaxScore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Example_Result', @level2type=N'COLUMN',@level2name=N'SortNo'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总题面' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question', @level2type=N'COLUMN',@level2name=N'NContent'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_Question_Option', @level2type=N'COLUMN',@level2name=N'SortNo'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan', @level2type=N'COLUMN',@level2name=N'TemplateId'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'得分 | 分割' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User', @level2type=N'COLUMN',@level2name=N'Score'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'得分' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Example', @level2type=N'COLUMN',@level2name=N'Score'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'PlanId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实例id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'ExampleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'QuestionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'选项id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'OptionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestPlan_User_Option', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'ClassifyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子类' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'SubClassifyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关键字 | 分割' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'Keys'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实例数' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'ExampleNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'题数' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'QuestionNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长（分钟）' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片附件Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'ImageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠价格' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'DiscountPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明  给被测者' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'Note'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注  给组织测试者' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'NContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 创建中 1 创建完成 2已上架' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'可以暂停' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'CanPause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'TemplateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用例id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'ExampleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长（分钟） 0不限' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'可以暂停' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'CanPause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_TestTemplate_Example', @level2type=N'COLUMN',@level2name=N'DelTime'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用例' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'TemplateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'ExpireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_User_TestTemplate', @level2type=N'COLUMN',@level2name=N'DelTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所有者Id' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'OwnerId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'RealName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别 1女 2男' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'Sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生日' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'Birthday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证号' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'IdCardNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'UnitName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除人' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'DelUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'test', @level1type=N'TABLE',@level1name=N'bus_UserInfo', @level2type=N'COLUMN',@level2name=N'DelTime'
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
