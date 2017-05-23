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
/*
Die Spalte "[dbo].[Mail].[From]" wird gelöscht, es könnte zu einem Datenverlust kommen.
*/

IF EXISTS (select top 1 1 from [dbo].[Mail])
    RAISERROR (N'Zeilen wurden erkannt. Das Schemaupdate wird beendet, da es möglicherweise zu einem Datenverlust kommt.', 16, 127) WITH NOWAIT

GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "8edd40ab-a604-4440-898a-9e8c9c9b3f30" wird übersprungen; das Element "[dbo].[MailAddresses].[Receiver]" (SqlSimpleColumn) wird nicht in "MailAddress" umbenannt.';


GO
PRINT N'Der folgende Vorgang wird aus der Umgestaltungsprotokolldatei "c28877ba-f20b-4c7c-b4af-ad73e1f4f5e8" erstellt.';

PRINT N'"[dbo].[PK_MailReceiver]" in "PK_MailAddresses" umbenennen';


GO
EXECUTE sp_rename @objname = N'[dbo].[PK_MailReceiver]', @newname = N'PK_MailAddresses', @objtype = N'OBJECT';


GO
PRINT N'Der folgende Vorgang wird aus der Umgestaltungsprotokolldatei "55dd02df-9341-4f93-9e9b-e05a83c1063b" erstellt.';

PRINT N'"[dbo].[FK_MailReceiver_Mail]" in "FK_MailAddresses_Mail" umbenennen';


GO
EXECUTE sp_rename @objname = N'[dbo].[FK_MailReceiver_Mail]', @newname = N'FK_MailAddresses_Mail', @objtype = N'OBJECT';


GO
PRINT N'[dbo].[AK_LuisResponse_MailId_TimeStamp] wird gelöscht....';


GO
ALTER TABLE [dbo].[LuisResponse] DROP CONSTRAINT [AK_LuisResponse_MailId_TimeStamp];


GO
PRINT N'[dbo].[LuisResponse] wird geändert....';


GO
ALTER TABLE [dbo].[LuisResponse] ALTER COLUMN [TimeStamp] DATETIMEOFFSET (7) NOT NULL;


GO
PRINT N'[dbo].[AK_LuisResponse_MailId_TimeStamp] wird erstellt....';


GO
ALTER TABLE [dbo].[LuisResponse]
    ADD CONSTRAINT [AK_LuisResponse_MailId_TimeStamp] UNIQUE NONCLUSTERED ([MailId] ASC, [TimeStamp] ASC);


GO
PRINT N'[dbo].[Mail] wird geändert....';


GO
ALTER TABLE [dbo].[Mail] DROP COLUMN [From];


GO
PRINT N'[dbo].[MailAddresses] wird erstellt....';


GO
CREATE TABLE [dbo].[MailAddresses] (
    [MailId]      INT            NOT NULL,
    [MailAddress] NVARCHAR (254) NOT NULL,
    CONSTRAINT [PK_MailAddresses] PRIMARY KEY CLUSTERED ([MailId] ASC, [MailAddress] ASC)
);


GO
PRINT N'[dbo].[FK_MailAddresses_Mail] wird erstellt....';


GO
ALTER TABLE [dbo].[MailAddresses] WITH NOCHECK
    ADD CONSTRAINT [FK_MailAddresses_Mail] FOREIGN KEY ([MailId]) REFERENCES [dbo].[Mail] ([Id]);


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
ALTER TABLE [dbo].[MailAddresses] WITH CHECK CHECK CONSTRAINT [FK_MailAddresses_Mail];


GO
PRINT N'Update abgeschlossen.';


GO
