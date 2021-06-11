using System;
using System.Collections.Generic;
using Nemesis.Essentials.Design;

namespace FestivalApp.BL.Models
{
    public record StageDetailModel : IModel
    {

        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Photo { get; set; }

        public ICollection<PerformanceListModel> Performances { get; set; } =
            new ValueCollection<PerformanceListModel>(PerformanceListModel.PerformanceListModelComparer);

    }


}
