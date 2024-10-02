using Persistence.Repositories.IRepositories;
using Persistence.Services;
using Persistence.Utilities.SQLProvider;

namespace Test;

class Program
{
    static void Main(string[] args)
    {
        ISQLProvider provider = SQLServerProvider.GetInstance();
        INotesRepository repository = new NotesRepository(provider);
        NotesService service = new(repository);
    }
}
