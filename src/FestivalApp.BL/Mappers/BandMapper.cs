using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Factories;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;

namespace FestivalApp.BL.Mappers
{
    public class BandMapper : IMapper<BandEntity, BandListModel, BandDetailModel>
    {
        public IEnumerable<BandListModel> Map(IQueryable<BandEntity> entities)
            => entities?.Select(e => _mapToListModel(e)).ToValueCollection();

        public BandDetailModel Map(BandEntity entity)
            => entity == null ? null : new BandDetailModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Genre = entity.Genre,
                Photo = entity.Photo,
                CountryOfOrigin = entity.CountryOfOrigin,
                LongDescription = entity.LongDescription,
                ShortDescription = entity.ShortDescription,
                Performances = new PerformanceMapper().Map(
                    entity.Performances.AsQueryable()).ToValueCollection()
            };

        public BandEntity Map(BandDetailModel detailedModel, IEntityFactory entityFactory)
        {
            var entity = (entityFactory ??= new CreateNewEntityFactory()).Create<BandEntity>(detailedModel.Id);

            entity.Id = detailedModel.Id;
            entity.Name = detailedModel.Name;
            entity.Genre = detailedModel.Genre;
            entity.Photo = detailedModel.Photo;
            entity.CountryOfOrigin = detailedModel.CountryOfOrigin;
            entity.LongDescription = detailedModel.LongDescription;
            entity.ShortDescription = detailedModel.ShortDescription;

            return entity;
        }

        public BandListModel MapToListModel(BandEntity entity) => _mapToListModel(entity);

        private static BandListModel _mapToListModel(BandEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Photo = entity.Photo,
            ShortDescription = entity.ShortDescription
        };
    }
}
