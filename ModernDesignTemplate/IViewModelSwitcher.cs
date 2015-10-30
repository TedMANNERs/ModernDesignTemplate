namespace ModernDesignTemplate
{
    public interface IViewModelSwitcher
    {
        IViewModel CurrentView { get; set; }

        void Switch(object sender, string e);
    }
}