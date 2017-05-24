CREATE TABLE [dbo].[MailAccountFolder]
(
	[MailAccountId] INT NOT NULL,
	[Id] INT NOT NULL,
    CONSTRAINT [FK_MailAccountFolder_MailAccount] FOREIGN KEY (MailAccountId) REFERENCES dbo.MailAccount(id), 
    CONSTRAINT [PK_MailAccountFolder] PRIMARY KEY (Id, MailAccountId) 
)
