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
    public class BandsViewModel : ViewModelBase, IBandListViewModel
    {
        private readonly BandFacade _facade;
        private readonly IFactory<IBandDetailViewModel> _factory;
        private static IMediator _mediator;
        public RelayCommand ToNewCommand { get; set; }
        public RelayCommand<BandListModel> ToBandCommand { get; set; }
        public ObservableCollection<BandListModel> Bands { get; } = new();
        private ObservableCollection<IBandDetailViewModel> BandDetailViewModels { get; } = new();


        public BandsViewModel(BandFacade bandFacade, IMediator mediator, IFactory<IBandDetailViewModel> factory)
        {
            _mediator = mediator;
            _facade = bandFacade;
            _factory = factory;

            ToNewCommand = new RelayCommand(NewBand);
            ToBandCommand = new RelayCommand<BandListModel>(SelectBand);

            Load();

            // Register Mediator Actions
            mediator.Register<UpdateMessage<BandDetailModel>>(OnUpdated);
            mediator.Register<DeleteMessage<BandDetailModel>>(OnDelete);
        }

        private void NewBand() =>
            _mediator.Send(new NewEntityMessage<BandDetailModel>());
        public static void SelectBand(BandListModel model) =>
            _mediator.Send(new SelectedMessage<BandListModel> { Id = model.Id });
        public static void SelectBand(BandDetailModel model) =>
            _mediator.Send(new SelectedMessage<BandListModel> { Id = model.Id });

        public void Load()
        {
            Bands.Clear();
            Bands.AddRange(_facade.GetAll());
        }

        public IBandDetailViewModel OnNewBand()
        {
            var model = _factory.Create();
            model.Band = new BandDetailModel();
            return model;
        }

        public IBandDetailViewModel OnBandSelected(Guid Id)
        {
            var bandDetailViewModel =
                BandDetailViewModels.SingleOrDefault(vm => vm.Band.Id.Equals(Id));
            if (bandDetailViewModel == null)
            {
                bandDetailViewModel = _factory.Create();
                BandDetailViewModels.Add(bandDetailViewModel);
                bandDetailViewModel.Load(Id);
            }
            return bandDetailViewModel;
        }

        private void OnUpdated(UpdateMessage<BandDetailModel> message) => Load();

        private void OnDelete(DeleteMessage<BandDetailModel> obj)
        {
            var band = Bands.SingleOrDefault(x => x.Id == obj.Id);
            Bands.Remove(band);
        }
    }
}
