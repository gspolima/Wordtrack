using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Domain;

namespace Wordtrack.Api.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetBooks(int? count = 0);
        Task<Book> GetBook(int id);
        Task<int> AddBook(Book book);
        Task<int> EditBook(Book book);

        Task<bool> RemoveBook(int id);
    }
}
