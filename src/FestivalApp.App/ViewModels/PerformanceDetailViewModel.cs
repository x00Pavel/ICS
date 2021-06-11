using System;
using System.Windows;
using FestivalApp.App.Commands;
using FestivalApp.App.Messages;
using FestivalApp.App.Services;
using FestivalApp.BL.Facades;
using FestivalApp.BL.Models;

namespace FestivalApp.App.ViewModels
{
    public class PerformanceDetailViewModel : ViewModelBase, IPerformanceDetailViewModel
    {
        private readonly PerformanceFacade _facade;
        private readonly IMediator _mediator;
        public PerformanceDetailModel Performance { get; set; }

        public RelayCommand<BandDetailModel> ToBandCommand { get; }
        public RelayCommand<StageDetailModel> ToStageCommand { get; }
        public RelayCommand SaveCommand { get; set; }

        public RelayCommand DeleteCommand { get; }

        public PerformanceDetailViewModel(PerformanceFacade facade, IMediator mediator)
        {
            _facade = facade;
            _mediator = mediator;

            ToBandCommand = new RelayCommand<BandDetailModel>(BandsViewModel.SelectBand);
            ToStageCommand = new RelayCommand<StageDetailModel>(StagesViewModel.SelectStage);
            DeleteCommand = new RelayCommand(OnDelete);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }


        private bool CanSave()
        {
            return Performance.From < Performance.To;
        }

        private void OnSave()
        {
            var result = MessageBox.Show($"Do you want to update this performance?", "Update", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);
            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                _facade.Save(Performance);
                // Start communication with BandsViewModel
                MessageBox.Show("Performance is updated.", "Update", MessageBoxButton.OK, MessageBoxImage.Information,
                    MessageBoxResult.OK);
                _mediator.Send(new UpdateMessage<PerformanceDetailModel> { Id = Performance.Id });
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show($"There is collision in time for this band of fot this stage. Choose another time slot",
                    "Collision", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

        }

        public void Load(Guid id)
        {
            Performance = _facade.GetById(id) ?? new PerformanceDetailModel();
        }

        private void OnDelete()
        {
            var result = MessageBox.Show($"Do you want to delete this performance?", "Delete", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                return;


            _facade.Delete(Performance);
            _mediator.Send(new DeleteMessage<PerformanceDetailModel> { Id = Performance.Id });
            // Start communication with other ViewModels

        }
    }
}
