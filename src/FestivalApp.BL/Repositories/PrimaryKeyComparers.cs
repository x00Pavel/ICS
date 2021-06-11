using System;
using System.Collections.Generic;
using FestivalApp.DAL.Entities;

namespace FestivalApp.BL.Repositories
{
    public static class PrimaryKeyComparers
    {
        public static IEqualityComparer<IEntity> IdComparer { get; } = new IdEqualityComparer<Guid>();

        private sealed class IdEqualityComparer<TKey> : IEqualityComparer<IEntity>
        {
            public bool Equals(IEntity x, IEntity y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (ReferenceEquals(x, null))
                {
                    return false;
                }

                if (ReferenceEquals(y, null))
                {
                    return false;
                }

                return x.GetType() == y.GetType() && x.Id.Equals(y.Id);
            }

            public int GetHashCode(IEntity obj) => obj.Id.GetHashCode();
        }
    }
}
