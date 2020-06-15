using System;
using System.Collections.Generic;
using System.Text;
using Agranillo.ToDoList.App.Services.Localization;
using Xamarin.Forms.Xaml;

namespace Agranillo.ToDoList.App.UI.MarkupExtensions
{
    public class LocalizationExtension : IMarkupExtension<string>
    {
        public string ResourceKey { get; set; }

        public string ProvideValue(IServiceProvider serviceProvider)
        {
            var localizationService = new LocalizationService();
            return localizationService.GetString(ResourceKey);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return this.ProvideValue(serviceProvider);
        }
    }
}
