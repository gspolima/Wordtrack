using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Domain;

namespace Wordtrack.Data.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooks(int count = 0);
        Task<Book> GetBook(int id);
        Task<List<Book>> GetBooksByAuthor(string author);
        Task<int> Save(Book book);
        Task<int> Update(Book book);
        Task<int> Delete(Book book);
    }
}
