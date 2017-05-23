CREATE TABLE [dbo].[MailAttachment]
(
	[MailId] INT NOT NULL, 
    [Id] INT NOT NULL IDENTITY, 
    [Attachment] VARBINARY(MAX) NOT NULL, 
    [FileExtension] NVARCHAR(260) NULL, 
    CONSTRAINT [FK_MailAttachment_Mail] FOREIGN KEY (MailId) REFERENCES dbo.Mail(Id), 
    CONSTRAINT [PK_MailAttachment] PRIMARY KEY (MailId,Id) 
)
