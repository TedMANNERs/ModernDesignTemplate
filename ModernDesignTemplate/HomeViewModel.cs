using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ModernDesignTemplate.Annotations;
using TedTechVpn.UserInterface;

namespace ModernDesignTemplate
{
    public class HomeViewModel : IViewModel
    {
        public HomeViewModel()
        {
            string className = GetType().Name;
            Name = className.Substring(0, className.IndexOf("ViewModel", StringComparison.Ordinal));
            SwitchCommand = new DelegateCommand(obj => Switch.Invoke(this, "Test"), () => true);
        }

        public ICommand SwitchCommand { get; set; }
        public string Name { get; set; }
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