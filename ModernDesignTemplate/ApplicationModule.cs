using System;
using System.Linq;
using Ninject.Modules;

namespace ModernDesignTemplate
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IViewModelSwitcher>().To<ViewModelSwitcher>();
            Bind<IDataTemplateManager>().To<DataTemplateManager>();

            foreach (Type viewModel in AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(
                            assembly => assembly.GetTypes()
                                .Where(x => typeof(IViewModel).IsAssignableFrom(x) && !x.IsInterface)))
            {
                Bind<IViewModel>().To(viewModel);
            }
        }
    }
}