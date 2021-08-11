using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Api.Dtos;
using Wordtrack.Domain;

namespace Wordtrack.Api.Services
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooks();
        Task<Book> GetBook(int id);
        Task<int> AddBook(Book book);
        Task<int> EditBook(Book book);
    }
}
