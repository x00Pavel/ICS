using System;

namespace FestivalApp.App.ViewModels
{
    public interface IPerformanceListViewModel : IListViewModel
    {
        public IPerformanceNewViewModel OnNewPerf();

        public IPerformanceDetailViewModel OnPerformanceSelected(Guid Id);
    }
}
