using System;
using System.Collections.Generic;

namespace FestivalApp.BL.Models
{
    public record PerformanceListModel : IModel
    {
        public Guid Id { get; init; }

        public string BandName { get; set; }
        public string StageName { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        private sealed class PerformanceListModelEqualityComparer : IEqualityComparer<PerformanceListModel>
        {
            public bool Equals(PerformanceListModel x, PerformanceListModel y) => !ReferenceEquals(x, null) && x.Equals(y);

            public int GetHashCode(PerformanceListModel obj) => obj.GetHashCode();
        }

        public static IEqualityComparer<PerformanceListModel> PerformanceListModelComparer { get; }
            = new PerformanceListModelEqualityComparer();
    }
}
