CREATE TABLE [dbo].[MailReceiver]
(
	[MailId] INT NOT NULL, 
    [Receiver] NVARCHAR(254) NOT NULL, 
    CONSTRAINT [FK_MailReceiver_Mail] FOREIGN KEY ([MailId]) REFERENCES dbo.Mail(Id), 
    CONSTRAINT [PK_MailReceiver] PRIMARY KEY ([MailId],Receiver) 
)
