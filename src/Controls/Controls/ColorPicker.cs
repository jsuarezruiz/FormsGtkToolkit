using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class ColorPicker : ContentView
    {
        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }

        public static readonly BindableProperty CurrentColorProperty =
            BindableProperty.Create("CurrentColor", typeof(Color), typeof(ColorPicker), Color.Black,
                BindingMode.TwoWay);

        public bool HasOpacity
        {
            get { return (bool)GetValue(HasOpacityProperty); }
            set { SetValue(HasOpacityProperty, value); }
        }

        public static readonly BindableProperty HasOpacityProperty =
            BindableProperty.Create("HasOpacity", typeof(bool), typeof(ColorPicker), true,
                BindingMode.TwoWay);

        public bool HasPalette
        {
            get { return (bool)GetValue(HasPaletteProperty); }
            set { SetValue(HasPaletteProperty, value); }
        }

        public static readonly BindableProperty HasPaletteProperty =
            BindableProperty.Create("HasPalette", typeof(bool), typeof(ColorPicker), true,
                BindingMode.TwoWay);
    }
}