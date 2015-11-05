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
        public delegate void SwitchViewEventHandler(object sender, SwitchViewEventArgs e);

        private readonly IList<ISwitchableViewModel> _viewModels;
        private readonly Type _defaultViewModel;
        private ISwitchableViewModel _currentView;

        public ViewModelSwitcher(IEnumerable<ISwitchableViewModel> viewModels, Type defaultViewModel)
        {
            _viewModels = viewModels.ToList();
            _defaultViewModel = defaultViewModel;

            foreach (ISwitchableViewModel viewModel in _viewModels)
            {
                //TODO: Add abstraction for the viewmodel collection (beware of circular dependency)
                if (viewModel.GetType() == typeof(HomeViewModel))
                {
                    HomeViewModel homeViewModel = (HomeViewModel)viewModel;
                    homeViewModel.ViewModels = _viewModels.Where(x => x != homeViewModel);
                }

                viewModel.Switch += Switch;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ISwitchableViewModel CurrentView
        {
            get { return _currentView ?? (_currentView = _viewModels.Single(x => x.GetType() == _defaultViewModel)); }
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