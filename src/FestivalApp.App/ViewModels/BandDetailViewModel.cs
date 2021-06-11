using System;
using System.Collections.ObjectModel;
using System.Windows;
using FestivalApp.App.Commands;
using FestivalApp.App.Messages;
using FestivalApp.App.Services;
using FestivalApp.BL.Facades;
using FestivalApp.BL.Models;
using Microsoft.Win32;

namespace FestivalApp.App.ViewModels
{
    public class BandDetailViewModel : ViewModelBase, IBandDetailViewModel
    {
        private readonly BandFacade _facade;
        private readonly IMediator _mediator;
        public BandDetailModel Band { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand UploadImage { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand<PerformanceListModel> ToPerformanceCommand { get; set; }
        public ObservableCollection<PerformanceListModel> Performances { get; set; }
        public BandDetailViewModel(BandFacade bandFacade, IMediator mediator)
        {
            _facade = bandFacade;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(OnDelete);
            UploadImage = new RelayCommand(UploadImageCmd);
            ToPerformanceCommand = new RelayCommand<PerformanceListModel>(PerformancesViewModel.SelectPerformance);
            _mediator.Register<UpdateMessage<PerformanceDetailModel>>(UpdatePerfs);
        }

        private void UpdatePerfs(UpdateMessage<PerformanceDetailModel> obj)
        {
            if (obj.Model.BandDetailedModel.Id == Band.Id)
            {
                Load(Band.Id);
            }
        }

        public void Load(Guid id)
        {
            Band = _facade.GetById(id) ?? new BandDetailModel();
            Performances = new ObservableCollection<PerformanceListModel>(Band.Performances);
        }

        public void UploadImageCmd()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != true)
            {
                return;
            }

            Band.Photo = fileDialog.FileName;
            _mediator.Send(new UpdateMessage<BandDetailModel> { Model = Band });
            OnPropertyChanged($"Photo");

        }

        public void Save()
        {
            if (Band == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            var result = MessageBox.Show($"Do you want to save {Band?.Name}?", "Save",
                MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                return;

            Band = _facade.Save(Band);

            // Start communication with other ViewModels
            _mediator.Send(new UpdateMessage<BandDetailModel> { Model = Band });
        }

        private bool CanSave()
        {
            return Band != null
            && !string.IsNullOrWhiteSpace(Band.Name)
            && !string.IsNullOrWhiteSpace(Band.Genre)
            && !string.IsNullOrWhiteSpace(Band.ShortDescription);
        }

        public void OnDelete()
        {
            var result = MessageBox.Show($"Do you want to delete band {Band.Name}?", "Delete",
                MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes)
                return;

            _facade.Delete(Band);

            // Start communication with other ViewModels
            _mediator.Send(new DeleteMessage<BandDetailModel> { Id = Band.Id });
        }
    }
}
