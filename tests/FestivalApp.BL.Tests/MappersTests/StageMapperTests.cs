using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Seeds;
using FestivalApp.DAL.Tests;
using Xunit;

namespace FestivalApp.BL.Tests.MappersTests
{
    public class StageMapperTests : BaseRepositoryTests<StageEntity>
    {
        private readonly StageMapper _stageMapper;
        private readonly BandMapper _bandMapper;
        public StageMapperTests()
        {
            _stageMapper = new StageMapper();
            _bandMapper = new BandMapper();
        }

        [Fact]
        public void Test_MapStageEntity_ToDetailedModel()
        {
            // Arrange
            var stageEntity = TestUtilities.CreateTestStage(1);
            var entity = repository.InsertOrUpdate(stageEntity);


            // Action
            var expectedModel = new StageDetailModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Photo = entity.Photo,
                Capacity = entity.Capacity,
                Description = entity.Description,
                //Bands = _bandMapper.Map(entity.GetListOfBands().AsQueryable()).ToValueCollection()
            };

            var actualModel = _stageMapper.Map(entity);

            // Asserts
            Assert.Equal(actualModel, expectedModel);

        }

        [Fact]
        public void Test_MapStageDetailedModel_ToEntity()
        {
            // Arrange
            var stageEntity = TestUtilities.CreateTestStage(1);
            repository.InsertOrUpdate(stageEntity);

            var stageDetailedModel = new StageDetailModel()
            {
                Id = stageEntity.Id,
                Name = stageEntity.Name,
                Capacity = stageEntity.Capacity,
                Description = stageEntity.Description,
                Photo = stageEntity.Photo,
            };

            // Action
            StageEntity retrieveStageEntity = _stageMapper.Map(stageDetailedModel, null);

            // Assert
            Assert.Equal(stageEntity, retrieveStageEntity);
        }


        [Fact]
        public void Test_MapStageEntity_ToListModel()
        {
            var stageEntity = TestUtilities.CreateTestStage(1);
            var expectedListModel = new StageListModel()
            {
                Id = stageEntity.Id,
                Name = stageEntity.Name,
                Capacity = stageEntity.Capacity,
                Photo = stageEntity.Photo
            };

            var stageListModel = _stageMapper.MapToListModel(stageEntity);

            Assert.Equal(expectedListModel, stageListModel);
        }

        [Fact]
        public void Test_MapStageEntities_ToListModels()
        {
            var expectedListModels = StageSeeds.Dict.Values.Select(stage => _stageMapper.MapToListModel(stage)).ToList();

            var stageListModels = _stageMapper.Map(repository.GetAll()).ToList();

            Assert.True(expectedListModels.SequenceEqual(stageListModels));
        }
    }
}
