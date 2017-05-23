CREATE TABLE [dbo].[LuisIntent]
(
	[LuisResponseId] INT NOT NULL, 
    [Id] INT NOT NULL, 
    [Intent] NVARCHAR(MAX) NOT NULL, 
    [Score] FLOAT NOT NULL, 
    [IsTopScore] BIT NOT NULL, 
    CONSTRAINT [FK_LuisIntent_LuisResponse] FOREIGN KEY (LuisResponseId) REFERENCES dbo.LuisResponse(Id), 
    CONSTRAINT [PK_LuisIntent] PRIMARY KEY (LuisResponseId, Id) 
)
