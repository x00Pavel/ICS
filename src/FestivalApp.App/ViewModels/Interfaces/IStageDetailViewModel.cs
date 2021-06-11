using FestivalApp.BL.Models;

namespace FestivalApp.App.ViewModels
{
    public interface IStageDetailViewModel : IDetailViewModel
    {
        StageDetailModel Stage { get; set; }
    }
}
