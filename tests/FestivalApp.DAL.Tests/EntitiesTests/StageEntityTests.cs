using System.Linq;
using FestivalApp.DAL.Seeds;
using Xunit;

namespace FestivalApp.DAL.Tests
{
    public class StageEntityTests : BaseEntityTests
    {
        [Fact]
        public void AddStage_Persisted()
        {
            //Arrange
            var defaultStage = TestUtilities.CreateTestStage(1);

            //Act
            _dbContextSut.Stages.Add(defaultStage);
            _dbContextSut.SaveChanges();

            //Assert
            using var dbxA = _dbContextFactory.CreateDbContext();
            var retrievedStage = dbxA.Stages
                    .Single(entity => entity.Id == defaultStage.Id);
            Assert.Equal(defaultStage, retrievedStage);
        }

        [Fact]
        public void UpdateStage_Persisted()
        {
            //Arrange
            var defaultStage = _dbContextSut.Stages.SingleOrDefault(i => i.Id == StageSeeds.Stage1.Id);

            defaultStage.Description = "Changed";
            defaultStage.Capacity = 10;
            _dbContextSut.Stages.Update(defaultStage);
            _dbContextSut.SaveChanges();

            //Assert
            using var dbxA = _dbContextFactory.CreateDbContext();
            var retrievedStage = dbxA.Stages
                .FirstOrDefault(i => i.Id == defaultStage.Id);
            Assert.Equal("Changed", retrievedStage.Description);
            Assert.Equal(10, retrievedStage.Capacity);
        }

        [Fact]
        public void RemoveStage_Persisted()
        {
            //Arrange
            var defaultStage = _dbContextSut.Stages.SingleOrDefault(i => i.Id == StageSeeds.Stage1.Id);

            // Act
            _dbContextSut.Stages.Remove(defaultStage);
            _dbContextSut.SaveChanges();

            //Assert
            using var dbxA = _dbContextFactory.CreateDbContext();
            var retrievedStage = dbxA.Stages
                .FirstOrDefault(i => i.Id == defaultStage.Id);
            Assert.Null(retrievedStage);
        }
    }
}
