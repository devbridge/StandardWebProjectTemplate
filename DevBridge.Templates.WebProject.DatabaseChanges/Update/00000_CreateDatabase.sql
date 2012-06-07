/****** Object:  Table [dbo].[CustomerType]    Script Date: 09/16/2011 15:20:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CustomerType](
	[Type] [int] NOT NULL,
	[Title] [varchar](20) NOT NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_CustomerType] PRIMARY KEY CLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 09/16/2011 15:20:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nchar](10) NOT NULL,
	[Type] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Agreement]    Script Date: 09/16/2011 15:20:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agreement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Agreement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nchar](20) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Agreement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Agreement]') AND name = N'IX_Agreement_CustomerId')
CREATE NONCLUSTERED INDEX [IX_Agreement_CustomerId] ON [dbo].[Agreement] 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Agreement]') AND name = N'Unique_Agreement_Number')
CREATE NONCLUSTERED INDEX [Unique_Agreement_Number] ON [dbo].[Agreement] 
(
	[Number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Default [DF_Agreement_CreatedOn]    Script Date: 09/16/2011 15:20:34 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Agreement_CreatedOn]') AND parent_object_id = OBJECT_ID(N'[dbo].[Agreement]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Agreement_CreatedOn]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Agreement] ADD  CONSTRAINT [DF_Agreement_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END


End
GO
/****** Object:  Default [DF_Agreement_IsDeleted]    Script Date: 09/16/2011 15:20:34 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Agreement_IsDeleted]') AND parent_object_id = OBJECT_ID(N'[dbo].[Agreement]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Agreement_IsDeleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Agreement] ADD  CONSTRAINT [DF_Agreement_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
END


End
GO
/****** Object:  Default [DF_Customer_CreatedOn]    Script Date: 09/16/2011 15:20:34 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_CreatedOn]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_CreatedOn]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
END


End
GO
/****** Object:  Default [DF_Customer_IsDeleted]    Script Date: 09/16/2011 15:20:34 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Customer_IsDeleted]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customer]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Customer_IsDeleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
END


End
GO
/****** Object:  ForeignKey [FK_Agreement_Customer]    Script Date: 09/16/2011 15:20:34 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Agreement_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Agreement]'))
ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD  CONSTRAINT [FK_Agreement_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Agreement_Customer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Agreement]'))
ALTER TABLE [dbo].[Agreement] CHECK CONSTRAINT [FK_Agreement_Customer]
GO
