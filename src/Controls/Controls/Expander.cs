using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class Expander : ContentView
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(Expander), string.Empty,
                BindingMode.TwoWay);
    }
}