using System;
using System.Linq;
using FestivalApp.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FestivalApp.DAL.Tests
{
    public class PerformanceEntityTests : BaseEntityTests
    {
        [Fact]
        public void AddPerformance_Persisted()
        {
            //Arrange
            var defaultPerformance = TestUtilities.CreateTestPerformance(1);
            defaultPerformance.BandId = BandSeeds.Band1.Id;
            defaultPerformance.Band = _dbContextSut.Bands.SingleOrDefault(i => i.Id == BandSeeds.Band1.Id);
            defaultPerformance.StageId = StageSeeds.Stage1.Id;
            defaultPerformance.Stage = _dbContextSut.Stages.SingleOrDefault(i => i.Id == StageSeeds.Stage1.Id);

            //Act
            _dbContextSut.Performances.Add(defaultPerformance);
            _dbContextSut.SaveChanges();

            //Assert
            using var dbxA = _dbContextFactory.CreateDbContext();
            var retrievedPerformance = dbxA.Performances
                .Include(i => i.Stage)
                .Include(i => i.Band)
                .FirstOrDefault(entity => entity.Id == defaultPerformance.Id);
            Assert.Equal(defaultPerformance, retrievedPerformance);
        }

        [Fact]
        public void UpdatePerformance_Persisted()
        {
            //Arrange
            var defaultPerformance = _dbContextSut.Performances
                .Include(i => i.Stage)
                .Include(i => i.Band)
                .SingleOrDefault(i => i.Id == PerformanceSeeds.Perf1.Id);

            //Act
            defaultPerformance.Band = _dbContextSut.Bands.SingleOrDefault(i => i.Id == BandSeeds.Band2.Id);
            defaultPerformance.BandId = BandSeeds.Band2.Id;
            defaultPerformance.From = new DateTime(2021, 2, 1, 7, 0, 0);
            _dbContextSut.Performances.Update(defaultPerformance);
            _dbContextSut.SaveChanges();

            //Assert
            using var dbxA = _dbContextFactory.CreateDbContext();
            var updatedPerformance = dbxA.Performances
                .Include(i => i.Stage)
                .Include(i => i.Band)
                .FirstOrDefault(entity => entity.Id == defaultPerformance.Id);
            Assert.Equal(defaultPerformance, updatedPerformance);
        }

        [Fact]
        public void DeletePerformance_Persisted()
        {
            //Arrange
            var defaultPerformance = _dbContextSut.Performances
                .Include(i => i.Stage)
                .Include(i => i.Band)
                .SingleOrDefault(i => i.Id == PerformanceSeeds.Perf1.Id);

            //Act
            _dbContextSut.Performances.Remove(defaultPerformance);
            _dbContextSut.SaveChanges();

            //Assert
            using var dbxA = _dbContextFactory.CreateDbContext();
            var retrievedPerformance = dbxA.Performances
                .FirstOrDefault(entity => entity.Id == defaultPerformance.Id);
            Assert.Null(retrievedPerformance);
            var s = dbxA.Stages.SingleOrDefault(i => i.Id == defaultPerformance.StageId);
        }


        [Theory(Skip = "This test need to be trasnfered to BL")]
        [InlineData("2021/02/01 18:00:00", "2021/02/01 19:30:00")]
        [InlineData("2021/02/01 22:00:00", "2021/02/01 21:30:00")]
        [InlineData("2021/02/01 21:00:00", "2021/02/01 04:30:00")]
        public void TestPerformance_Time_Setters(string from, string to)
        {
            // All default performances have time slots "2021/02/01 21:00:00 - 21:30:00"
            // Arrange
            var defaultPerformance = TestUtilities.CreateTestPerformance(1);
            var newFrom = DateTime.Parse(from);
            var newTo = DateTime.Parse(to);

            if (newFrom > defaultPerformance.To)
            {
                // Act
                defaultPerformance.From = newFrom;

                // Assert
                Assert.Equal(defaultPerformance.From, newFrom);
                Assert.Equal(defaultPerformance.To, new DateTime());
            }
            else if (newTo < defaultPerformance.From)
            {
                // Act
                defaultPerformance.To = newTo;

                // Assert
                Assert.Equal(defaultPerformance.From, new DateTime());
                Assert.Equal(defaultPerformance.To, newTo);
            }
            else
            {
                // Act
                defaultPerformance.From = newFrom;
                defaultPerformance.To = newTo;

                // Assert
                Assert.Equal(defaultPerformance.From, newFrom);
                Assert.Equal(defaultPerformance.To, newTo);
            }
        }
    }
}
