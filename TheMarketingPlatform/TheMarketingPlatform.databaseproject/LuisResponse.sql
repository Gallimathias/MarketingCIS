CREATE TABLE [dbo].[LuisResponse]
(
	[Id] INT NOT NULL IDENTITY, 
	[MailId] INT NOT NULL, 
    [TimeStamp] DATETIME NOT NULL, 
    CONSTRAINT [PK_LuisResponse] PRIMARY KEY (Id), 
    CONSTRAINT [AK_LuisResponse_MailId_TimeStamp] UNIQUE (MailId,TimeStamp), 
    CONSTRAINT [FK_LuisResponse_Mail] FOREIGN KEY (MailId) REFERENCES dbo.Mail(Id)
)
