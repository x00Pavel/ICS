using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;

namespace FestivalApp.BL.Mappers
{
    public interface IMapper<TEntity, out TListModel, TDetailedModel>
        where TEntity : class, IEntity, new()
        where TListModel : IModel, new()
        where TDetailedModel : IModel, new()
    {
        IEnumerable<TListModel> Map(IQueryable<TEntity> entities);
        TDetailedModel Map(TEntity entity);
        TEntity Map(TDetailedModel detailedModel, IEntityFactory entityFactory);
        TListModel MapToListModel(TEntity entity);
    }
}
