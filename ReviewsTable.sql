USE [CourseWork]
GO

/****** Object:  Table [dbo].[Messages]    Script Date: 10.11.2019 1:03:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Reviews](
	[id] [int] IDENTITY(0,1) NOT NULL,	
	[carnumber] [nvarchar](6) NOT NULL,	
	[text] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


  INSERT INTO [dbo].[Reviews]
           (
           [carNumber]
           ,[text])
     VALUES
           (
           '123456'
           ,'fhhhgdh');

