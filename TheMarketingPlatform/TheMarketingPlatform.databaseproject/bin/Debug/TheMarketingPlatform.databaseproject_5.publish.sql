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
USE [$(DatabaseName)];


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "8edd40ab-a604-4440-898a-9e8c9c9b3f30" wird übersprungen; das Element "[dbo].[MailAddresses].[Receiver]" (SqlSimpleColumn) wird nicht in "MailAddress" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "c28877ba-f20b-4c7c-b4af-ad73e1f4f5e8" wird übersprungen; das Element "[dbo].[PK_MailReceiver]" (SqlPrimaryKeyConstraint) wird nicht in "[PK_MailAddresses]" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "55dd02df-9341-4f93-9e9b-e05a83c1063b" wird übersprungen; das Element "[dbo].[FK_MailReceiver_Mail]" (SqlForeignKeyConstraint) wird nicht in "[FK_MailAddresses_Mail]" umbenannt.';


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
    [Id]        INT                IDENTITY (1, 1) NOT NULL,
    [MailId]    INT                NOT NULL,
    [TimeStamp] DATETIMEOFFSET (7) NOT NULL,
    CONSTRAINT [PK_LuisResponse] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_LuisResponse_MailId_TimeStamp] UNIQUE NONCLUSTERED ([MailId] ASC, [TimeStamp] ASC)
);


GO
PRINT N'[dbo].[Mail] wird erstellt....';


GO
CREATE TABLE [dbo].[Mail] (
    [Id]        INT                IDENTITY (1, 1) NOT NULL,
    [Subject]   NVARCHAR (254)     NULL,
    [Body]      NVARCHAR (MAX)     NOT NULL,
    [TimeStamp] DATETIMEOFFSET (7) NOT NULL,
    CONSTRAINT [PK_Mail] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'[dbo].[MailAddresses] wird erstellt....';


GO
CREATE TABLE [dbo].[MailAddresses] (
    [MailId]      INT            NOT NULL,
    [MailAddress] NVARCHAR (254) NOT NULL,
    CONSTRAINT [PK_MailAddresses] PRIMARY KEY CLUSTERED ([MailId] ASC, [MailAddress] ASC)
);


GO
PRINT N'[dbo].[MailAttachment] wird erstellt....';


GO
CREATE TABLE [dbo].[MailAttachment] (
    [MailId]        INT             NOT NULL,
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Attachment]    VARBINARY (MAX) NOT NULL,
    [FileExtension] NVARCHAR (260)  NULL,
    CONSTRAINT [PK_MailAttachment] PRIMARY KEY CLUSTERED ([MailId] ASC, [Id] ASC)
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
PRINT N'[dbo].[FK_MailAddresses_Mail] wird erstellt....';


GO
ALTER TABLE [dbo].[MailAddresses] WITH NOCHECK
    ADD CONSTRAINT [FK_MailAddresses_Mail] FOREIGN KEY ([MailId]) REFERENCES [dbo].[Mail] ([Id]);


GO
PRINT N'[dbo].[FK_MailAttachment_Mail] wird erstellt....';


GO
ALTER TABLE [dbo].[MailAttachment] WITH NOCHECK
    ADD CONSTRAINT [FK_MailAttachment_Mail] FOREIGN KEY ([MailId]) REFERENCES [dbo].[Mail] ([Id]);


GO
-- Umgestaltungsschritt zum Aktualisieren des Zielservers mit bereitgestellten Transaktionsprotokollen
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '8edd40ab-a604-4440-898a-9e8c9c9b3f30')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('8edd40ab-a604-4440-898a-9e8c9c9b3f30')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'c28877ba-f20b-4c7c-b4af-ad73e1f4f5e8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('c28877ba-f20b-4c7c-b4af-ad73e1f4f5e8')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '55dd02df-9341-4f93-9e9b-e05a83c1063b')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('55dd02df-9341-4f93-9e9b-e05a83c1063b')

GO

GO
PRINT N'Vorhandene Daten werden auf neu erstellte Einschränkungen hin überprüft.';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[LuisEntity] WITH CHECK CHECK CONSTRAINT [FK_LuisEntity_LuisResponse];

ALTER TABLE [dbo].[LuisIntent] WITH CHECK CHECK CONSTRAINT [FK_LuisIntent_LuisResponse];

ALTER TABLE [dbo].[LuisResponse] WITH CHECK CHECK CONSTRAINT [FK_LuisResponse_Mail];

ALTER TABLE [dbo].[MailAddresses] WITH CHECK CHECK CONSTRAINT [FK_MailAddresses_Mail];

ALTER TABLE [dbo].[MailAttachment] WITH CHECK CHECK CONSTRAINT [FK_MailAttachment_Mail];


GO
PRINT N'Update abgeschlossen.';


GO
