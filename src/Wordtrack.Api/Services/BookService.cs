using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<Book>> GetBooks(int? count = 0)
        {
            if (count.HasValue)
                return await repo.GetBooks(count.Value);

            var booksFromRepo = await repo.GetBooks();
            return booksFromRepo;
        }
        public async Task<Book> GetBook(int id)
        {
            var book = await repo.GetBook(id);

            if (book == null)
                return null;

            return book;
        }

        public async Task<List<Book>> SearchBooksByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author) || author.Length < 4)
                return null;

            var books = await repo.GetBooksByAuthor(author);
            return books;
        }
        public async Task<List<Book>> SearchBooksByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length < 2)
                return null;

            var books = await repo.GetBooksByTitle(title);

            return books;
        }

        public async Task<List<Book>> SearchBooksByPublisingYear(int year)
        {
            if (year > DateTime.Now.Year || year == 0)
                return null;

            var books = await repo.GetBooksByPublishingYear(year);

            return books;
        }

        public async Task<int> AddBook(Book book)
        {
            await repo.Save(book);
            var assignedId = book.Id;
            return assignedId;
        }

        public async Task<int> EditBook(Book book)
        {
            await repo.Update(book);
            var editedBookId = book.Id;
            return editedBookId;
        }

        public async Task<bool> RemoveBook(int id)
        {
            var book = await repo.GetBook(id);
            var successful = await repo.Delete(book);
            return successful == 1;
        }
    }
}
