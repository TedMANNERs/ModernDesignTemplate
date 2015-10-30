using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ModernDesignTemplate.Annotations;

namespace ModernDesignTemplate
{
    public class ViewModelSwitcher : IViewModelSwitcher, INotifyPropertyChanged
    {
        private readonly IList<IViewModel> _viewModels;
        private IViewModel _currentView;
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void SwitchViewEventHandler(object sender, string args);

        public ViewModelSwitcher(IList<IViewModel> viewModels)
        {
            _viewModels = viewModels;

            foreach (IViewModel viewModel in viewModels)
            {
                viewModel.Switch += Switch;
            }

            CurrentView = _viewModels.FirstOrDefault();
        }

        public IViewModel CurrentView
        {
            get { return _currentView; }
            set
            {
                if (Equals(value, _currentView))
                    return;
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public void Switch(object sender, string viewModelName)
        {
            CurrentView = _viewModels.First(x => x.Name == viewModelName);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}