using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public enum GridSplitterOrientation
    {
        Horizontal,
        Vertical
    }

    public class GridSplitter : ContentView
    {
        public GridSplitterOrientation Orientation
        {
            get { return (GridSplitterOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation", typeof(GridSplitterOrientation), typeof(GridSplitter), GridSplitterOrientation.Horizontal,
                BindingMode.TwoWay);

        public View Content1
        {
            get { return (View)GetValue(Content1Property); }
            set { SetValue(Content1Property, value); }
        }

        public static readonly BindableProperty Content1Property =
            BindableProperty.Create("Content1", typeof(View), typeof(GridSplitter), default(View),
                BindingMode.TwoWay);

        public View Content2
        {
            get { return (View)GetValue(Content2Property); }
            set { SetValue(Content2Property, value); }
        }

        public static readonly BindableProperty Content2Property =
            BindableProperty.Create("Content2", typeof(View), typeof(GridSplitter), default(View),
                BindingMode.TwoWay);
    }
}