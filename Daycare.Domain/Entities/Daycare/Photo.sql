-- ============================================================================
-- Photo table (secure photo feature / Plan B)
-- This project does NOT use EF Core Migrations (no Migrations folder, dotnet-ef
-- not installed). The DB schema is managed manually, matching existing tables
-- such as Child / DeviceToken. Apply this script to the database manually.
-- Target DB: KCDB2 (see ConnectionStrings:myConnection)
-- NOTE: NOT yet applied to the database. Review before running.
-- ============================================================================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Photo')
BEGIN
    CREATE TABLE [dbo].[Photo] (
        [PhotoId]        INT IDENTITY(1,1) NOT NULL,
        [ChildId]        INT NULL,
        [OrganizationId] INT NULL,
        [BlobName]       NVARCHAR(MAX) NULL,
        [FileName]       NVARCHAR(MAX) NULL,
        [ContentType]    NVARCHAR(MAX) NULL,
        [Caption]        NVARCHAR(MAX) NULL,
        [UploadedBy]     NVARCHAR(450) NULL,
        [CreatedDate]    DATETIME2(7) NULL,
        [ActiveStatus]   BIT NULL,
        CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED ([PhotoId] ASC)
    );

    CREATE INDEX [IX_Photo_ChildId] ON [dbo].[Photo] ([ChildId]);
END
GO
