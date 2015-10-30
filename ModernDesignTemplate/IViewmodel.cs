using System.ComponentModel;

namespace ModernDesignTemplate
{
    public interface IViewModel : INotifyPropertyChanged
    {
        string Name { get; set; }
        event ViewModelSwitcher.SwitchViewEventHandler Switch;
    }
}