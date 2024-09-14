CREATE PROCEDURE DeleteNote
    @id INT
AS
    DELETE FROM dbo.Notes WHERE dbo.Notes.Id = @id;
GO

CREATE PROCEDURE GetNote
    @id INT
AS
    SELECT * FROM dbo.Notes WHERE dbo.Notes.Id = @id;
GO

CREATE PROCEDURE GetNotes
AS
    SELECT * FROM dbo.Notes;
GO

CREATE PROCEDURE NoteExists
    @id INT
AS
    DECLARE @exists BIT
    IF EXISTS (SELECT * FROM dbo.Notes WHERE dbo.Notes.Id = @id)
        SET @exists = 1
    ELSE
        SET @exists = 0
    SELECT @exists;
GO

CREATE PROCEDURE AddNote
    @title VARCHAR(255), @body VARCHAR(max)
AS
    INSERT INTO dbo.Notes (dbo.Notes.Title, dbo.Notes.Body) VALUES (@title, @body)
    SELECT SCOPE_IDENTITY();
GO

CREATE PROCEDURE UpdateNote
    @id INT, @title VARCHAR(255), @body VARCHAR(max)
AS
    UPDATE dbo.Notes SET dbo.Notes.Title = @title, dbo.Notes.Body = @body WHERE dbo.Notes.Id = @id;
GO
