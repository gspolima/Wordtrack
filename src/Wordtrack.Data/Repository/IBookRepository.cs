using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Domain;

namespace Wordtrack.Data.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooks();
        Task<Book> GetBook(int id);
        Task<int> Save(Book book);
        Task<int> Update(Book book);
    }
}
