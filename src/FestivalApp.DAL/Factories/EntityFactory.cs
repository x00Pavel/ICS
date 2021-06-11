using System;
using System.Linq;
using FestivalApp.DAL.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FestivalApp.DAL.Factories
{
    public class EntityFactory : IEntityFactory
    {
        private readonly ChangeTracker _changeTracker;

        public EntityFactory(FestivalDbContext dbContext) => _changeTracker = dbContext.ChangeTracker;

        public TEntity Create<TEntity>(Guid id) where TEntity : class, IEntity, new()
        {
            TEntity entity = null;
            if (id != Guid.Empty)
            {
                entity = _changeTracker?.Entries<TEntity>().SingleOrDefault(i => i.Entity.Id == id)?.Entity;

                if (entity != null)
                {
                    return entity;
                }

                entity = new TEntity { Id = id };
                _changeTracker?.Context.Attach(entity);
            }
            else
            {
                entity = new TEntity();
            }
            return entity;
        }
    }
}
