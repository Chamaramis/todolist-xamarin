using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using Agranillo.ToDoList.App.Resources;

namespace Agranillo.ToDoList.App.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private ResourceManager resourceManager;

        public LocalizationService()
        {
            this.resourceManager = new ResourceManager(typeof(AppResources));
        }

        public string GetString(string key) => this.resourceManager.GetString(key ?? string.Empty) ?? string.Empty;
    }
}
