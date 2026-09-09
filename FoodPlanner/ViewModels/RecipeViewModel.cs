namespace Fooddaily.Portable.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Fooddaily.Portable.Models;
    using Fooddaily.Portable.Data;
    using Xamarin.Forms;

    public class RecipeViewModel : BaseViewModel
    {
        public RecipeViewModel()
        {
            this.Title = Data.Constants.TITLE;
            this.Icon = Data.Constants.IMG4;
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private ObservableCollection<Result.RecipeGroup> rg = new ObservableCollection<Result.RecipeGroup>();

        public ObservableCollection<Result.RecipeGroup> Rg
        {
            get => this.rg;
            set
            {
                this.rg = value;
                this.OnPropertyChanged(); 
            }
        }

        private Result selectedRg;

        /// <summary>
        /// Gets or sets the selected feed item
        /// </summary>
        public Result SelectedRg
        {
            get => this.selectedRg;
            set
            {
                this.selectedRg = value;
                this.OnPropertyChanged();
            }
        }

        private string message;

        public string Message
        {

            get => this.message;

            set
            {
                this.message = value;
                this.OnPropertyChanged();
            }
        }

        private Command loadItemsCommand;

        /// <summary>
        /// Command to load/refresh items
        /// </summary>
        public Command LoadItemsCommand => this.loadItemsCommand ?? (this.loadItemsCommand = new Command(async () => await this.ExecuteLoadItemsCommand()));

        private async Task ExecuteLoadItemsCommand()
        {
            /*
            if (this.IsBusy)
                return;

            this.IsBusy = true;
            */


            IsRefreshing = true;
            try
            {
                this.Rg.Clear();

                await RecipeData.GetRecipe(this.Message, this.Rg);

            }
            catch (Exception ex)
            {
                App.IT.IssueLogger(Data.Constants.ERROR + ex.InnerException);
            }
            finally
            {
                //this.IsBusy = false;
                IsRefreshing = false;
            }
        }

        public Command RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //IsRefreshing = true;

                    await this.ExecuteLoadItemsCommand();

                    //IsRefreshing = false;
                });
            }
        }
    }
}
