using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FestivalApp.App.ViewModels
{
    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private IViewModel _currentView;

        public IViewModel CurrentViewModel
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public void SetView(IViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}
