using Xamarin.Forms;
using Fooddaily.Portable.Helpers;
using Fooddaily.Portable.ViewModels;
using Fooddaily.Portable.Data;

namespace Fooddaily.Portable.Views
{
    public class RecipeView : BaseView
    {
        private RecipeViewModel ViewModel => this.BindingContext as RecipeViewModel;

        private Entry textView;

        public RecipeView()
        {
            this.BindingContext = new RecipeViewModel();
            

            this.BackgroundColor = Color.FromHex(Data.Constants.BGCOLOR1);
 

            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(Data.Constants.THICK_S, Data.Constants.THICK_S, Data.Constants.THICK_S, Data.Constants.THICK_M)
            };
            //
            var stackEnter = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };
            //
            var stackPowered = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(Data.Constants.THICK_S, Data.Constants.THICK_M, Data.Constants.THICK_S, Data.Constants.THICK_M),
                BackgroundColor = Color.White
            };

            var attrib = new Image
            {
                Source = new FileImageSource { File = Data.Constants.IMG2 },
                HeightRequest = Data.Constants.HEIGHT_BIG,
                WidthRequest = Data.Constants.WIDTH_BIG
            };

            stackPowered.Children.Add(attrib);
            stack.Children.Add(stackPowered);
            //
            // UITextView
            this.textView = new Entry
            {
                Text = Data.Constants.INGREDIENTS
            };
            this.textView.SetBinding(Entry.TextProperty, Data.Constants.MESSAGE);

            stackEnter.Children.Add(this.textView);
            //
          
            var imagePlay = new Image
            {
                Source = ImageSource.FromFile(Data.Constants.IMG1),
                Aspect = Aspect.AspectFill,
                HeightRequest = Data.Constants.HEIGHT_PIC,
                WidthRequest = Data.Constants.WID_PIC,
            };

            var tapPlay = new TapGestureRecognizer();
            tapPlay.Tapped += (s, e) => {
                ViewModel.RefreshCommand.Execute(null);
            };
            imagePlay.GestureRecognizers.Add(tapPlay);
            stackEnter.Children.Add(imagePlay);
            //
            stack.Children.Add(stackEnter);
            //



            var dataTemplate = new DataTemplate(typeof(ContentCell));
            dataTemplate.SetBinding(TextCell.TextProperty, Data.Constants.NAME);

            var listView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding(Data.Constants.NAME),
                ItemsSource = this.ViewModel.Rg,
                ItemTemplate = dataTemplate,
                GroupHeaderTemplate = new DataTemplate(typeof(GroupCell)),
                HasUnevenRows = true,
                IsPullToRefreshEnabled = true,

            };

            listView.SetBinding(ListView.RefreshCommandProperty, new Binding(Data.Constants.REFRESH));
            listView.SetBinding(ListView.IsRefreshingProperty, new Binding(Data.Constants.ISREFRESHING));

            listView.ItemTapped += (sender, args) => {
                if (listView.SelectedItem == null)
                    return;

                this.Navigation.PushAsync(new RecipeViewDetails(listView.SelectedItem as Models.Result.Recipe));
                //Device.OpenUri(new Uri((listView.SelectedItem as Models.Result.Recipe).url));
                
                
                App.IT.IssueLogger(Data.Constants.RECIPE_CHOSEN + (listView.SelectedItem as Models.Result.Recipe).uri);
                listView.SelectedItem = null;
            };
            
            
            stack.Children.Add(listView);

            var adBanner = new AdBanner
            {
                Size = AdBanner.Sizes.Standardbanner
            };
            stack.Children.Add(adBanner);


            this.Content = stack;

        }

        /// <summary>Method that is called to invalidate the layout of this <see cref="T:Xamarin.Forms.VisualElement" />. Raises the <see cref="E:Xamarin.Forms.VisualElement.MeasureInvalidated" /> event.</summary>
        /// <remarks>To be added.</remarks>
        protected override void InvalidateMeasure()
        {
            base.InvalidateMeasure();
            this.textView.WidthRequest = (Application.Current.MainPage.Width * 0.8);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.InvalidateMeasure();

            if (this.ViewModel == null || !this.ViewModel.CanLoadMore || this.ViewModel.IsBusy || this.ViewModel.Rg.Count > 0)
                return;

            this.ViewModel.LoadItemsCommand.Execute(null);
        }
    }
}
