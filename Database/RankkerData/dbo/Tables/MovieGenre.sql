CREATE TABLE [dbo].[MovieGenre]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [SourceId] INT NOT NULL,
	CONSTRAINT [moviegenre_namesourceid] UNIQUE NONCLUSTERED (
		[Name], [SourceId]
	)
)

