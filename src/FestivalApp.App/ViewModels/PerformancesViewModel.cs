using System;
using System.Collections.ObjectModel;
using System.Linq;
using FestivalApp.App.Commands;
using FestivalApp.App.Extensions;
using FestivalApp.App.Factories;
using FestivalApp.App.Messages;
using FestivalApp.App.Services;
using FestivalApp.BL.Facades;
using FestivalApp.BL.Models;


namespace FestivalApp.App.ViewModels
{
    public class PerformancesViewModel : ViewModelBase, IPerformanceListViewModel
    {
        public RelayCommand<PerformanceListModel> ToPerformanceCommand { get; set; }
        public RelayCommand ToNewCommand { get; set; }

        private readonly PerformanceFacade _facade;
        private static IMediator _mediator;
        private readonly IFactory<IPerformanceDetailViewModel> _factory;
        private IFactory<IPerformanceNewViewModel> _newViewFactory;
        public ObservableCollection<PerformanceListModel> Performances { get; } = new();
        private ObservableCollection<IPerformanceDetailViewModel> PerformanceDetailViewModels { get; } = new();

        public PerformancesViewModel(
            PerformanceFacade performanceFacade,
            IMediator mediator,
            IFactory<IPerformanceDetailViewModel> factory,
            IFactory<IPerformanceNewViewModel> newViewFactory
        )
        {
            _facade = performanceFacade;
            _mediator = mediator;
            _factory = factory;
            _newViewFactory = newViewFactory;

            // Define VM's Commands
            ToPerformanceCommand = new RelayCommand<PerformanceListModel>(SelectPerformance);
            ToNewCommand = new RelayCommand(NewPerformance);

            Load();

            // Register mediator actions
            mediator.Register<UpdateMessage<PerformanceDetailModel>>(_ => Load());
            mediator.Register<DeleteMessage<StageDetailModel>>(_ => Load());
            mediator.Register<DeleteMessage<BandDetailModel>>(_ => Load());
            mediator.Register<DeleteMessage<PerformanceDetailModel>>(_ => Load());
        }

        private void NewPerformance()
            => _mediator.Send(new NewEntityMessage<PerformanceDetailModel>());

        public static void SelectPerformance(PerformanceListModel model) =>
            _mediator.Send(new SelectedMessage<PerformanceListModel> { Id = model.Id });

        public void Load()
        {
            Performances.Clear();
            Performances.AddRange(_facade.GetAll().OrderBy(x => x.From));
        }

        public IPerformanceNewViewModel OnNewPerf() => _newViewFactory.Create();

        public IPerformanceDetailViewModel OnPerformanceSelected(Guid Id)
        {
            var performanceDetailViewModel = PerformanceDetailViewModels
                .SingleOrDefault(vm => vm.Performance.Id.Equals(Id));
            if (performanceDetailViewModel == null)
            {
                performanceDetailViewModel = _factory.Create();
                PerformanceDetailViewModels.Add(performanceDetailViewModel);
                performanceDetailViewModel.Load(Id);
            }

            return performanceDetailViewModel;
        }
    }
}
