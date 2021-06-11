using System;
using System.Linq;
using FestivalApp.BL.Facades;
using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Tests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FestivalApp.BL.Tests.FacadesTests
{
    public class PerformanceFacadeTests : BaseRepositoryTests<PerformanceEntity>
    {

        private readonly PerformanceMapper perfMapper;
        private readonly PerformanceFacade _facadeSUT;

        public PerformanceFacadeTests()
        {
            perfMapper = new PerformanceMapper();
            _facadeSUT = new PerformanceFacade(perfMapper, repository, entityFactory);
        }

        [Fact]
        public void Test_InsertOrUpdate_Persisted()
        {
            // Arrange
            var detail = perfMapper.Map(TestUtilities.CreateTestPerformance(1));

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
            var detail = perfMapper.Map(TestUtilities.CreateTestPerformance(1));

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
            var perfs = _dbContextSut.Performances
                .Select(i => i)
                .Include(i => i.Stage)
                .Include(i => i.Band);
            var expectedListModels = perfs.Select(perf => perfMapper.MapToListModel(perf)).ToList();


            // Act
            var listModels = _facadeSUT.GetAll().ToList();

            // Assert
            Assert.True(expectedListModels.SequenceEqual(listModels));
        }
    }
}
