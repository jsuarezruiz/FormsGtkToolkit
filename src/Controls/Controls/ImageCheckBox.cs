using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class ImageCheckBox : ContentView
    {
        public ImageSource CheckedImage
        {
            get { return (ImageSource)GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        public static readonly BindableProperty CheckedImageProperty =
            BindableProperty.Create("CheckedImage", typeof(ImageSource), typeof(ImageCheckBox), default(ImageCheckBox),
                BindingMode.TwoWay);

        public ImageSource UncheckedImage
        {
            get { return (ImageSource)GetValue(UncheckedImageProperty); }
            set { SetValue(UncheckedImageProperty, value); }
        }

        public static readonly BindableProperty UncheckedImageProperty =
            BindableProperty.Create("UncheckedImage", typeof(ImageSource), typeof(ImageCheckBox), default(ImageCheckBox),
                BindingMode.TwoWay);
    }
}