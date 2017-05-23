CREATE TABLE [dbo].[MailAddresses]
(
	[MailId] INT NOT NULL, 
    [MailAddress] NVARCHAR(254) NOT NULL, 
    CONSTRAINT [FK_MailAddresses_Mail] FOREIGN KEY ([MailId]) REFERENCES dbo.Mail(Id), 
    CONSTRAINT [PK_MailAddresses] PRIMARY KEY ([MailId],[MailAddress]) 
)
