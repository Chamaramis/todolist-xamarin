using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Agranillo.ToDoList.App.Models;
using Agranillo.ToDoList.App.Services.Database;
using Agranillo.ToDoList.App.Services.Localization;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Agranillo.ToDoList.App.ViewModels
{
    public class ItemPageViewModel : ViewModelBase
    {
        #region Dependencies

        private readonly IDatabaseService databaseService;
        private readonly IPageDialogService pageDialogService;
        private readonly ILocalizationService localizationService;

        #endregion

        #region Constructor

        public ItemPageViewModel(INavigationService navigationService, IDatabaseService databaseService, IPageDialogService pageDialogService, ILocalizationService localizationService)
            : base(navigationService)
        {
            this.databaseService = databaseService;
            this.pageDialogService = pageDialogService;
            this.localizationService = localizationService;
        }

        #endregion

        #region Overrides

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue("Item", out ToDoItem item))
            {
                base.Title = this.localizationService.GetString("ItemPage.Title.Editing");
                this.Creating = false;
                this.Id = item.Id;
                this.ItemTitle = item.Title;
                this.Description = item.Description;
                this.Finished = item.Finished;
            }
            else
            {
                base.Title = this.localizationService.GetString("ItemPage.Title.Creating");
                this.Creating = true;
            }
        }

        #endregion

        #region Properties

        private bool creating;

        public bool Creating
        {
            get => this.creating;
            set => SetProperty(ref this.creating, value);
        }

        private int id;

        public int Id
        {
            get => this.id;
            set => SetProperty(ref this.id, value);
        }

        private string itemTitle;

        public string ItemTitle
        {
            get => this.itemTitle;
            set => SetProperty(ref this.itemTitle, value);
        }

        private string description;

        public string Description
        {
            get => this.description;
            set => SetProperty(ref this.description, value);
        }

        private bool finished;

        public bool Finished
        {
            get => this.finished;
            set => SetProperty(ref this.finished, value);
        }

        #endregion

        #region Commands

        public ICommand SaveItem => new Command(this.SaveItemAction);

        #endregion

        #region CommandActions

        private async void SaveItemAction()
        {
            var entity = new ToDoItem();
            entity.Title = this.ItemTitle;
            entity.Description = this.Description;
            var saved = false;

            if (this.Creating)
            {
                entity.Id = 0;
                entity.Finished = false;
                saved = await this.databaseService.Create(entity);
            }
            else
            {
                entity.Id = this.Id;
                entity.Finished = this.Finished;
                saved = await this.databaseService.Update(entity);
            }

            if (saved)
            {
                await this.NavigationService.GoBackAsync();
            }
            else
            {
                await this.pageDialogService.DisplayAlertAsync(
                    this.localizationService.GetString("ItemPage.Alert.Title"),
                    this.localizationService.GetString("ItemPage.Alert.Message"),
                    this.localizationService.GetString("ItemPage.Alert.Accept"));
            }
        }

        #endregion
    }
}
