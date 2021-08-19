using Microsoft.EntityFrameworkCore;
using Wordtrack.Data;
using Wordtrack.Domain;
using Xunit;

namespace Wordtrack.Test.Database
{
    public class DatabaseTests
    {
        [Fact]
        public void CanSaveNewBookIntoDatabase()
        {
            var context =
                new WordtrackTestContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var book = new Book()
            {
                Title = "test title",
                Author = "tester",
                YearPublished = 2021,
                Pages = 40
            };
            context.Books.Add(book);
            context.SaveChanges();

            Assert.NotEqual(0, book.Id);
        }


        [Fact]
        public void CanEditExisitingBookFromDatabase()
        {
            var context = new WordtrackTestContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Books.Add(new Book()
            {
                Title = "test title",
                Author = "tester",
                YearPublished = 2021,
                Pages = 40
            });
            context.SaveChanges();

            var book = context.Books.Find(1);
            context.Entry(book).State = EntityState.Unchanged;
            book.Author = "Lord Tester";
            context.SaveChanges();

            Assert.NotEqual("tester", book.Author);
        }
    }
}
