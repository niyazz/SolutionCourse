USE [CourseWork]
GO

/****** Object:  Table [dbo].[Cars]    Script Date: 02.12.2019 20:32:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cars](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[region] [nchar](10) NOT NULL,
	[description] [nchar](10) NULL,
	[old] [int] NULL,
	[numbers] [nchar](10) NOT NULL,
	[model] [nchar](10) NOT NULL,
	[brand] [nchar](10) NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


