using System;
using System.Collections.Generic;
using FestivalApp.BL.Facades;
using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;
using FestivalApp.DAL.Tests;
using Xunit;

namespace FestivalApp.BL.Tests.FacadesTests
{
    public class CompleteFacadeTests : BaseEntityTests
    {
        private readonly BandMapper bandMapper;
        private readonly StageMapper stageMapper;
        private readonly PerformanceMapper perfMapper;

        protected readonly RepositoryBase<BandEntity> bandRepository;
        protected readonly RepositoryBase<StageEntity> stageRepository;
        protected readonly RepositoryBase<PerformanceEntity> perfRepository;

        protected readonly EntityFactory entityFactory;

        private readonly PerformanceFacade _facadePerformance;
        private readonly StageFacade _facadeStage;
        private readonly BandFacade _facadeBand;

        public CompleteFacadeTests()
        {
            bandMapper = new BandMapper();
            stageMapper = new StageMapper();
            perfMapper = new PerformanceMapper();

            bandRepository = new RepositoryBase<BandEntity>(_dbContextSut);
            stageRepository = new RepositoryBase<StageEntity>(_dbContextSut);
            perfRepository = new RepositoryBase<PerformanceEntity>(_dbContextSut);

            entityFactory = new EntityFactory(_dbContextSut);

            _facadeBand = new BandFacade(bandMapper, bandRepository, entityFactory);
            _facadeStage = new StageFacade(stageMapper, stageRepository, entityFactory);
            _facadePerformance = new PerformanceFacade(perfMapper, perfRepository, entityFactory);
        }

        [Fact]
        public void Test_InsertOrUpdate_Persisted()
        {
            // Arrange BANDS
            BandEntity band1 = TestUtilities.CreateTestBand(1);
            BandEntity band2 = TestUtilities.CreateTestBand(2);

            band1 = bandMapper.Map(_facadeBand.Save(bandMapper.Map(band1)), entityFactory);
            band2 = bandMapper.Map(_facadeBand.Save(bandMapper.Map(band2)), entityFactory);

            // Arrange STAGES
            StageEntity stage1 = TestUtilities.CreateTestStage(1);
            StageEntity stage2 = TestUtilities.CreateTestStage(2);

            stage1 = stageMapper.Map(_facadeStage.Save(stageMapper.Map(stage1)), entityFactory);
            stage2 = stageMapper.Map(_facadeStage.Save(stageMapper.Map(stage2)), entityFactory);

            // Arrange PERFOMANCES
            var performance1 = TestUtilities.CreateTestPerformanceWithArgs(1, args: new Dictionary<string, object>()
            {
                {"Band", band1},
                {"Stage", stage1},
                {"From", new DateTime(2021, 3, 1, 21, 30, 0)},
                {"To", new DateTime(2021, 3, 1, 23, 0, 0)}
            });
            var detailPerf1 = _facadePerformance.Save(perfMapper.Map(performance1));

            var performance2 = TestUtilities.CreateTestPerformanceWithArgs(2, args: new Dictionary<string, object>()
            {
                {"Band", band1},
                {"Stage", stage2},
                {"From", new DateTime(2021, 2, 1, 21, 30, 0)},
                {"To", new DateTime(2021, 2, 1, 23, 0, 0)}
            });
            var detailPerf2 = _facadePerformance.Save(perfMapper.Map(performance2));

            var performance3 = TestUtilities.CreateTestPerformanceWithArgs(3, args: new Dictionary<string, object>()
            {
                {"Band", band2},
                {"Stage", stage2},
                {"From", new DateTime(2021, 1, 1, 21, 30, 0)},
                {"To", new DateTime(2021, 1, 1, 23, 0, 0)}
            });
            var detailPerf3 = _facadePerformance.Save(perfMapper.Map(performance3));

            // Act
            var bandFromDb1 = _facadeBand.GetById(detailPerf1.BandDetailedModel.Id);
            var bandFromDb2 = _facadeBand.GetById(detailPerf3.BandDetailedModel.Id);

            var stageFromDb1 = _facadeStage.GetById(detailPerf1.StageDetailedModel.Id);
            var stageFromDb2 = _facadeStage.GetById(detailPerf3.StageDetailedModel.Id);

            Assert.Equal(2, bandFromDb1.Performances.Count);
            Assert.Equal(1, bandFromDb2.Performances.Count);
            //Assert.Equal(1, stageFromDb1.Bands.Count);
            //Assert.Equal(2, stageFromDb2.Bands.Count);
        }
    }
}
