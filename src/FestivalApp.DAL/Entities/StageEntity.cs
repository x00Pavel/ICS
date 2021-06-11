using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.Essentials.Design;

namespace FestivalApp.DAL.Entities
{
    public record StageEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Photo { get; set; }

#if DEBUG
        public ICollection<PerformanceEntity> Performances { get; set; }
            = new ReferenceLoopProneValueCollection<PerformanceEntity>(PerformanceEntity.PerformanceWithoutReferenceFieldsComparer);
#else
        public ICollection<PerformanceEntity> Performances { get; set; }
            = new ValueCollection<PerformanceEntity>(PerformanceEntity.PerformanceWithoutReferenceFieldsComparer);

#endif

        public IEnumerable<BandEntity> GetListOfBands()
        {
            return Performances.Select(i => i.Band).Distinct();
        }

        public virtual bool Equals(StageEntity other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return base.Equals(other)
                   && Name == other.Name
                   && Description == other.Description
                   && Capacity == other.Capacity
                   && Photo == other.Photo
                   && Performances.SequenceEqual(Performances);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(),
                                    Name,
                                    Description,
                                    Capacity,
                                    Photo,
                                    Performances);
        }
    }
}
