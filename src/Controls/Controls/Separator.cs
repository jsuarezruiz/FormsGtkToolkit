using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public enum SeparatorOrientation
    {
        Horizontal,
        Vertical
    }

    public class Separator : ContentView
    {
        public SeparatorOrientation Orientation
        {
            get { return (SeparatorOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation", typeof(SeparatorOrientation), typeof(Separator), SeparatorOrientation.Horizontal,
                BindingMode.TwoWay);
    }
}