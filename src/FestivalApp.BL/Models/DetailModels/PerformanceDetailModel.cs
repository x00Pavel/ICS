using System;

namespace FestivalApp.BL.Models
{
    public record PerformanceDetailModel : IModel
    {
        public Guid Id { get; init; }

        public BandDetailModel BandDetailedModel { get; set; }

        public StageDetailModel StageDetailedModel { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
