using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class HyperLink : View
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(HyperLink), string.Empty,
                BindingMode.TwoWay);

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public static readonly BindableProperty UriProperty =
            BindableProperty.Create("Uri", typeof(string), typeof(HyperLink), string.Empty,
                BindingMode.TwoWay);
    }
}