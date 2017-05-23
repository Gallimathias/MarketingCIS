CREATE TABLE [dbo].[MailReceiver]
(
	[Id] INT NOT NULL, 
    [Receiver] NVARCHAR(254) NOT NULL, 
    CONSTRAINT [FK_MailReceiver_Mail] FOREIGN KEY (Id) REFERENCES dbo.Mail(Id), 
    CONSTRAINT [PK_MailReceiver] PRIMARY KEY (Id,Receiver) 
)
