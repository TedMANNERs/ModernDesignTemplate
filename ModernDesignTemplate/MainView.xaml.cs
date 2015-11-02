using System.Windows;

namespace ModernDesignTemplate
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            SourceInitialized += NativeMethods.OnSourceInitialized;
        }

        private void WindowMinimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void WindowMaximizeRestore(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void WindowClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}