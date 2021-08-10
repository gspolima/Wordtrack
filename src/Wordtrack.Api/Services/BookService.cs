using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Api.Dtos;
using Wordtrack.Data.Repository;
using Wordtrack.Domain;

namespace Wordtrack.Api.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository repo;

        public BookService(IBookRepository bookRepo)
        {
            this.repo = bookRepo;
        }

        public async Task<List<BookDto>> GetAllBooks()
        {
            var booksFromRepo = await repo.GetBooksAsync();
            var books = new List<BookDto>();
            foreach (var book in booksFromRepo)
            {
                books.Add(new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    YearPublished = book.YearPublished,
                    Pages = book.Pages
                });
            }

            return books;
        }
        public async Task<Book> GetBook(int id)
        {
            var book = await repo.GetBookByIdAsync(id);

            if (book == null)
                return null;

            return book;
        }

        public async Task<int> AddBook(Book book)
        {
            await repo.SaveNewBook(book);
            return book.Id;
        }

    }
}
