using System;
using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class ColorButton : ContentView
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(ColorButton), string.Empty,
                BindingMode.TwoWay);

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create("Color", typeof(Color), typeof(ColorButton), Color.Black,
                BindingMode.TwoWay);

        public event EventHandler ColorChanged;

        public void SendColorChanged()
        {
            ColorChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}