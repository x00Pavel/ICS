using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Factories;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;

namespace FestivalApp.BL.Mappers
{
    public class StageMapper : IMapper<StageEntity, StageListModel, StageDetailModel>
    {
        public IEnumerable<StageListModel> Map(IQueryable<StageEntity> entities)
            => entities?.Select(e => _mapToListModel(e)).ToValueCollection();


        public StageDetailModel Map(StageEntity entity)
            => entity == null ? null : new StageDetailModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Capacity = entity.Capacity,
                Photo = entity.Photo,
                Performances = new PerformanceMapper().Map(
                    entity.Performances.AsQueryable()).ToValueCollection()
            };

        public StageEntity Map(StageDetailModel detailedModel, IEntityFactory factory)
        {
            var entity = (factory ??= new CreateNewEntityFactory()).Create<StageEntity>(detailedModel.Id);

            entity.Id = detailedModel.Id;
            entity.Capacity = detailedModel.Capacity;
            entity.Description = detailedModel.Description;
            entity.Name = detailedModel.Name;
            entity.Photo = detailedModel.Photo;

            return entity;
        }

        public StageListModel MapToListModel(StageEntity entity) => _mapToListModel(entity);

        private static StageListModel _mapToListModel(StageEntity entity)
            => new()
            {
                Photo = entity.Photo,
                Id = entity.Id,
                Name = entity.Name,
                Capacity = entity.Capacity
            };
    }
}
