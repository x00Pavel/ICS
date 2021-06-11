using System.Linq;
using FestivalApp.DAL.Seeds;
using Xunit;

namespace FestivalApp.DAL.Tests
{
    public class BandEntityTests : BaseEntityTests
    {
        [Fact]
        public void AddBand_Persisted()
        {
            //Arrange
            var defaultBand = TestUtilities.CreateTestBand(1);

            //Act
            _dbContextSut.Bands.Add(defaultBand);
            _dbContextSut.SaveChanges();

            //Assert
            using var dbxA = _dbContextFactory.CreateDbContext();
            var retrievedBand = dbxA.Bands
                .Single(entity => entity.Id == defaultBand.Id);
            Assert.Equal(defaultBand, retrievedBand);
        }

        [Fact]
        public void UpdateBand_Persisted()
        {
            //Arrange
            var defaultBand = _dbContextSut.Bands.SingleOrDefault(i => i.Id == BandSeeds.Band1.Id);

            defaultBand.Genre = "Rock";
            _dbContextSut.Bands.Update(defaultBand);
            _dbContextSut.SaveChanges();

            //Assert
            using var dbxA = _dbContextFactory.CreateDbContext();
            var retrievedBand = dbxA.Bands
                .FirstOrDefault(i => i.Id == defaultBand.Id);
            Assert.Equal("Rock", retrievedBand.Genre);
        }

        [Fact]
        public void RemoveBand_Persisted()
        {
            //Arrange
            var defaultBand = _dbContextSut.Bands.SingleOrDefault(i => i.Id == BandSeeds.Band1.Id);

            //Act
            //    _dbContextSut.Bands.Add(defaultBand);
            //  _dbContextSut.SaveChanges();

            //  using var dbx = _dbContextFactory.Create();
            _dbContextSut.Bands.Remove(defaultBand);
            _dbContextSut.SaveChanges();

            //Assert
            using var dbxA = _dbContextFactory.CreateDbContext();
            var retrievedBand = dbxA.Bands.
                FirstOrDefault(i => i.Id == defaultBand.Id);
            Assert.Null(retrievedBand);
        }
    }
}
