using FestivalApp.BL.Models;

namespace FestivalApp.App.ViewModels
{
    public interface IPerformanceDetailViewModel : IDetailViewModel
    {
        PerformanceDetailModel Performance { get; set; }
    }
}
