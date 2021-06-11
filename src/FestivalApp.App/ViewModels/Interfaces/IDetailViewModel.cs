using System;

namespace FestivalApp.App.ViewModels
{
    public interface IDetailViewModel : IViewModel
    {
        void Load(Guid id);
    }
}
