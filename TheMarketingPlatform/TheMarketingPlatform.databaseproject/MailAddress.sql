CREATE TABLE [dbo].[MailAddress]
(
	[MailId] INT NOT NULL, 
    [Adress] NVARCHAR(254) NOT NULL, 
    [Type] BINARY(1) NOT NULL, 
    CONSTRAINT [FK_MailAddress_Mail] FOREIGN KEY ([MailId]) REFERENCES dbo.Mail(Id), 
    CONSTRAINT [PK_MailAddress] PRIMARY KEY ([MailId],[Adress]) 
)
