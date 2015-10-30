namespace ModernDesignTemplate
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Switcher = ApplicationKernel.Get<IViewModelSwitcher>();
        }

        public IViewModelSwitcher Switcher { get; set; }
    }
}