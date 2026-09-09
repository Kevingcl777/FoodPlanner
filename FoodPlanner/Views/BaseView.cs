namespace Fooddaily.Portable.Views
{
    using Fooddaily.Portable.Data;
    using Fooddaily.Portable.Abstractions;
    using Fooddaily.Portable.ViewModels;
    using Xamarin.Forms;

    public class BaseView : ContentPage
    {
        public BaseView()
        {
            this.SetBinding(TitleProperty, new Binding(BaseViewModel.TitlePropertyName));
            this.SetBinding(IconProperty, new Binding(BaseViewModel.IconPropertyName));
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => 
            {
                var result = await this.DisplayAlert(string.Empty, Data.Constants.EXIT, Data.Constants.YES, Data.Constants.NO);// replace with localisation later
                if (!result)
                {
                    return;
                }

                if(Device.RuntimePlatform.Equals(Device.Android))
                {
                    DependencyService.Get<IApplicationMethods>().CloseApp();
                }
            });

            return true;
        }
    }
}

