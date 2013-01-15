/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Agreement ADD
	ModifiedOn datetime NOT NULL CONSTRAINT DF_Agreement_ModifiedOn DEFAULT getdate(),
	CreatedBy nvarchar(50) NOT NULL CONSTRAINT DF_Agreement_CreatedBy DEFAULT N'',
	ModifiedBy nvarchar(50) NOT NULL CONSTRAINT DF_Agreement_ModifiedBy DEFAULT N'',
	DeletedBy nvarchar(50) NULL
GO
ALTER TABLE dbo.Agreement SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
