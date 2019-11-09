USE [CourseWork]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 10.11.2019 1:03:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[sername] [nvarchar](50) NOT NULL,
	[datebirth] [datetime] NOT NULL,
	[liters] [int] NOT NULL,
	[mail] [nvarchar](max) NOT NULL,
	[password] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_liters]  DEFAULT ((0)) FOR [liters]
GO


  INSERT INTO [CourseWork].[dbo].[Users2] VALUES ('admin','John', 'Smith', '1988-10-10',100,'admin-surer@mail.com','12345');