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
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types = assembly.GetTypes();
                foreach (Type viewModel in types.Where(x => typeof(ISwitchableViewModel).IsAssignableFrom(x) && !x.IsInterface))
                {
                    string viewName = viewModel.Name.Replace("ViewModel", "View");
                    manager.RegisterDataTemplate(viewModel, types.Single(x => x.Name == viewName));
                }
            }
        }
    }
}