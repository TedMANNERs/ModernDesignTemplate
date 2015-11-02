using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ModernDesignTemplate.Annotations;

namespace ModernDesignTemplate
{
    public class ViewModelSwitcher : IViewModelSwitcher, INotifyPropertyChanged
    {
        public delegate void SwitchViewEventHandler(object sender, SwitchViewEventArgs e);

        private readonly IList<ISwitchableViewModel> _viewModels;
        private ISwitchableViewModel _currentView;

        public ViewModelSwitcher(IList<ISwitchableViewModel> viewModels)
        {
            _viewModels = viewModels;

            foreach (ISwitchableViewModel viewModel in viewModels)
            {
                viewModel.Switch += Switch;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ISwitchableViewModel CurrentView
        {
            get { return _currentView ?? (_currentView = _viewModels.FirstOrDefault()); }
            set
            {
                if (Equals(value, _currentView))
                    return;
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public void Switch(object sender, SwitchViewEventArgs e)
        {
            CurrentView = _viewModels.Single(x => x.GetType() == e.ViewModelType);
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