CREATE TABLE [dbo].[MailAttachments]
(
	[MailId] INT NOT NULL, 
    [Id] INT NOT NULL IDENTITY, 
    [Attachment] VARBINARY(MAX) NOT NULL, 
    [FileExtension] NVARCHAR(260) NULL, 
    CONSTRAINT [FK_MailAttachments_Mail] FOREIGN KEY (MailId) REFERENCES dbo.Mail(Id), 
    CONSTRAINT [PK_MailAttachments] PRIMARY KEY (MailId,Id) 
)
