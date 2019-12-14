CREATE PROCEDURE [dbo].[MovieGenre_Insert]
	@Id INT, 
	@Name nvarchar(50),
	@SourceId INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT  INTO dbo.MovieGenre ([Name], SourceId)
    VALUES                (@Name, @SourceId);
    SELECT CAST(SCOPE_IDENTITY() as int)
END
