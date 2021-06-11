using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.Essentials.Design;

namespace FestivalApp.DAL.Entities
{
    public record BandEntity : EntityBase
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Photo { get; set; }
        public string CountryOfOrigin { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }

#if DEBUG
        public ICollection<PerformanceEntity> Performances { get; set; }
            = new ReferenceLoopProneValueCollection<PerformanceEntity>(PerformanceEntity.PerformanceWithoutReferenceFieldsComparer);
#else
        public ICollection<PerformanceEntity> Performances { get; set; }
            = new ValueCollection<PerformanceEntity>(PerformanceEntity.PerformanceWithoutReferenceFieldsComparer);

#endif

        public virtual bool Equals(BandEntity other)
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
                   && Genre == other.Genre
                   && Photo == other.Photo
                   && CountryOfOrigin == other.CountryOfOrigin
                   && LongDescription == other.LongDescription
                   && ShortDescription == other.ShortDescription
                   && Performances.SequenceEqual(other.Performances);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(),
                Name,
                Genre,
                Photo,
                CountryOfOrigin,
                LongDescription,
                ShortDescription,
                Performances);
        }

        private sealed class BandEntityWithoutPerformances : IEqualityComparer<BandEntity>
        {
            public bool Equals(BandEntity x, BandEntity y) => !ReferenceEquals(x, null) && x.Equals(y);

            public int GetHashCode(BandEntity obj) => obj.GetHashCode();
        }

        public static IEqualityComparer<BandEntity> BandWithoutPathFieldsComparer { get; } =
            new BandEntityWithoutPerformances();



    }
}
