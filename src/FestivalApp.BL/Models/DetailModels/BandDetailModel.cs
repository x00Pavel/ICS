using System;
using System.Collections.Generic;
using Nemesis.Essentials.Design;

namespace FestivalApp.BL.Models
{
    public record BandDetailModel : IModel
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Photo { get; set; }
        public string CountryOfOrigin { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }

        public ICollection<PerformanceListModel> Performances { get; set; } =
            new ValueCollection<PerformanceListModel>(PerformanceListModel.PerformanceListModelComparer);
    }
}
