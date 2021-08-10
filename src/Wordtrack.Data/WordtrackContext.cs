using Microsoft.EntityFrameworkCore;
using Wordtrack.Domain;

namespace Wordtrack.Data
{
    public class WordtrackContext : DbContext
    {
        public WordtrackContext()
        { }
        public WordtrackContext(DbContextOptionsBuilder options)
        { }

        public WordtrackContext(DbContextOptions<WordtrackContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WordtrackTestData;Trusted_Connection=true")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasKey(b => b.Id);

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(b => b.Author)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(b => b.YearPublished)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(b => b.Pages)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .HasData(
                    new Book()
                    {
                        Id = 1,
                        Title = "Apology, The Death of Socrates",
                        Author = "Plato",
                        YearPublished = -399,
                        Pages = 40
                    });
        }
    }

}
