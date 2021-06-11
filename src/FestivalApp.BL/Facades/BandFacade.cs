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
    public class BandFacade : CrudFacadeBase<BandEntity, BandListModel, BandDetailModel>
    {

        public BandFacade(
            IMapper<BandEntity, BandListModel, BandDetailModel> mapper,
            RepositoryBase<BandEntity> repository,
            IEntityFactory entityFactory)
            : base(mapper, repository, entityFactory)
        {
        }
        protected override Func<IQueryable<BandEntity>, IIncludableQueryable<BandEntity, object>>[] Includes
        {
            get;
        } = new Func<IQueryable<BandEntity>, IIncludableQueryable<BandEntity, object>>[]
        {
            entities => entities.Include(i=>i.Performances).ThenInclude(i => i.Stage)
        };

    }
}
