USE [CourseWork]
GO

/****** Object:  Table [dbo].[Messages]    Script Date: 10.11.2019 1:03:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Messages](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[sId] [int] NOT NULL,
	[aId] [int] NOT NULL,
	[sName] [nvarchar](50) NOT NULL,
	[aName] [nvarchar](50) NOT NULL,
	[time] [datetime] NOT NULL,
	[text] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


  INSERT INTO [CourseWork].[dbo].[Messages] VALUES (0, 1, 'Sersya', 'John', '1988-10-10 10:12:09 AM', 'Фу чьмо игнориую');
  INSERT INTO [CourseWork].[dbo].[Messages] VALUES (0, 1, 'Bran', 'John', '1988-10-10 11:30:59 AM', 'А я знаю что ты дрочишь');