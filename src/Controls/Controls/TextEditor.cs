using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class TextEditor : ContentView
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty TextProperty =        
            BindableProperty.Create(nameof(Text), typeof(string), typeof(TextEditor), string.Empty);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TextEditor), Color.Black);
    }
}