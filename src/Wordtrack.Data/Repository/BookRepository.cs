using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> SaveNewBook(Book book)
        {
            context.Add(book);
            var writtenRows = await context.SaveChangesAsync();
            return writtenRows;
        }

        public Task<List<Book>> GetBooksAsync()
        {
            var books = context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await context.Books
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            return book;
        }
    }
}
