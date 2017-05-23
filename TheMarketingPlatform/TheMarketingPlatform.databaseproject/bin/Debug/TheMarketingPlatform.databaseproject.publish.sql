/*
Bereitstellungsskript für TheMarketingPlatform

Dieser Code wurde von einem Tool generiert.
Änderungen an dieser Datei führen möglicherweise zu falschem Verhalten und gehen verloren, falls
der Code neu generiert wird.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TheMarketingPlatform"
:setvar DefaultFilePrefix "TheMarketingPlatform"
:setvar DefaultDataPath "D:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "D:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Überprüfen Sie den SQLCMD-Modus, und deaktivieren Sie die Skriptausführung, wenn der SQLCMD-Modus nicht unterstützt wird.
Um das Skript nach dem Aktivieren des SQLCMD-Modus erneut zu aktivieren, führen Sie folgenden Befehl aus:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Der SQLCMD-Modus muss aktiviert sein, damit dieses Skript erfolgreich ausgeführt werden kann.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367)) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "cfe46511-bfbe-4789-a5f5-ec7f090f2bc8" wird übersprungen; das Element "[dbo].[MailAttachments].[Id]" (SqlSimpleColumn) wird nicht in "MailId" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "39b02dcd-fc55-4e69-ad3f-16ac4e845d7c" wird übersprungen; das Element "[dbo].[MailAttachments].[AttachmentId]" (SqlSimpleColumn) wird nicht in "Id" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "41462b17-1dbe-44cc-a752-e9cbbbaab425" wird übersprungen; das Element "[dbo].[LuisScore].[Id]" (SqlSimpleColumn) wird nicht in "MailId" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "41752177-fc0e-42e0-a81c-73637f45dc74" wird übersprungen; das Element "[dbo].[MailReceiver].[Id]" (SqlSimpleColumn) wird nicht in "MailId" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "81145bc8-e895-4204-a4af-3449ee8db789" wird übersprungen; das Element "[dbo].[PK_LuisScore]" (SqlPrimaryKeyConstraint) wird nicht in "[PK_LuisTopScore]" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "e860af23-e7cd-4d52-9c71-6f951d5cfbf8, c1f256cc-951a-4506-8456-9fc5fe13a488" wird übersprungen; das Element "[dbo].[FK_LuisScore_Mail]" (SqlForeignKeyConstraint) wird nicht in "[FK_LuisTopScore_LuisResponse]" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "6b13f5dc-a3c1-43e5-8811-775a3ffc6cfb" wird übersprungen; das Element "[dbo].[LuisResponse].[Id]" (SqlSimpleColumn) wird nicht in "MailId" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "0514c05e-89cc-4884-9a2e-182cb1343826" wird übersprungen; das Element "[dbo].[LuisTopScore].[MailId]" (SqlSimpleColumn) wird nicht in "LuisResponseId" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "adeba05f-62e3-47b9-bd2d-e20beed54aa4" wird übersprungen; das Element "[dbo].[LuisItent].[Id]" (SqlSimpleColumn) wird nicht in "LuisResponseId" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "52aa0260-f7c3-4541-869a-efbdd804ede2" wird übersprungen; das Element "[dbo].[LuisEntity].[Id]" (SqlSimpleColumn) wird nicht in "LuisResponseId" umbenannt.';


GO
PRINT N'[dbo].[LuisEntity] wird erstellt....';


GO
CREATE TABLE [dbo].[LuisEntity] (
    [LuisResponseId] INT            NOT NULL,
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Entity]         NVARCHAR (MAX) NOT NULL,
    [Type]           NVARCHAR (MAX) NOT NULL,
    [StartIndex]     INT            NULL,
    [EndIndex]       INT            NULL,
    [Score]          FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_LuisEntity] PRIMARY KEY CLUSTERED ([LuisResponseId] ASC, [Id] ASC)
);


GO
PRINT N'[dbo].[LuisIntent] wird erstellt....';


GO
CREATE TABLE [dbo].[LuisIntent] (
    [LuisResponseId] INT            NOT NULL,
    [Id]             INT            NOT NULL,
    [Intent]         NVARCHAR (MAX) NOT NULL,
    [Score]          FLOAT (53)     NOT NULL,
    [IsTopScore]     BIT            NOT NULL,
    CONSTRAINT [PK_LuisIntent] PRIMARY KEY CLUSTERED ([LuisResponseId] ASC, [Id] ASC)
);


GO
PRINT N'[dbo].[LuisResponse] wird erstellt....';


GO
CREATE TABLE [dbo].[LuisResponse] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [MailId]    INT      NOT NULL,
    [TimeStamp] DATETIME NOT NULL,
    CONSTRAINT [PK_LuisResponse] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_LuisResponse_MailId_TimeStamp] UNIQUE NONCLUSTERED ([MailId] ASC, [TimeStamp] ASC)
);


GO
PRINT N'[dbo].[Mail] wird erstellt....';


GO
CREATE TABLE [dbo].[Mail] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [From]      NVARCHAR (254) NOT NULL,
    [Subject]   NVARCHAR (254) NULL,
    [Body]      NVARCHAR (MAX) NOT NULL,
    [TimeStamp] DATETIME       NOT NULL,
    CONSTRAINT [PK_Mail] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'[dbo].[MailAttachments] wird erstellt....';


GO
CREATE TABLE [dbo].[MailAttachments] (
    [MailId]        INT             NOT NULL,
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Attachment]    VARBINARY (MAX) NOT NULL,
    [FileExtension] NVARCHAR (260)  NULL,
    CONSTRAINT [PK_MailAttachments] PRIMARY KEY CLUSTERED ([MailId] ASC, [Id] ASC)
);


GO
PRINT N'[dbo].[MailReceiver] wird erstellt....';


GO
CREATE TABLE [dbo].[MailReceiver] (
    [MailId]   INT            NOT NULL,
    [Receiver] NVARCHAR (254) NOT NULL,
    CONSTRAINT [PK_MailReceiver] PRIMARY KEY CLUSTERED ([MailId] ASC, [Receiver] ASC)
);


GO
PRINT N'[dbo].[FK_LuisEntity_LuisResponse] wird erstellt....';


GO
ALTER TABLE [dbo].[LuisEntity] WITH NOCHECK
    ADD CONSTRAINT [FK_LuisEntity_LuisResponse] FOREIGN KEY ([LuisResponseId]) REFERENCES [dbo].[LuisResponse] ([Id]);


GO
PRINT N'[dbo].[FK_LuisIntent_LuisResponse] wird erstellt....';


GO
ALTER TABLE [dbo].[LuisIntent] WITH NOCHECK
    ADD CONSTRAINT [FK_LuisIntent_LuisResponse] FOREIGN KEY ([LuisResponseId]) REFERENCES [dbo].[LuisResponse] ([Id]);


GO
PRINT N'[dbo].[FK_LuisResponse_Mail] wird erstellt....';


GO
ALTER TABLE [dbo].[LuisResponse] WITH NOCHECK
    ADD CONSTRAINT [FK_LuisResponse_Mail] FOREIGN KEY ([MailId]) REFERENCES [dbo].[Mail] ([Id]);


GO
PRINT N'[dbo].[FK_MailAttachments_Mail] wird erstellt....';


GO
ALTER TABLE [dbo].[MailAttachments] WITH NOCHECK
    ADD CONSTRAINT [FK_MailAttachments_Mail] FOREIGN KEY ([MailId]) REFERENCES [dbo].[Mail] ([Id]);


GO
PRINT N'[dbo].[FK_MailReceiver_Mail] wird erstellt....';


GO
ALTER TABLE [dbo].[MailReceiver] WITH NOCHECK
    ADD CONSTRAINT [FK_MailReceiver_Mail] FOREIGN KEY ([MailId]) REFERENCES [dbo].[Mail] ([Id]);


GO
-- Umgestaltungsschritt zum Aktualisieren des Zielservers mit bereitgestellten Transaktionsprotokollen

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'cfe46511-bfbe-4789-a5f5-ec7f090f2bc8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('cfe46511-bfbe-4789-a5f5-ec7f090f2bc8')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '39b02dcd-fc55-4e69-ad3f-16ac4e845d7c')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('39b02dcd-fc55-4e69-ad3f-16ac4e845d7c')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '41462b17-1dbe-44cc-a752-e9cbbbaab425')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('41462b17-1dbe-44cc-a752-e9cbbbaab425')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '41752177-fc0e-42e0-a81c-73637f45dc74')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('41752177-fc0e-42e0-a81c-73637f45dc74')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '81145bc8-e895-4204-a4af-3449ee8db789')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('81145bc8-e895-4204-a4af-3449ee8db789')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'e860af23-e7cd-4d52-9c71-6f951d5cfbf8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('e860af23-e7cd-4d52-9c71-6f951d5cfbf8')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '6b13f5dc-a3c1-43e5-8811-775a3ffc6cfb')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('6b13f5dc-a3c1-43e5-8811-775a3ffc6cfb')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '0514c05e-89cc-4884-9a2e-182cb1343826')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('0514c05e-89cc-4884-9a2e-182cb1343826')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'c1f256cc-951a-4506-8456-9fc5fe13a488')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('c1f256cc-951a-4506-8456-9fc5fe13a488')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'adeba05f-62e3-47b9-bd2d-e20beed54aa4')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('adeba05f-62e3-47b9-bd2d-e20beed54aa4')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '52aa0260-f7c3-4541-869a-efbdd804ede2')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('52aa0260-f7c3-4541-869a-efbdd804ede2')

GO

GO
PRINT N'Vorhandene Daten werden auf neu erstellte Einschränkungen hin überprüft.';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[LuisEntity] WITH CHECK CHECK CONSTRAINT [FK_LuisEntity_LuisResponse];

ALTER TABLE [dbo].[LuisIntent] WITH CHECK CHECK CONSTRAINT [FK_LuisIntent_LuisResponse];

ALTER TABLE [dbo].[LuisResponse] WITH CHECK CHECK CONSTRAINT [FK_LuisResponse_Mail];

ALTER TABLE [dbo].[MailAttachments] WITH CHECK CHECK CONSTRAINT [FK_MailAttachments_Mail];

ALTER TABLE [dbo].[MailReceiver] WITH CHECK CHECK CONSTRAINT [FK_MailReceiver_Mail];


GO
PRINT N'Update abgeschlossen.';


GO
