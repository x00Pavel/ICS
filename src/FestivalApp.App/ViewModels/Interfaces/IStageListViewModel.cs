using System;

namespace FestivalApp.App.ViewModels
{
    public interface IStageListViewModel : IListViewModel
    {
        public IStageDetailViewModel OnNewStage();

        public IStageDetailViewModel OnStageSelected(Guid Id);
    }
}
