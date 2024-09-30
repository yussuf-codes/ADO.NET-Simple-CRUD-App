using System;
using System.Collections.Generic;
using System.Data;
using Persistence.Models;
using Persistence.Repositories.IRepositories;
using Persistence.Utilities.SQLProvider;

public class NotesRepository : INotesRepository
{
    private readonly ISQLProvider _provider;

    public NotesRepository(ISQLProvider provider)
    {
        _provider = provider;
    }

    public void Add(Note obj)
    {
        List<StoredProcedureArg> args = new()
        {
            new () { Name = "@title", Value = obj.Title },
            new () { Name = "@body", Value = obj.Body }
        };

        _provider.ExecuteCommand("AddNote", args);
    }

    public void Delete(int id)
    {
        List<StoredProcedureArg> args = new()
        {
            new () { Name = "@id", Value = id }
        };

        _provider.ExecuteCommand("DeleteNote", args);
    }

    public bool Exists(int id)
    {
        List<StoredProcedureArg> args = new()
        {
            new () { Name = "@id", Value = id }
        };

        if ((bool)_provider.ExecuteScalar("NoteExists", args))
            return true;
        return false;
    }

    public IEnumerable<Note> Get()
    {
        DataTable dataTable = (DataTable)_provider.ExecuteQuery("GetNotes");

        List<Note> notes = new();

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            Note note = new Note()
            {
                Id = (int)dataTable.Rows[i]["Id"],
                Title = (string)dataTable.Rows[i]["Title"],
                Body = (string)dataTable.Rows[i]["Body"]
            };

            notes.Add(note);
        }

        return notes;
    }

    public Note Get(int id)
    {
        List<StoredProcedureArg> args = new()
        {
            new () { Name = "@id", Value = id }
        };

        DataTable dataTable = (DataTable)_provider.ExecuteQuery("GetNote", args);

        Note note = new Note()
        {
            Id = (int)dataTable.Rows[0]["Id"],
            Title = (string)dataTable.Rows[0]["Title"],
            Body = (string)dataTable.Rows[0]["Body"]
        };

        return note;
    }

    public void Update(int id, Note obj)
    {
        List<StoredProcedureArg> args = new()
        {
            new () { Name = "@id", Value = id },
            new () { Name = "@title", Value = obj.Title },
            new () { Name = "@body", Value = obj.Body }
        };

        _provider.ExecuteCommand("UpdateNote", args);
    }
}
