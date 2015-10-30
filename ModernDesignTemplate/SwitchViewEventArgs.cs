namespace ModernDesignTemplate
{
    public class SwitchViewEventArgs
    {
        public SwitchViewEventArgs(string viewModelName)
        {
            ViewModelName = viewModelName;
        }

        public string ViewModelName { get; set; }
    }
}