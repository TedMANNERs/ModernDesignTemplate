using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ModernDesignTemplate.Annotations;

namespace ModernDesignTemplate
{
    public class ViewModelSwitcher : IViewModelSwitcher, INotifyPropertyChanged
    {
        private readonly IList<ISwitchableViewModel> _viewModels;
        private ISwitchableViewModel _currentView;
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void SwitchViewEventHandler(object sender, Type viewModel);

        public ViewModelSwitcher(IList<ISwitchableViewModel> viewModels)
        {
            _viewModels = viewModels;

            foreach (ISwitchableViewModel viewModel in viewModels)
            {
                viewModel.Switch += Switch;
            }

            CurrentView = _viewModels.FirstOrDefault();
        }

        public ISwitchableViewModel CurrentView
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

        public void Switch(object sender, Type viewModel)
        {
            CurrentView = _viewModels.Single(x => x.GetType() == viewModel);
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