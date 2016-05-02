USE [SportsTracker]
GO
/****** Object:  Table [dbo].[METCondition]    Script Date: 01-May-16 3:23:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[METCondition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartLimit] [float] NULL,
	[EndLimit] [float] NULL,
	[MET] [float] NOT NULL,
	[ActivityTypeId] [int] NOT NULL,
 CONSTRAINT [PK_METCondition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[METCondition] ON 

INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (2, 0, 2, 2, 3)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (4, 2, 3.4, 4.5, 3)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (5, 3.5, 4, 4.8, 3)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (6, 4.1, 5, 9.5, 3)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (7, 5, 9.9, 5, 1)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (8, 0, 4, 3, 1)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (9, 10, 11.9, 6.8, 1)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (11, 12, 13.9, 8, 1)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (12, 14, 15.9, 10, 1)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (13, 16, 19.9, 12, 1)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (14, 20, 9999999, 15.8, 1)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (15, 0, 3, 4, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (17, 3.1, 4, 6, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (18, 4.1, 5, 8.3, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (19, 5.1, 6, 9.8, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (20, 6.1, 7, 11, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (21, 7.1, 8, 11.8, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (22, 8.1, 9, 12.8, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (23, 9.1, 10, 14.5, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (24, 10.1, 11, 16, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (25, 11.1, 12, 19, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (26, 12.1, 13, 19.8, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (27, 13.1, 14, 23, 2)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (28, 5.1, 999999999, 9.5, 3)
INSERT [dbo].[METCondition] ([Id], [StartLimit], [EndLimit], [MET], [ActivityTypeId]) VALUES (29, 14.1, 9999999999, 23, 2)
SET IDENTITY_INSERT [dbo].[METCondition] OFF
