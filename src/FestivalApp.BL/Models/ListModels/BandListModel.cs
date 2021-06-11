using System;
using System.Collections.Generic;

namespace FestivalApp.BL.Models
{
    public record BandListModel : IModel
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string ShortDescription { get; set; }

        private sealed class BandListModelEqualityComparer : IEqualityComparer<BandListModel>
        {
            public bool Equals(BandListModel x, BandListModel y) => !ReferenceEquals(x, null) && x.Equals(y);

            public int GetHashCode(BandListModel obj) => obj.GetHashCode();
        }

        public static IEqualityComparer<BandListModel> BandListModelComparer { get; } = new BandListModelEqualityComparer();
    }
}
