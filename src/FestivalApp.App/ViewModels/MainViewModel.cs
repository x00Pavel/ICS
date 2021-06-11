using FestivalApp.App.Commands;
using FestivalApp.App.Messages;
using FestivalApp.App.Services;
using FestivalApp.BL.Models;

namespace FestivalApp.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand ToPerformancesViewCommand { get; set; }
        public RelayCommand ToStagesViewCommand { get; set; }
        public RelayCommand ToBandsViewCommand { get; set; }

        public IPerformanceListViewModel PerformancesVM { get; set; }
        public IStageListViewModel StagesVM { get; set; }
        public IBandListViewModel BandsVM { get; set; }


        public MainViewModel(
            IBandListViewModel bandsVM,
            IStageListViewModel stagesVM,
            IPerformanceListViewModel performancesVM,
            IMediator mediator)
        {
            // Define other VMs
            BandsVM = bandsVM;
            StagesVM = stagesVM;
            PerformancesVM = performancesVM;

            // Define VM's Commands
            ToPerformancesViewCommand = new RelayCommand(o => SetView(PerformancesVM));
            ToStagesViewCommand = new RelayCommand(o => SetView(StagesVM));
            ToBandsViewCommand = new RelayCommand(o => SetView(BandsVM));

            // Set Current View Model
            SetView(PerformancesVM);

            // Register actions
            mediator.Register<SelectedMessage<BandListModel>>(m =>
                SetView(bandsVM.OnBandSelected(m.Id)));
            mediator.Register<SelectedMessage<StageListModel>>(m =>
                SetView(stagesVM.OnStageSelected(m.Id)));
            mediator.Register<SelectedMessage<PerformanceListModel>>(m =>
                SetView(performancesVM.OnPerformanceSelected(m.Id)));

            //// New model actions
            mediator.Register<NewEntityMessage<BandDetailModel>>(_ => SetView(bandsVM.OnNewBand()));
            mediator.Register<NewEntityMessage<StageDetailModel>>(_ => SetView(stagesVM.OnNewStage()));
            mediator.Register<NewEntityMessage<PerformanceDetailModel>>(_ => SetView(performancesVM.OnNewPerf()));

            //// Update model actions
            mediator.Register<UpdateMessage<PerformanceDetailModel>>(obj =>
            {
                PerformancesVM.Load();
                SetView(performancesVM.OnPerformanceSelected(obj.Id));
            });
            mediator.Register<UpdateMessage<StageDetailModel>>(_ =>
            {
                PerformancesVM.Load();
            });
            mediator.Register<UpdateMessage<BandDetailModel>>(_ =>
            {
                PerformancesVM.Load();
            });

            //// Delete model actions
            mediator.Register<DeleteMessage<StageDetailModel>>(_ => SetView(StagesVM));
            mediator.Register<DeleteMessage<BandDetailModel>>(_ => SetView(BandsVM));
            mediator.Register<DeleteMessage<PerformanceDetailModel>>(_ => SetView(PerformancesVM));
        }

    }
}
