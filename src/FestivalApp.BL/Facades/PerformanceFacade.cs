using System;
using System.Linq;
using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;
using FestivalApp.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FestivalApp.BL.Facades
{
    public class PerformanceFacade : CrudFacadeBase<PerformanceEntity, PerformanceListModel, PerformanceDetailModel>
    {
        public PerformanceFacade(
            IMapper<PerformanceEntity, PerformanceListModel, PerformanceDetailModel> mapper,
            RepositoryBase<PerformanceEntity> repository,
            IEntityFactory entityFactory)
            : base(mapper, repository, entityFactory)
        {
        }

        protected override Func<IQueryable<PerformanceEntity>, IIncludableQueryable<PerformanceEntity, object>>[] Includes
        {
            get;
        } = new Func<IQueryable<PerformanceEntity>, IIncludableQueryable<PerformanceEntity, object>>[]
        {
            entities => entities.Include(i=>i.Band),
            entities => entities.Include(i=>i.Stage)
        };
    }
}
