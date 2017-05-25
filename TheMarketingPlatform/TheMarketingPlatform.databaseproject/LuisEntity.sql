CREATE TABLE [dbo].[LuisEntity]
(
	[LuisResponseId] INT NOT NULL, 
    [Id] INT NOT NULL IDENTITY, 
    [Entity] NVARCHAR(MAX) NOT NULL, 
    [Type] NVARCHAR(MAX) NOT NULL, 
    [StartIndex] INT NULL, 
    [EndIndex] INT NULL, 
    [Score] FLOAT NOT NULL, 
    CONSTRAINT [FK_LuisEntity_LuisResponse] FOREIGN KEY (LuisResponseId) REFERENCES dbo.LuisResponse(Id), 
    CONSTRAINT [PK_LuisEntity] PRIMARY KEY (LuisResponseId, Id) 
)
