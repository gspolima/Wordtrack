using Microsoft.EntityFrameworkCore;

namespace Wordtrack.Data
{
    public class WordtrackTestContext : WordtrackContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WordtrackTestData;Trusted_Connection=true")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
