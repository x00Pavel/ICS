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
    public class StageFacade : CrudFacadeBase<StageEntity, StageListModel, StageDetailModel>
    {
        public StageFacade(
            IMapper<StageEntity, StageListModel, StageDetailModel> mapper,
            RepositoryBase<StageEntity> repository,
            IEntityFactory entityFactory)
            : base(mapper, repository, entityFactory)
        {
        }

        protected override Func<IQueryable<StageEntity>, IIncludableQueryable<StageEntity, object>>[] Includes
        {
            get;
        } = new Func<IQueryable<StageEntity>, IIncludableQueryable<StageEntity, object>>[]
        {
            entities => entities.Include(i=>i.Performances).ThenInclude(i => i.Band)
        };

    }
}
