CREATE TABLE [dbo].[MailAccount]
(
	[Id] INT NOT NULL IDENTITY,
	[Port] INT NOT NULL, 
    [Host] NVARCHAR(MAX) NOT NULL, 
    [Username] NVARCHAR(MAX) NOT NULL, 
    [Password] VARBINARY(255) NOT NULL,
	[UseSsl] bit NOT NULL,
    [Type] BINARY(1) NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_MailAccount] PRIMARY KEY (Id) 
)
