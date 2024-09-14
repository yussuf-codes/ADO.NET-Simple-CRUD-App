using System.Collections.Generic;
using Persistence.Models;

namespace Persistence.Repositories.IRepositories;

public interface INotesRepository
{
    Note Add(Note obj);
    void Delete(int id);
    bool Exists(int id);
    IEnumerable<Note> Get();
    Note Get(int id);
    void Update(int id, Note obj);
}
