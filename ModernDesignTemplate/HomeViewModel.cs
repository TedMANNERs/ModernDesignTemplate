using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ModernDesignTemplate.Annotations;

namespace ModernDesignTemplate
{
    public class HomeViewModel : IViewModel
    {
        public HomeViewModel()
        {
            SwitchCommand = new DelegateCommand(obj => Switch.Invoke(this, typeof(TestViewModel)), () => true);
        }

        public ICommand SwitchCommand { get; set; }
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