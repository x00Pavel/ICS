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
    public class StageDetailViewModel : ViewModelBase, IStageDetailViewModel
    {
        private readonly StageFacade _facade;
        private readonly IMediator _mediator;
        public StageDetailModel Stage { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UploadImage { get; set; }
        public RelayCommand<PerformanceListModel> ToPerformanceCommand { get; set; }
        public ObservableCollection<PerformanceListModel> Performances { get; set; }
        public StageDetailViewModel(StageFacade facade, IMediator mediator)
        {
            _facade = facade;
            _mediator = mediator;

            SaveCommand = new RelayCommand(OnSave, CanSave);
            DeleteCommand = new RelayCommand(OnDelete, CanSave);
            UploadImage = new RelayCommand(OnUploadImage);
            ToPerformanceCommand = new RelayCommand<PerformanceListModel>(PerformancesViewModel.SelectPerformance);

            _mediator.Register<UpdateMessage<BandDetailModel>>(_ => Load(Stage.Id));
            _mediator.Register<UpdateMessage<PerformanceDetailModel>>(UpdatePerfs);
        }

        private void UpdatePerfs(UpdateMessage<PerformanceDetailModel> obj)
        {
            if (obj.Model.StageDetailedModel.Id == Stage.Id)
            {
                Load(Stage.Id);
            }
        }

        private void OnSave()
        {
            if (Stage == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            var result = MessageBox.Show($"Do you want to save stage {Stage.Name}?", "Delete", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                return;

            Stage = _facade.Save(Stage);

            // Start communication with other ViewModels
            _mediator.Send(new UpdateMessage<StageDetailModel> { Model = Stage });
        }

        private bool CanSave()
        {
            return Stage != null
                && !string.IsNullOrWhiteSpace(Stage.Name);
        }


        public void OnUploadImage()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images|*.jpeg;*.png|All|*";
            if (fileDialog.ShowDialog() != true)
            {
                return;
            }

            Stage.Photo = fileDialog.FileName;
            MessageBox.Show("Image is updated. Save to change", "Image update", MessageBoxButton.OK);
        }
        private void OnDelete()
        {
            var result = MessageBox.Show($"Do you want to delete stage {Stage.Name}?", "Delete", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                return;
            _facade.Delete(Stage);

            // Start communication with other ViewModels
            _mediator.Send(new DeleteMessage<StageDetailModel> { Id = Stage.Id });
        }

        public void Load(Guid id)
        {
            Stage = _facade.GetById(id) ?? new StageDetailModel();
            Performances = new ObservableCollection<PerformanceListModel>(Stage.Performances);
        }
    }
}
