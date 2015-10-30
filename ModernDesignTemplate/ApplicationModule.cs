using System;
using System.Linq;
using System.Reflection;
using Ninject.Modules;

namespace ModernDesignTemplate
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IViewModelSwitcher>().To<ViewModelSwitcher>();
            Bind<IDataTemplateManager>().To<DataTemplateManager>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type viewModel in assembly.GetTypes().Where(x => typeof(IViewModel).IsAssignableFrom(x) && !x.IsInterface))
            {
                Bind<IViewModel>().To(viewModel);
            }
        }
    }
}