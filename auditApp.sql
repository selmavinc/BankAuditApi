USE [AuditApp]
GO
/****** Object:  Table [dbo].[Audits]    Script Date: 26-10-2022 15:13:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audits](
	[AuditID] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [varchar](4) NULL,
	[LeadExaminerID] [int] NULL,
	[AssociateExaminerID] [int] NULL,
	[AuditDate] [datetime] NULL,
	[AuditHours] [decimal](18, 0) NULL,
	[AuditStatusID] [int] NULL,
 CONSTRAINT [PK_Audits] PRIMARY KEY CLUSTERED 
(
	[AuditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditStatuses]    Script Date: 26-10-2022 15:13:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditStatuses](
	[AuditStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_AuditStatuses] PRIMARY KEY CLUSTERED 
(
	[AuditStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BranchDetails]    Script Date: 26-10-2022 15:13:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchDetails](
	[BranchID] [varchar](4) NOT NULL,
	[Address] [varchar](500) NULL,
	[Location] [varchar](50) NULL,
	[BranchManagerName] [varchar](100) NULL,
 CONSTRAINT [PK_BranchDetails] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Examiners]    Script Date: 26-10-2022 15:13:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Examiners](
	[ExaminerID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[HoursAssigned] [decimal](18, 0) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Examiners] PRIMARY KEY CLUSTERED 
(
	[ExaminerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Audits] ON 

INSERT [dbo].[Audits] ([AuditID], [BranchID], [LeadExaminerID], [AssociateExaminerID], [AuditDate], [AuditHours], [AuditStatusID]) VALUES (1, N'A111', 7, NULL, CAST(N'2022-04-12T00:00:00.000' AS DateTime), CAST(4 AS Decimal(18, 0)), 2)
INSERT [dbo].[Audits] ([AuditID], [BranchID], [LeadExaminerID], [AssociateExaminerID], [AuditDate], [AuditHours], [AuditStatusID]) VALUES (2, N'A112', 6, NULL, CAST(N'2022-03-05T00:00:00.000' AS DateTime), CAST(4 AS Decimal(18, 0)), 2)
INSERT [dbo].[Audits] ([AuditID], [BranchID], [LeadExaminerID], [AssociateExaminerID], [AuditDate], [AuditHours], [AuditStatusID]) VALUES (3, N'A113', 6, 1, CAST(N'2022-03-10T00:00:00.000' AS DateTime), CAST(8 AS Decimal(18, 0)), 2)
INSERT [dbo].[Audits] ([AuditID], [BranchID], [LeadExaminerID], [AssociateExaminerID], [AuditDate], [AuditHours], [AuditStatusID]) VALUES (4, N'A114', 8, 3, CAST(N'2022-11-17T00:00:00.000' AS DateTime), CAST(8 AS Decimal(18, 0)), 2)
INSERT [dbo].[Audits] ([AuditID], [BranchID], [LeadExaminerID], [AssociateExaminerID], [AuditDate], [AuditHours], [AuditStatusID]) VALUES (5, N'A115', NULL, NULL, NULL, CAST(4 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[Audits] OFF
GO
SET IDENTITY_INSERT [dbo].[AuditStatuses] ON 

INSERT [dbo].[AuditStatuses] ([AuditStatusID], [Name]) VALUES (1, N'Unscheduled')
INSERT [dbo].[AuditStatuses] ([AuditStatusID], [Name]) VALUES (2, N'Scheduled')
SET IDENTITY_INSERT [dbo].[AuditStatuses] OFF
GO
INSERT [dbo].[BranchDetails] ([BranchID], [Address], [Location], [BranchManagerName]) VALUES (N'A111', N'APO complex', N'Chennai', N'Aravind')
INSERT [dbo].[BranchDetails] ([BranchID], [Address], [Location], [BranchManagerName]) VALUES (N'A112', N'Taj complex', N'Chennai', N'Jovin')
INSERT [dbo].[BranchDetails] ([BranchID], [Address], [Location], [BranchManagerName]) VALUES (N'A113', N'Mahesh complex', N'Karnataka', N'Christy')
INSERT [dbo].[BranchDetails] ([BranchID], [Address], [Location], [BranchManagerName]) VALUES (N'A114', N'XXX complex', N'Delhi', N'Mariya')
INSERT [dbo].[BranchDetails] ([BranchID], [Address], [Location], [BranchManagerName]) VALUES (N'A115', N'YYY complex', N'Kochi', N'Hennah')
GO
SET IDENTITY_INSERT [dbo].[Examiners] ON 

INSERT [dbo].[Examiners] ([ExaminerID], [Username], [Password], [Name], [HoursAssigned], [IsActive]) VALUES (1, N'Mahesh', N'123', N'Mahesh', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Examiners] ([ExaminerID], [Username], [Password], [Name], [HoursAssigned], [IsActive]) VALUES (2, N'Deepa', N'deepa123', N'Deepthi', CAST(0 AS Decimal(18, 0)), 0)
INSERT [dbo].[Examiners] ([ExaminerID], [Username], [Password], [Name], [HoursAssigned], [IsActive]) VALUES (3, N'Veena', N'veena321', N'Veena Nair', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Examiners] ([ExaminerID], [Username], [Password], [Name], [HoursAssigned], [IsActive]) VALUES (4, N'Selma', N'selma', N'selma', CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[Examiners] ([ExaminerID], [Username], [Password], [Name], [HoursAssigned], [IsActive]) VALUES (6, N'dany', N'12345', N'Daniel', CAST(8 AS Decimal(18, 0)), 1)
INSERT [dbo].[Examiners] ([ExaminerID], [Username], [Password], [Name], [HoursAssigned], [IsActive]) VALUES (7, N'john', N'54321', N'John', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Examiners] ([ExaminerID], [Username], [Password], [Name], [HoursAssigned], [IsActive]) VALUES (8, N'meera', N'12345', N'Meera', CAST(4 AS Decimal(18, 0)), 1)
INSERT [dbo].[Examiners] ([ExaminerID], [Username], [Password], [Name], [HoursAssigned], [IsActive]) VALUES (10, N'deen', N'54321', N'deen', CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[Examiners] ([ExaminerID], [Username], [Password], [Name], [HoursAssigned], [IsActive]) VALUES (12, N'Test', N'123', N'Test', CAST(0 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[Examiners] OFF
GO
ALTER TABLE [dbo].[Examiners] ADD  CONSTRAINT [DF_Examiners_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Audits]  WITH CHECK ADD  CONSTRAINT [FK_Audits_AssociateExaminers] FOREIGN KEY([AssociateExaminerID])
REFERENCES [dbo].[Examiners] ([ExaminerID])
GO
ALTER TABLE [dbo].[Audits] CHECK CONSTRAINT [FK_Audits_AssociateExaminers]
GO
ALTER TABLE [dbo].[Audits]  WITH CHECK ADD  CONSTRAINT [FK_Audits_AuditStatuses] FOREIGN KEY([AuditStatusID])
REFERENCES [dbo].[AuditStatuses] ([AuditStatusID])
GO
ALTER TABLE [dbo].[Audits] CHECK CONSTRAINT [FK_Audits_AuditStatuses]
GO
ALTER TABLE [dbo].[Audits]  WITH CHECK ADD  CONSTRAINT [FK_Audits_BranchDetails] FOREIGN KEY([BranchID])
REFERENCES [dbo].[BranchDetails] ([BranchID])
GO
ALTER TABLE [dbo].[Audits] CHECK CONSTRAINT [FK_Audits_BranchDetails]
GO
ALTER TABLE [dbo].[Audits]  WITH CHECK ADD  CONSTRAINT [FK_Audits_LeadExaminers] FOREIGN KEY([LeadExaminerID])
REFERENCES [dbo].[Examiners] ([ExaminerID])
GO
ALTER TABLE [dbo].[Audits] CHECK CONSTRAINT [FK_Audits_LeadExaminers]
GO
