using System;

namespace ModernDesignTemplate
{
    public class SwitchViewEventArgs : EventArgs
    {
        public SwitchViewEventArgs(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        public Type ViewModelType { get; set; }
    }
}