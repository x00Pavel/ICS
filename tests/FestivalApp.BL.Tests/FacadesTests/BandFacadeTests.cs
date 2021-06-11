using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Facades;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Seeds;
using FestivalApp.DAL.Tests;
using Xunit;

namespace FestivalApp.BL.Tests.FacadesTests
{
    public class BandFacadeTests : BaseRepositoryTests<BandEntity>
    {
        private readonly BandMapper bandMapper;
        private readonly BandFacade _facadeSUT;

        public BandFacadeTests()
        {
            bandMapper = new BandMapper();
            _facadeSUT = new BandFacade(bandMapper, repository, entityFactory);
        }

        [Fact]
        public void NewBand_InsertOrUpdate_Persisted()
        {
            // Arrange
            var detail = bandMapper.Map(TestUtilities.CreateTestBand(1));

            // Act
            detail = _facadeSUT.Save(detail);

            // Assert
            Assert.NotEqual(Guid.Empty, detail.Id);
            Assert.Equal(detail, _facadeSUT.GetById(detail.Id));
        }

        [Fact]
        public void NewBand_Delete_Persisted()
        {
            // Arrange
            var detail = bandMapper.Map(TestUtilities.CreateTestBand(1));

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
            var expectedListModels = new List<BandListModel>();
            foreach (var band in BandSeeds.Dict.Values)
            {
                expectedListModels.Add(bandMapper.MapToListModel(band));
            }

            // Act
            var listModels = _facadeSUT.GetAll().ToList();

            // Assert
            Assert.True(expectedListModels.SequenceEqual(listModels));
        }
    }
}
