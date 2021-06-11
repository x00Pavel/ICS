using System;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;

namespace FestivalApp.BL.Factories
{
    public class CreateNewEntityFactory : IEntityFactory
    {
        public TEntity Create<TEntity>(Guid id) where TEntity : class, IEntity, new() => new();
    }
}
