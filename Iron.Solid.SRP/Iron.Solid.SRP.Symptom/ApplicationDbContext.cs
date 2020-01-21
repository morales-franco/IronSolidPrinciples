using Microsoft.EntityFrameworkCore;

namespace Iron.Solid.SRP.Symptom
{
    class ApplicationDbContext: DbContext
    {
        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseInMemoryDatabase(databaseName: "CountryDb");
            }
        }
    }
}
