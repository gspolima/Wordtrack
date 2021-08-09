using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Api.Dtos;
using Wordtrack.Data.Repository;

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
            var booksFromRepo = await repo.GetBooks();
            var books = new List<BookDto>();
            foreach (var book in booksFromRepo)
            {
                books.Add(new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Published = book.PublishedYear,
                    Pages = book.Pages
                });
            }

            return books;
        }
    }
}
