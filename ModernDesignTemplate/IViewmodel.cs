using System.ComponentModel;

namespace ModernDesignTemplate
{
    public interface IViewModel : INotifyPropertyChanged
    {
        event ViewModelSwitcher.SwitchViewEventHandler Switch;
    }
}