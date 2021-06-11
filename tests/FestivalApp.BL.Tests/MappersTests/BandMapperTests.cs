using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Seeds;
using FestivalApp.DAL.Tests;
using Xunit;

namespace FestivalApp.BL.Tests.MappersTests
{
    public class BandMapperTests : BaseRepositoryTests<BandEntity>
    {
        private readonly BandMapper _bandMapper;
        private readonly PerformanceMapper _performanceMapper;

        public BandMapperTests()
        {
            _bandMapper = new BandMapper();
            _performanceMapper = new PerformanceMapper();
        }

        [Fact]
        public void Test_MapBandEntity_ToDetailedModel()
        {
            BandEntity bandEntity = TestUtilities.CreateTestBand(1);
            repository.InsertOrUpdate(bandEntity);

            BandDetailModel expectedModel = new BandDetailModel()
            {
                Id = bandEntity.Id,
                Name = bandEntity.Name,
                Genre = bandEntity.Genre,
                Photo = bandEntity.Photo,
                CountryOfOrigin = bandEntity.CountryOfOrigin,
                LongDescription = bandEntity.LongDescription,
                ShortDescription = bandEntity.ShortDescription,
                Performances = _performanceMapper.Map(bandEntity.Performances.AsQueryable()).ToValueCollection()
            };

            BandDetailModel retrievedModel = _bandMapper.Map(bandEntity);

            Assert.Equal(expectedModel, retrievedModel);
        }


        [Fact]
        public void Test_MapBandDetailedModel_ToBandEntity()
        {
            BandEntity bandEntity = TestUtilities.CreateTestBand(1);
            repository.InsertOrUpdate(bandEntity);

            BandDetailModel bandDetailedModel = new BandDetailModel()
            {
                Id = bandEntity.Id,
                Name = bandEntity.Name,
                Genre = bandEntity.Genre,
                Photo = bandEntity.Photo,
                CountryOfOrigin = bandEntity.CountryOfOrigin,
                LongDescription = bandEntity.LongDescription,
                ShortDescription = bandEntity.ShortDescription,
                Performances = _performanceMapper.Map(bandEntity.Performances.AsQueryable()).ToValueCollection()
            };

            BandEntity retrievedBandEntity = _bandMapper.Map(bandDetailedModel, null);

            Assert.Equal(bandEntity, retrievedBandEntity);
        }


        [Fact]
        public void Test_MapBandEntity_ToListModel()
        {
            var bandEntity = TestUtilities.CreateTestBand(1);
            repository.InsertOrUpdate(bandEntity);

            var expectedListModel = new BandListModel()
            {
                Id = bandEntity.Id,
                Name = bandEntity.Name,
                Photo = bandEntity.Photo,
                ShortDescription = bandEntity.ShortDescription
            };

            var bandListModel = _bandMapper.MapToListModel(bandEntity);

            Assert.Equal(expectedListModel, bandListModel);
        }


        [Fact]
        public void Test_MapBandEntities_ToListModels()
        {
            var expectedBandsModels = BandSeeds.Dict.Values.Select(band => _bandMapper.MapToListModel(band)).ToList();

            var retrievedBandsModels = _bandMapper.Map(repository.GetAll()).ToList();

            Assert.True(expectedBandsModels.SequenceEqual(retrievedBandsModels));

        }
    }
}
