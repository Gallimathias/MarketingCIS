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
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "3f19fdf6-ba07-41d4-9a18-807387af91dc" wird übersprungen; das Element "[dbo].[PK_MailAddresses]" (SqlPrimaryKeyConstraint) wird nicht in "[PK_MailAddress]" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "2cf83723-de3d-416f-8c2f-243213e50553" wird übersprungen; das Element "[dbo].[FK_MailAddresses_Mail]" (SqlForeignKeyConstraint) wird nicht in "[FK_MailAddress_Mail]" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "d3a9b2fd-7614-45c8-81f6-37339f5979d7" wird übersprungen; das Element "[dbo].[MailAccount].[host]" (SqlSimpleColumn) wird nicht in "Host" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "095ee2d7-8c38-43fd-a3fe-790752fe181f" wird übersprungen; das Element "[dbo].[MailAccount].[port]" (SqlSimpleColumn) wird nicht in "Port" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "5902e9b9-871c-4cba-9535-181f7d97124f" wird übersprungen; das Element "[dbo].[MailAccount].[username]" (SqlSimpleColumn) wird nicht in "Username" umbenannt.';


GO
PRINT N'Der Umgestaltungsvorgang mit Umbenennung mit Schlüssel "9e1625e1-652b-4063-aa6b-71791a21a437" wird übersprungen; das Element "[dbo].[MailAccount].[password]" (SqlSimpleColumn) wird nicht in "Password" umbenannt.';


GO
PRINT N'[dbo].[MailAccount] wird erstellt....';


GO
CREATE TABLE [dbo].[MailAccount] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [Port]     INT             NOT NULL,
    [Host]     NVARCHAR (MAX)  NOT NULL,
    [Username] NVARCHAR (MAX)  NOT NULL,
    [Password] VARBINARY (255) NOT NULL,
    [Type]     BINARY (1)      NOT NULL,
    CONSTRAINT [PK_MailAccount] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'[dbo].[MailAddress] wird erstellt....';


GO
CREATE TABLE [dbo].[MailAddress] (
    [MailId]      INT            NOT NULL,
    [MailAddress] NVARCHAR (254) NOT NULL,
    [Type]        BINARY (1)     NOT NULL,
    CONSTRAINT [PK_MailAddress] PRIMARY KEY CLUSTERED ([MailId] ASC, [MailAddress] ASC)
);


GO
PRINT N'[dbo].[FK_MailAddress_Mail] wird erstellt....';


GO
ALTER TABLE [dbo].[MailAddress] WITH NOCHECK
    ADD CONSTRAINT [FK_MailAddress_Mail] FOREIGN KEY ([MailId]) REFERENCES [dbo].[Mail] ([Id]);


GO
-- Umgestaltungsschritt zum Aktualisieren des Zielservers mit bereitgestellten Transaktionsprotokollen
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '3f19fdf6-ba07-41d4-9a18-807387af91dc')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('3f19fdf6-ba07-41d4-9a18-807387af91dc')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '2cf83723-de3d-416f-8c2f-243213e50553')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('2cf83723-de3d-416f-8c2f-243213e50553')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'd3a9b2fd-7614-45c8-81f6-37339f5979d7')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('d3a9b2fd-7614-45c8-81f6-37339f5979d7')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '095ee2d7-8c38-43fd-a3fe-790752fe181f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('095ee2d7-8c38-43fd-a3fe-790752fe181f')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '5902e9b9-871c-4cba-9535-181f7d97124f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('5902e9b9-871c-4cba-9535-181f7d97124f')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9e1625e1-652b-4063-aa6b-71791a21a437')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9e1625e1-652b-4063-aa6b-71791a21a437')

GO

GO
PRINT N'Vorhandene Daten werden auf neu erstellte Einschränkungen hin überprüft.';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[MailAddress] WITH CHECK CHECK CONSTRAINT [FK_MailAddress_Mail];


GO
PRINT N'Update abgeschlossen.';


GO
