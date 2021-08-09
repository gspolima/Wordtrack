using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Domain;

namespace Wordtrack.Data.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooks();
    }
}
