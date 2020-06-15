using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Agranillo.ToDoList.App.Models;
using Agranillo.ToDoList.App.Services.Database;
using Agranillo.ToDoList.App.Services.Localization;
using Prism.Services;
using Xamarin.Forms;

namespace Agranillo.ToDoList.App.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Dependencies

        private readonly IDatabaseService databaseService;
        private readonly IPageDialogService pageDialogService;
        private readonly ILocalizationService localizationService;

        #endregion

        #region Constructor

        public MainPageViewModel(
            INavigationService navigationService,
            IDatabaseService databaseService,
            IPageDialogService pageDialogService,
            ILocalizationService localizationService)
            : base(navigationService)
        {
            this.databaseService = databaseService;
            this.pageDialogService = pageDialogService;
            this.localizationService = localizationService;
            base.Title = this.localizationService.GetString("MainPage.Title");
        }

        #endregion

        #region Overrides

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await this.ReloadData();
        }

        #endregion

        #region Properties

        private List<ToDoItem> toDoList = new List<ToDoItem>();

        public List<ToDoItem> ToDoList
        {
            get => this.toDoList;
            set => base.SetProperty(ref this.toDoList, value);
        }

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => base.SetProperty(ref this.isRefreshing, value);
        }


        #endregion

        #region Commands

        public ICommand AddItem => new Command(this.AddItemAction);
        public ICommand EditItem => new Command<ToDoItem>(this.EditItemAction);
        public ICommand DeleteItem => new Command<ToDoItem>(this.DeleteItemAction);
        public ICommand Refresh => new Command(this.RefreshAction);

        #endregion

        #region CommandActions

        private async void AddItemAction()
        {
            await this.NavigationService.NavigateAsync("ItemPage");
        }

        private async void EditItemAction(ToDoItem item)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("Item", item);
            await this.NavigationService.NavigateAsync("ItemPage", navigationParameters);
        }

        private async void DeleteItemAction(ToDoItem item)
        {
            bool confirmed = await this.pageDialogService.DisplayAlertAsync(this.localizationService.GetString("MainPage.Delete.Confirm.Title"), this.localizationService.GetString("MainPage.Delete.Confirm.Message"), this.localizationService.GetString("MainPage.Delete.Confirm.Accept"), this.localizationService.GetString("MainPage.Delete.Confirm.Cancel"));
            if (confirmed)
            {
                bool deleted = await this.databaseService.Delete(item);
                if (deleted)
                {
                    await this.ReloadData();
                }
                else
                {
                    await this.pageDialogService.DisplayAlertAsync(this.localizationService.GetString("MainPage.Delete.Result.Title"), this.localizationService.GetString("MainPage.Delete.Result.Message"), this.localizationService.GetString("MainPage.Delete.Result.Accept"));
                }
            }
        }

        private async void RefreshAction()
        {
            this.IsRefreshing = true;
            await this.ReloadData();
            this.IsRefreshing = false;
        }

        #endregion

        #region Miscellaneous

        private async Task ReloadData()
        {
            this.ToDoList = await this.databaseService.Get<ToDoItem>();
        }

        #endregion
    }
}
