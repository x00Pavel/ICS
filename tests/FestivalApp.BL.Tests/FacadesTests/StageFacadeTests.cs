using System;
using System.Linq;
using FestivalApp.BL.Facades;
using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Seeds;
using FestivalApp.DAL.Tests;
using Xunit;

namespace FestivalApp.BL.Tests.FacadesTests
{
    public class StageFacadeTests : BaseRepositoryTests<StageEntity>
    {
        private readonly StageMapper stageMapper;
        private readonly StageFacade _facadeSUT;

        public StageFacadeTests()
        {
            stageMapper = new StageMapper();
            _facadeSUT = new StageFacade(stageMapper, repository, entityFactory);
        }

        [Fact]
        public void Test_InsertOrUpdate_Persisted()
        {
            // Arrange
            var detail = stageMapper.Map(TestUtilities.CreateTestStage(1));

            // Act
            detail = _facadeSUT.Save(detail);

            // Assert
            Assert.NotEqual(Guid.Empty, detail.Id);
            Assert.Equal(detail, _facadeSUT.GetById(detail.Id));
        }

        [Fact]
        public void Test_Delete_Persisted()
        {
            // Arrange
            var detail = stageMapper.Map(TestUtilities.CreateTestStage(1));

            // Act
            detail = _facadeSUT.Save(detail);
            Assert.NotEqual(Guid.Empty, detail.Id);
            _facadeSUT.Delete(detail);

            // Assert
            Assert.Null(_facadeSUT.GetById(detail.Id));
        }

        [Fact]
        public void Test_GetAllList_Persisted()
        {
            // Arrange
            var expectedListModels = StageSeeds.Dict.Values.Select(stage => stageMapper.MapToListModel(stage)).ToList();

            // Act
            var listModels = _facadeSUT.GetAll().ToList();

            // Assert
            Assert.True(expectedListModels.SequenceEqual(listModels));
        }
    }
}
