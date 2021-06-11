using Microsoft.EntityFrameworkCore;

namespace FestivalApp.DAL.Factories
{
    public class DbContextInMemoryFactory : IDbContextFactory<FestivalDbContext>
    {
        private readonly string _dbName;
        public object Create;

        public DbContextInMemoryFactory(string dbName)
        {
            _dbName = dbName;
        }

        public FestivalDbContext CreateDbContext()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            contextOptionsBuilder.UseInMemoryDatabase(this._dbName);
            contextOptionsBuilder.EnableSensitiveDataLogging(true);

            return new FestivalDbContext(contextOptionsBuilder.Options);
        }
    }
}
