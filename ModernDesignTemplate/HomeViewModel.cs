using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ModernDesignTemplate.Annotations;

namespace ModernDesignTemplate
{
    public class HomeViewModel : ISwitchableViewModel
    {
        public HomeViewModel()
        {
        }

        public IEnumerable<ISwitchableViewModel> ViewModels { get; set; }
        public event ViewModelSwitcher.SwitchViewEventHandler Switch;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}