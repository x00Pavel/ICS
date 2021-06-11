using System;
using System.Collections.Generic;

namespace FestivalApp.DAL.Entities
{
    public record PerformanceEntity : EntityBase
    {
        private DateTime _from;
        private DateTime _to;

        public Guid BandId { get; set; }
        public BandEntity Band { get; set; }

        public Guid StageId { get; set; }
        public StageEntity Stage { get; set; }

        public DateTime From
        {
            get { return _from; }
            set
            {
                if (value > _to)
                {
                    _to = new DateTime();
                }

                _from = value;
            }
        }
        public DateTime To
        {
            get { return _to; }
            set
            {
                if (value < _from)
                {
                    _from = new DateTime();
                }

                _to = value;
            }
        }

        public virtual bool Equals(PerformanceEntity other)
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
                   && BandId.Equals(other.BandId)
                   && StageId.Equals(other.StageId)
                   && From.Equals(other.From)
                   && To.Equals(other.To);
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), BandId, Band, StageId, Stage, From, To);

        private sealed class PerformanceEntityWithoutReferenceFields : IEqualityComparer<PerformanceEntity>
        {
            public bool Equals(PerformanceEntity x, PerformanceEntity y) => !ReferenceEquals(x, null) && x.Equals(y);

            public int GetHashCode(PerformanceEntity obj) => obj.GetHashCode();
        }

        public static IEqualityComparer<PerformanceEntity> PerformanceWithoutReferenceFieldsComparer { get; }
            = new PerformanceEntityWithoutReferenceFields();


    }


}
