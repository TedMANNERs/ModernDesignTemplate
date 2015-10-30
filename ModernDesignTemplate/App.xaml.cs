using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace ModernDesignTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            IDataTemplateManager manager = ApplicationKernel.Get<IDataTemplateManager>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            foreach (Type viewModel in types.Where(x => typeof(IViewModel).IsAssignableFrom(x) && !x.IsInterface))
            {
                string viewName = viewModel.Name.Replace("ViewModel", "View");
                Type view = types.First(x => x.Name == viewName);
                manager.RegisterDataTemplate(viewModel, view);
            }
        }
    }
}