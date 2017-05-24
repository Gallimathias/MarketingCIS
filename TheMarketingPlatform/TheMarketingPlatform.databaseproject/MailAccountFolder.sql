CREATE TABLE [dbo].[MailAccountFolder]
(
	[MailAccountId] INT NOT NULL,
	[Id] INT NOT NULL IDENTITY,
    [Folder] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_MailAccountFolder_MailAccount] FOREIGN KEY (MailAccountId) REFERENCES dbo.MailAccount(Id), 
    CONSTRAINT [PK_MailAccountFolder] PRIMARY KEY ([Id], MailAccountId) 
)
