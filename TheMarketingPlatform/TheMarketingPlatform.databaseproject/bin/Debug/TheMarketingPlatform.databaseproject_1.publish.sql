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
PRINT N'Der folgende Vorgang wird aus der Umgestaltungsprotokolldatei "f83a1c36-63f0-43d8-8a96-ee246122d01c" erstellt.';

PRINT N'"[dbo].[FK_MailAttachments_Mail]" in "FK_MailAttachment_Mail" umbenennen';


GO
EXECUTE sp_rename @objname = N'[dbo].[FK_MailAttachments_Mail]', @newname = N'FK_MailAttachment_Mail', @objtype = N'OBJECT';


GO
PRINT N'Der folgende Vorgang wird aus der Umgestaltungsprotokolldatei "05e21239-620e-4aed-b49b-651a15e072e5" erstellt.';

PRINT N'"[dbo].[PK_MailAttachments]" in "PK_MailAttachment" umbenennen';


GO
EXECUTE sp_rename @objname = N'[dbo].[PK_MailAttachments]', @newname = N'PK_MailAttachment', @objtype = N'OBJECT';


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
PRINT N'[dbo].[FK_MailAttachment_Mail] wird erstellt....';


GO
ALTER TABLE [dbo].[MailAttachment] WITH NOCHECK
    ADD CONSTRAINT [FK_MailAttachment_Mail] FOREIGN KEY ([MailId]) REFERENCES [dbo].[Mail] ([Id]);


GO
-- Umgestaltungsschritt zum Aktualisieren des Zielservers mit bereitgestellten Transaktionsprotokollen
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'f83a1c36-63f0-43d8-8a96-ee246122d01c')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('f83a1c36-63f0-43d8-8a96-ee246122d01c')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '05e21239-620e-4aed-b49b-651a15e072e5')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('05e21239-620e-4aed-b49b-651a15e072e5')

GO

GO
PRINT N'Vorhandene Daten werden auf neu erstellte Einschränkungen hin überprüft.';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[MailAttachment] WITH CHECK CHECK CONSTRAINT [FK_MailAttachment_Mail];


GO
PRINT N'Update abgeschlossen.';


GO
