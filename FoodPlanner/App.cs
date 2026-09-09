using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Fooddaily.Portable.Views;
using Fooddaily.Portable.Helpers;
using Xamarin.Auth;

namespace Fooddaily.Portable
{
    public class App : Application
    {
        public static IssueTracker IT { get; set; }
        public static Account account;
        public static AccountStore store;


        public static Action HideLoginView
        {
            get
            {
                return new Action(() => Current.MainPage.Navigation.PopModalAsync());
            }
        }

        public static async Task NavigateToGeneral(string message)
        {
            await Current.MainPage.Navigation.PushAsync(new General(message));
        }

        public static void NavigateToApp()
        {
            //await App.Current.MainPage.Navigation.PushAsync(new RootPage());
            Current.MainPage = new RootPage();
        }

        public static void NavigateToLogin()
        {
            //await App.Current.MainPage.Navigation.PushAsync(new RootPage());
            Current.MainPage = new NavigationPage(new Main());

        }

        public App()
        {
            IT = new IssueTracker();
            /*
            App.store = AccountStore.Create();
            // The root page of your application
            //MainPage = new RootPage();
            App.account = App.store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            if (App.account != null)
            {
                App.NavigateToApp();
            }
            else
            {
                MainPage = new NavigationPage(new Main());//NavigateToLogin()
            }
            */
            NavigateToApp();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
