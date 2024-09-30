using System.Collections.Generic;
using Persistence.Exceptions;
using Persistence.Models;
using Persistence.Repositories.IRepositories;

namespace Persistence.Services;

public class NotesService
{
    private readonly INotesRepository _repository;

    public NotesService(INotesRepository repository)
    {
        _repository = repository;
    }

    public void Add(Note obj) => _repository.Add(obj);

    public void Delete(int id)
    {
        if (!_repository.Exists(id))
            throw new NotFoundException();
        _repository.Delete(id);
    }

    public IEnumerable<Note> Get() => _repository.Get();

    public Note Get(int id)
    {
        if (!_repository.Exists(id))
            throw new NotFoundException();
        return _repository.Get(id);
    }

    public void Update(int id, Note obj)
    {
        if (id != obj.Id)
            throw new BadRequestException();
        if (!_repository.Exists(id))
            throw new NotFoundException();
        _repository.Update(id, obj);
    }
}
