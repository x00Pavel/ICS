using FestivalApp.DAL;
using Microsoft.EntityFrameworkCore;

namespace FestivalApp.App.Factories
{
    public class SqlServerDbContextFactory : IDbContextFactory<FestivalDbContext>
    {
        private string _connectionString;

        public SqlServerDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FestivalDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new FestivalDbContext(optionsBuilder.Options);
        }
    }
}
