using System;
using System.Windows;
using System.Windows.Markup;

namespace ModernDesignTemplate
{
    public class DataTemplateManager : IDataTemplateManager
    {
        public void RegisterDataTemplate<TViewModel, TView>() where TView : FrameworkElement
        {
            RegisterDataTemplate(typeof(TViewModel), typeof(TView));
        }

        public void RegisterDataTemplate(Type viewModel, Type view)
        {
            DataTemplate template = CreateTemplate(viewModel, view);
            object key = template.DataTemplateKey;
            Application.Current.Resources.Add(key, template);
        }

        private DataTemplate CreateTemplate(Type viewModel, Type view)
        {
            const string XamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><v:{1} /></DataTemplate>";
            string xaml = String.Format(XamlTemplate, viewModel.Name, view.Name);

            ParserContext context = new ParserContext { XamlTypeMapper = new XamlTypeMapper(new string[0]) };

            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModel.Namespace, viewModel.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction("v", view.Namespace, view.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("v", "v");

            DataTemplate template = (DataTemplate)XamlReader.Parse(xaml, context);
            return template;
        }
    }
}