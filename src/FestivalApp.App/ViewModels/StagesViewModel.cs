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
    public class StagesViewModel : ViewModelBase, IStageListViewModel
    {

        private readonly StageFacade _facade;
        private readonly IFactory<IStageDetailViewModel> _factory;
        private static IMediator _mediator;
        public RelayCommand<StageListModel> ToStageCommand { get; set; }

        public ObservableCollection<StageListModel> Stages { get; } = new();
        public ObservableCollection<IStageDetailViewModel> StageDetailViewModels { get; } = new();

        public RelayCommand ToNewCommand { get; set; }
        public StagesViewModel(StageFacade stageFacade, IMediator mediator, IFactory<IStageDetailViewModel> factory)
        {
            _mediator = mediator;
            _facade = stageFacade;
            _factory = factory;

            // Define VM's Commands
            ToStageCommand = new RelayCommand<StageListModel>(SelectStage);
            ToNewCommand = new RelayCommand(NewStage);

            Load();

            // Register mediator actions
            mediator.Register<UpdateMessage<StageDetailModel>>(_ => Load());
            mediator.Register<DeleteMessage<StageDetailModel>>(_ => Load());
        }

        private void NewStage() =>
            _mediator.Send(new NewEntityMessage<StageDetailModel>());

        public static void SelectStage(StageListModel model) =>
            _mediator.Send(new SelectedMessage<StageListModel> { Id = model.Id });
        public static void SelectStage(StageDetailModel model) =>
            _mediator.Send(new SelectedMessage<StageListModel> { Id = model.Id });

        public void Load()
        {
            Stages.Clear();
            Stages.AddRange(_facade.GetAll());
        }

        public IStageDetailViewModel OnNewStage()
        {
            var model = _factory.Create();
            model.Stage = new StageDetailModel();

            return model;
        }

        public IStageDetailViewModel OnStageSelected(Guid Id)
        {
            var stage =
                StageDetailViewModels.SingleOrDefault(vm => vm.Stage.Id.Equals(Id));
            if (stage == null)
            {
                stage = _factory.Create();
                StageDetailViewModels.Add(stage);
                stage.Load(Id);
            }

            return stage;
        }
    }
}
