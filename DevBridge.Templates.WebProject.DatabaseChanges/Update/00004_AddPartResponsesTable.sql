/****** Object:  Table [dbo].[PartResponses]    Script Date: 03/15/2013 11:45:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PartResponses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartNumber] [int] NOT NULL,
	[ETag] [nvarchar](250) NOT NULL,
	[MultipartUploadId] [int] NOT NULL,
 CONSTRAINT [PK_PartResponses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PartResponses]  WITH CHECK ADD  CONSTRAINT [FK_PartResponses_MultipartUploads] FOREIGN KEY([MultipartUploadId])
REFERENCES [dbo].[MultipartUploads] ([Id])
GO

ALTER TABLE [dbo].[PartResponses] CHECK CONSTRAINT [FK_PartResponses_MultipartUploads]
GO