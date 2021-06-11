using System;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Tests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FestivalApp.BL.Tests.MappersTests
{
    public class PerformanceMapperTests : BaseRepositoryTests<PerformanceEntity>
    {
        private readonly BandMapper _bandMapper;
        private readonly StageMapper _stageMapper;
        private readonly PerformanceMapper _perfMapper;

        public PerformanceMapperTests()
        {
            _perfMapper = new PerformanceMapper();
            _bandMapper = new BandMapper();
            _stageMapper = new StageMapper();
        }

        [Fact]
        public void Test_MapPerfEntity_ToDetailedModel()
        {
            // Arrange
            var perfEntity = TestUtilities.CreateTestPerformance(1);
            repository.InsertOrUpdate(perfEntity);
            var expectedDetailedModel = new PerformanceDetailModel()
            {
                Id = perfEntity.Id,
                From = perfEntity.From,
                To = perfEntity.To,
                BandDetailedModel = _bandMapper.Map(perfEntity.Band),
                StageDetailedModel = _stageMapper.Map(perfEntity.Stage)
            };

            // Action
            var perfDetailedModel = _perfMapper.Map(perfEntity);

            // Assert
            Assert.Equal(expectedDetailedModel, perfDetailedModel);

        }

        [Fact]
        public void Test_MapPerformanceDetailedModel_ToPerformanceEntity()
        {
            var expectedPerformanceEntity = TestUtilities.CreateTestPerformance(1);
            repository.InsertOrUpdate(expectedPerformanceEntity);

            var perfDetailedModel = new PerformanceDetailModel()
            {
                Id = expectedPerformanceEntity.Id,
                From = expectedPerformanceEntity.From,
                To = expectedPerformanceEntity.To,
                BandDetailedModel = _bandMapper.Map(expectedPerformanceEntity.Band),
                StageDetailedModel = _stageMapper.Map(expectedPerformanceEntity.Stage)
            };

            // Action
            var performanceEntity = _perfMapper.Map(perfDetailedModel, null);

            // Assert
            Assert.Equal(expectedPerformanceEntity, performanceEntity);
        }

        [Fact]
        public void Test_MapPerfEntities_ToListModels()
        {
            var perfs = _dbContextSut.Performances
                .Select(i => i)
                .Include(i => i.Stage)
                .Include(i => i.Band);
            var expectedListModels = perfs.Select(perf => _perfMapper.MapToListModel(perf)).ToList();

            var actualList = _perfMapper.Map(repository.GetAll()).ToValueCollection();

            Assert.True(expectedListModels.SequenceEqual(actualList));
        }
    }
}
