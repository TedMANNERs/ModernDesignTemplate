using System;

namespace ModernDesignTemplate
{
    public interface IViewModelSwitcher
    {
        ISwitchableViewModel CurrentView { get; set; }

        void Switch(object sender, Type e);
    }
}