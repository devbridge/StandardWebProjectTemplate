/****** Object:  Table [dbo].[MultipartUploads]    Script Date: 03/15/2013 11:44:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MultipartUploads](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentId] [int] NOT NULL,
	[DocumentType] [int] NOT NULL,
	[KeyName] [nvarchar](500) NOT NULL,
	[ContentLength] [float] NOT NULL,
	[UploadId] [nvarchar](500) NOT NULL,
	[BucketName] [nvarchar](500) NOT NULL,
	[Hash] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_MultipartUploads] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
