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

        public Task<List<Book>> GetBooks(int count = 0)
        {

            if (count > 0)
            {
                return context.Books
                    .Take(count)
                    .OrderBy(b => b.Id)
                    .ToListAsync();
            }

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

        public async Task<List<Book>> GetBooksByAuthor(string author)
        {
            var books = await context.Books
                .Where(b => b.Author.ToLower() == author.ToLower())
                .OrderBy(b => b.YearPublished)
                .ThenBy(b => b.Title)
                .ToListAsync();

            return books;
        }

        public async Task<List<Book>> GetBooksByTitle(string title)
        {
            var books = await context.Books
                .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                .OrderBy(b => b.YearPublished)
                .ThenBy(b => b.Title)
                .ToListAsync();

            return books;
        }

        public async Task<List<Book>> GetBooksByPublishingYear(int year)
        {
            var books = await context.Books
                .Where(b => b.YearPublished == year)
                .OrderBy(b => b.Title)
                .ToListAsync();

            return books;
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
