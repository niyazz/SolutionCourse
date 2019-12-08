USE [CourseWork]
GO

/****** Objecöót:  Table [dbo].[Friends]    Script Date: 08.12.2019 15:27:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Friends](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[friend_id] [int] NOT NULL,
	[friend_name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Friends] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


