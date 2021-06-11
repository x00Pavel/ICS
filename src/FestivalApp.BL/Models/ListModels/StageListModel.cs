using System;

namespace FestivalApp.BL.Models
{
    public record StageListModel : IModel
    {
        public string Photo { get; set; }
        public Guid Id { get; init; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
