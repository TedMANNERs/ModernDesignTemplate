using System;
using System.Windows;

namespace ModernDesignTemplate
{
    public interface IDataTemplateManager
    {
        void RegisterDataTemplate<TViewModel, TView>() where TView : FrameworkElement;

        void RegisterDataTemplate(Type viewModel, Type view);
    }
}