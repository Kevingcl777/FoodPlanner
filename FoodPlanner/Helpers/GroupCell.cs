namespace Fooddaily.Portable.Helpers
{
    using Xamarin.Forms;
    using Fooddaily.Portable.Data;
    public class GroupCell : ViewCell
    {
        public GroupCell()
        {
            var text = new Label();
            text.SetBinding(Label.TextProperty, Data.Constants.NAME);
          
            var view = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                    //icon,
                    text
                }
            };

            this.Height = Data.Constants.HEIGHT_SMALL;
            this.View = view;
        }
    }

}
