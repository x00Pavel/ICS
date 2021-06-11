using FestivalApp.BL.Models;

namespace FestivalApp.App.ViewModels
{
    public interface IPerformanceNewViewModel : IDetailViewModel
    {
        PerformanceDetailModel Performance { get; set; }
    }
}
