using FestivalApp.BL.Models;

namespace FestivalApp.App.ViewModels
{
    public interface IBandDetailViewModel : IDetailViewModel
    {
        BandDetailModel Band { get; set; }
    }
}
