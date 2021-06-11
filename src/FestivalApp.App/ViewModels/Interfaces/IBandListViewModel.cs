using System;

namespace FestivalApp.App.ViewModels
{
    public interface IBandListViewModel : IListViewModel
    {
        public IBandDetailViewModel OnNewBand();

        public IBandDetailViewModel OnBandSelected(Guid Id);
    }
}
