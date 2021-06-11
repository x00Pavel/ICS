using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FestivalApp.App.Commands;
using FestivalApp.App.Messages;
using FestivalApp.App.Services;
using FestivalApp.BL.Facades;
using FestivalApp.BL.Models;

namespace FestivalApp.App.ViewModels
{
    public class PerformanceNewViewModel : ViewModelBase, IPerformanceNewViewModel
    {
        private readonly PerformanceFacade _facade;
        private readonly BandFacade _bandFacade;
        private readonly StageFacade _stageFacade;
        private readonly IMediator _mediator;
        public PerformanceDetailModel Performance { get; set; }

        public DateTime From { get; set; } = DateTime.Today;
        public DateTime To { get; set; } = DateTime.Today;
        public Dictionary<Guid, string> Bands { get; set; }
        public Dictionary<Guid, string> Stages { get; set; }
        public Guid BandId { get; set; } = Guid.Empty;
        public Guid StageId { get; set; } = Guid.Empty;


        public RelayCommand SaveCommand { get; set; }

        public PerformanceNewViewModel(PerformanceFacade perfFacade, BandFacade bandFacade, StageFacade stageFacade, IMediator mediator)
        {
            _facade = perfFacade;
            _bandFacade = bandFacade;
            _stageFacade = stageFacade;
            _mediator = mediator;

            var bands = _bandFacade.GetAll().Select(x => new KeyValuePair<Guid, string>(x.Id, x.Name));
            var stages = _stageFacade.GetAll().Select(x => new KeyValuePair<Guid, string>(x.Id, x.Name));

            Bands = new Dictionary<Guid, string>(bands);
            Stages = new Dictionary<Guid, string>(stages);
            SaveCommand = new RelayCommand(Save, CanSave);
        }

        public void Save()
        {
            Performance = new PerformanceDetailModel
            {
                From = From,
                To = To,
                BandDetailedModel = _bandFacade.GetById(BandId),
                StageDetailedModel = _stageFacade.GetById(StageId)
            };

            try
            {
                var detailModel = _facade.Save(Performance);
                // Start communication with BandsViewModel
                MessageBox.Show("Performance is saved.", "Save", MessageBoxButton.OK, MessageBoxImage.Information,
                    MessageBoxResult.OK);
                _mediator.Send(new UpdateMessage<PerformanceDetailModel> { Model = detailModel });
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show($"There is collision in time for this band of fot this stage. Choose another time slot, another band or another stage.",
                    "Collision", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

        }

        private bool CanSave()
        {
            return BandId != Guid.Empty &&
                   StageId != Guid.Empty &&
                   From < To;
        }

        public void Load(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
