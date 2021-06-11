using System;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;
using FestivalApp.DAL.Tests;

namespace FestivalApp.BL.Tests
{
    public class BaseRepositoryTests<TEntity> : BaseEntityTests, IDisposable
        where TEntity : class, IEntity, new()
    {
        protected readonly RepositoryBase<TEntity> repository;
        protected readonly EntityFactory entityFactory;

        protected BaseRepositoryTests()
        {
            repository = new RepositoryBase<TEntity>(_dbContextSut);
            entityFactory = new EntityFactory(_dbContextSut);
        }
    }
}
