using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Factories;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;

namespace FestivalApp.BL.Mappers
{
    public class PerformanceMapper : IMapper<PerformanceEntity, PerformanceListModel, PerformanceDetailModel>
    {
        public IEnumerable<PerformanceListModel> Map(IQueryable<PerformanceEntity> entities) =>
            entities?.Select(perf => new PerformanceListModel()
            {
                Id = perf.Id,
                From = perf.From,
                To = perf.To,
                BandName = perf.Band.Name,
                StageName = perf.Stage.Name
            });

        public PerformanceDetailModel Map(PerformanceEntity entity)
            => entity == null ? null : new PerformanceDetailModel()
            {
                Id = entity.Id,
                BandDetailedModel = new BandMapper().Map(entity.Band),
                StageDetailedModel = new StageMapper().Map(entity.Stage),
                From = entity.From,
                To = entity.To
            };

        public PerformanceEntity Map(PerformanceDetailModel detailedModel, IEntityFactory factory)
        {
            var entity = (factory ?? new CreateNewEntityFactory()).Create<PerformanceEntity>(detailedModel.Id);

            entity.Id = detailedModel.Id;

            entity.From = detailedModel.From;
            entity.To = detailedModel.To;

            entity.BandId = detailedModel.BandDetailedModel.Id;
            entity.Band = new BandMapper().Map(detailedModel.BandDetailedModel, factory);

            entity.StageId = detailedModel.StageDetailedModel.Id;
            entity.Stage = new StageMapper().Map(detailedModel.StageDetailedModel, factory);

            return entity;
        }

        public PerformanceListModel MapToListModel(PerformanceEntity entity) =>
            new PerformanceListModel()
            {
                Id = entity.Id,
                From = entity.From,
                To = entity.To,
                BandName = entity.Band.Name,
                StageName = entity.Stage.Name
            };
    }
}
