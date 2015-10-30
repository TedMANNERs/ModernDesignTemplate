using System.ComponentModel;

namespace ModernDesignTemplate
{
    public interface ISwitchableViewModel : INotifyPropertyChanged
    {
        event ViewModelSwitcher.SwitchViewEventHandler Switch;
    }
}