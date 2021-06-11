using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Seeds;
using FestivalApp.DAL.Tests;
using Xunit;

namespace FestivalApp.BL.Tests.RepositoriesTests
{
    public class PerformanceRepositoryTests : BaseRepositoryTests<PerformanceEntity>
    {
        [Fact]
        public void NewPerformances_Create_Persisted()
        {
            // Arrange
            var performances = TestUtilities.CreateTestPerformanceWithSlots(1, 3);

            // Act
            foreach (PerformanceEntity performance in performances)
            {
                _ = repository.InsertOrUpdate(performance);
                // _ = Repository.InsertOrUpdate(performance);
            }

            // Assert
            Assert.Equal(performances.ElementAt(2), repository.GetById(performances.ElementAt(2).Id));
        }

        [Theory]
        [InlineData("2021/02/01 18:00:00", "2021/02/01 19:30:00", true)]
        [InlineData("2021/02/01 22:00:00", "2021/02/01 22:30:00", false)]
        [InlineData("2021/02/01 19:30:00", "2021/02/01 22:30:00", false)]
        public void NewPerformance_Create_CollisionDifferentStagesSameBand(string from, string to, bool isOk)
        {
            // Arrange 
            StageEntity stageOne = TestUtilities.CreateTestStage(1);
            StageEntity stageTwo = TestUtilities.CreateTestStage(2);
            BandEntity band = TestUtilities.CreateTestBand(1);

            var performanceOne = TestUtilities.CreateTestPerformanceWithArgs(args: new Dictionary<string, object>()
            {
                {"Band", band},
                {"Stage", stageOne},
                {"From", new DateTime(2021, 2, 1, 21, 30, 0)},
                {"To", new DateTime(2021, 2, 1, 23, 0, 0)}
            });
            var performanceTwo = TestUtilities.CreateTestPerformanceWithArgs(args: new Dictionary<string, object>()
            {
                {"Band", band},
                {"Stage", stageTwo},
                {"From", DateTime.Parse(from)},
                {"To", DateTime.Parse(to)}
            });

            // Act
            _ = repository.InsertOrUpdate(performanceOne);
            _ = repository.InsertOrUpdate(performanceTwo);

            // Assert
            Assert.Equal(performanceOne, repository.GetById(performanceOne.Id));
            if (!isOk)
            {
                Assert.Null(repository.GetById(performanceTwo.Id));
            }
            else
            {
                Assert.Equal(performanceTwo, repository.GetById(performanceTwo.Id));
            }
        }

        [Theory]
        [InlineData("2021/02/01 18:00:00", "2021/02/01 19:30:00", true)]
        [InlineData("2021/02/01 22:00:00", "2021/02/01 22:30:00", false)]
        [InlineData("2021/02/01 19:30:00", "2021/02/01 22:30:00", false)]
        public void NewPerformance_Create_CollisionSameStageDifferentBands(string from, string to, bool isOk)
        {
            // Arrange 
            BandEntity bandOne = TestUtilities.CreateTestBand(1);
            BandEntity bandTwo = TestUtilities.CreateTestBand(2);
            StageEntity stage = TestUtilities.CreateTestStage(1);

            var performanceOne = TestUtilities.CreateTestPerformanceWithArgs(args: new Dictionary<string, object>()
            {
                {"Band", bandOne},
                {"Stage", stage},
                {"From", new DateTime(2021, 2, 1, 21, 30, 0)},
                {"To", new DateTime(2021, 2, 1, 23, 0, 0)}
            });
            var performanceTwo = TestUtilities.CreateTestPerformanceWithArgs(args: new Dictionary<string, object>()
            {
                {"Band", bandTwo},
                {"Stage", stage},
                {"From", DateTime.Parse(from)},
                {"To", DateTime.Parse(to)}
            });

            // Act
            _ = repository.InsertOrUpdate(performanceOne);
            _ = repository.InsertOrUpdate(performanceTwo);

            // Assert
            Assert.Equal(performanceOne, repository.GetById(performanceOne.Id));
            if (!isOk)
            {
                Assert.Null(repository.GetById(performanceTwo.Id));
            }
            else
            {
                Assert.Equal(performanceTwo, repository.GetById(performanceTwo.Id));
            }
        }

        [Fact]
        public void Performance_GetById()
        {
            // Arrange
            var performance = PerformanceSeeds.Perf1;

            // Assert & Act
            Assert.Equal(performance, repository.GetById(performance.Id));
        }

        [Fact]
        public void Performance_GetAll()
        {
            // Arrange
            var perfs = PerformanceSeeds.Dict.Values;

            var received = repository.GetAll();
            // Assert
            Assert.True(perfs.SequenceEqual(received));
        }

        [Fact]
        public void Performance_Update_Persisted()
        {
            // Arrange
            var performance = TestUtilities.CreateTestPerformanceWithArgs(1);

            // Act
            // Act - Insert
            _ = repository.InsertOrUpdate(performance);
            var performanceFromRepository = repository.GetById(performance.Id);
            // Act - Update 
            performanceFromRepository.Stage = TestUtilities.CreateTestStage(2);
            performanceFromRepository.Band = TestUtilities.CreateTestBand(2);
            _ = repository.InsertOrUpdate(performanceFromRepository);

            // Assert 
            Assert.Equal(repository.GetById(performance.Id), repository.GetById(performanceFromRepository.Id));
        }

        [Fact]
        public void DeletePerformance()
        {
            // Arrange
            var performance = PerformanceSeeds.Perf1;

            // Act
            repository.Delete(performance);

            performance = _dbContextSut.Performances.SingleOrDefault(i => i.Id == performance.Id);
            Assert.Null(performance);
        }

        [Fact]
        public void DeletePerformance_ById()
        {
            // Arrange
            var performance = PerformanceSeeds.Perf1;

            // Act
            repository.DeleteById(performance.Id);

            performance = _dbContextSut.Performances.SingleOrDefault(i => i.Id == performance.Id);
            // Assert
            Assert.Null(performance);
        }
    }
}
