using Plugin.Share;
using System;
using Xamarin.Forms;
using Fooddaily.Portable.Data;

namespace Fooddaily.Portable.Views
{
    public class RecipeViewDetails : BaseView
    {
        public RecipeViewDetails(Models.Result.Recipe item)
        {
            this.BindingContext = item;

            var imgR = new Image
            {
                Source = item.image,
                HeightRequest = Data.Constants.HEIGHT,
                WidthRequest = Data.Constants.WIDTH
            };

            var boxH = new BoxView() { Color = Color.FromHex(Data.Constants.BGCOLOR2), HeightRequest = Data.Constants.HEIGHTR };
            var boxI = new BoxView() { Color = Color.FromHex(Data.Constants.BGCOLOR3), HeightRequest = Data.Constants.HEIGHTR };
            var txtT = new Label
            {
                Text = item.label,
                Style = Device.Styles.TitleStyle,
                FontAttributes = FontAttributes.Bold
            };

            var txtHealth = new Label
            {
                Text = Data.Constants.HEALTHLABEL,
                Style = Device.Styles.CaptionStyle,
                FontAttributes = FontAttributes.Bold
            };
            var health = string.Empty;
            foreach(var tmpH in item.healthLabels)
            {
                health = health + tmpH + Data.Constants.NEWL;
            }
            var txtH = new Label
            {
                Text = health,
                Style = Device.Styles.ListItemTextStyle
            };

            var txtIngredients = new Label
            {
                Text = Data.Constants.INGREDIENTS,
                Style = Device.Styles.CaptionStyle,
                FontAttributes = FontAttributes.Bold
            };
            var ingredients = string.Empty;
            foreach (var tmpI in item.ingredientLines)
            {
                ingredients = ingredients + tmpI + Data.Constants.NEWL;
            }
            var txtI = new Label
            {
                Text = ingredients,
                Style = Device.Styles.ListItemDetailTextStyle
            };

            var link = new Label();
            link.Text = Data.Constants.DETAILS;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Device.OpenUri(new Uri(item.url));
            };
            link.GestureRecognizers.Add(tapGestureRecognizer);

            var scroll = new ScrollView();

            var stack = new StackLayout
            {
                Padding = new Thickness(Data.Constants.THICK_S, Data.Constants.THICK_S, Data.Constants.THICK_S, Data.Constants.THICK_M),
                Children =
                            {
                                txtT,
                                imgR,
                                txtHealth,
                                boxH,
                                txtH,
                                txtIngredients,
                                boxI,
                                txtI,
                                link
                            }
            };

            this.Content = new ScrollView { Content = stack };

            var share = new ToolbarItem
            {
                Icon = Data.Constants.IMG3,
                Text = Data.Constants.SHARE,
                Command = new Command(() => CrossShare.Current
                  .Share(new Plugin.Share.Abstractions.ShareMessage
                  {
                      Text = Data.Constants.TEXT + item.label + Data.Constants.SPACE + item.url,
                      Title = Data.Constants.SHARE,
                      Url = item.url
                  }))
            };

            this.ToolbarItems.Add(share);
        }
    }
}
