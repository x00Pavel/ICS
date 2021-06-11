using System;
using FestivalApp.DAL.Factories;

namespace FestivalApp.DAL.Tests
{
    public abstract class BaseEntityTests : IDisposable
    {
        protected readonly FestivalDbContext _dbContextSut;
        protected readonly DbContextInMemoryFactory _dbContextFactory;

        protected BaseEntityTests()
        {
            _dbContextFactory = new DbContextInMemoryFactory(Guid.NewGuid().ToString());
            _dbContextSut = _dbContextFactory.CreateDbContext();
            _dbContextSut.Database.EnsureCreated();
        }

        private void TearDownDatabase()
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Database.EnsureDeleted();
        }

        public void Dispose() => TearDownDatabase();
    }
}
