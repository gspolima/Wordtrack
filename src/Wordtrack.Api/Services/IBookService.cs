using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Api.Dtos;

namespace Wordtrack.Api.Services
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooks();
    }
}
