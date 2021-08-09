using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Domain;

namespace Wordtrack.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly WordtrackContext context;
        public BookRepository(WordtrackContext context)
        {
            this.context = context;
        }

        public Task<List<Book>> GetBooks()
        {
            var books = context.Books.ToListAsync();
            return books;
        }
    }
}
