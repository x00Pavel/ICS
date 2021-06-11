using System;
using FestivalApp.DAL.Entities;

namespace FestivalApp.DAL.Factories
{
    public interface IEntityFactory
    {
        TEntity Create<TEntity>(Guid id) where TEntity : class, IEntity, new();
    }
}
