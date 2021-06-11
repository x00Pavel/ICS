using System;
using System.Collections.Generic;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;
using Microsoft.EntityFrameworkCore.Query;

namespace FestivalApp.BL.Facades
{
    public abstract class CrudFacadeBase<TEntity, TListModel, TDetailModel>
        where TEntity : class, IEntity, new()
        where TListModel : IModel, new()
        where TDetailModel : IModel, new()
    {
        protected readonly IMapper<TEntity, TListModel, TDetailModel> _mapper;
        protected readonly RepositoryBase<TEntity> _repository;
        protected readonly IEntityFactory _entityFactory;

        protected CrudFacadeBase(
            IMapper<TEntity, TListModel, TDetailModel> mapper,
            RepositoryBase<TEntity> repository,
            IEntityFactory entityFactory)
        {
            _mapper = mapper;
            _repository = repository;
            _entityFactory = entityFactory;
        }

        protected virtual Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] Includes { get; } = Array.Empty<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>();

        public IEnumerable<TListModel> GetAll() => _mapper.Map(_repository.GetAll());

        public TDetailModel GetById(Guid id)
        {
            var query = _repository.GetAll();

            query = Includes.Aggregate(query, (current, include) => include(current));

            return _mapper
                .Map(query.SingleOrDefault(i => i.Id.Equals(id)));
        }

        public void Delete(IModel model) => _repository.DeleteById(model.Id);

        public TDetailModel Save(TDetailModel model)
        {
            var entity = _mapper.Map(model, _entityFactory);
            entity = _repository.InsertOrUpdate(entity);
            return GetById(entity.Id);
        }
    }
}
