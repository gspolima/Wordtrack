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

        public Task<List<Book>> GetBooks()
        {
            var books = context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetBook(int id)
        {
            var book = await context.Books
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            return book;
        }

        public async Task<int> Save(Book book)
        {
            context.Add(book);
            var writtenRows = await context.SaveChangesAsync();
            return writtenRows;
        }

        public async Task<int> Update(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
            var writtenRows = await context.SaveChangesAsync();
            return writtenRows;
        }

        public async Task<int> Delete(Book book)
        {
            context.Books.Remove(book);
            var deletedRows = await context.SaveChangesAsync();
            return deletedRows;
        }
    }
}
