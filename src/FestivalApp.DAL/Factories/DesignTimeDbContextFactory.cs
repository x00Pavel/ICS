using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FestivalApp.DAL.Factories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FestivalDbContext>
    {
        public FestivalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            optionsBuilder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                  Initial Catalog = FestivalAppDB;
                  MultipleActiveResultSets = True;
                  Integrated Security = True; "
            );
            return new FestivalDbContext(optionsBuilder.Options);
        }
    }
}
