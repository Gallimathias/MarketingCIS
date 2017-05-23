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
PRINT N'Der folgende Vorgang wird aus der Umgestaltungsprotokolldatei "09602194-a8b5-4654-8e3e-8c44835de8eb" erstellt.';

PRINT N'"[dbo].[MailAddress].[Mail]" in "Adress" umbenennen';


GO
EXECUTE sp_rename @objname = N'[dbo].[MailAddress].[Mail]', @newname = N'Adress', @objtype = N'COLUMN';


GO
-- Umgestaltungsschritt zum Aktualisieren des Zielservers mit bereitgestellten Transaktionsprotokollen
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '09602194-a8b5-4654-8e3e-8c44835de8eb')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('09602194-a8b5-4654-8e3e-8c44835de8eb')

GO

GO
PRINT N'Update abgeschlossen.';


GO
