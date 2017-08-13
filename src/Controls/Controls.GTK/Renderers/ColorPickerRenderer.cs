using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Extensions;

[assembly: ExportRenderer(typeof(ColorPicker), typeof(ColorPickerRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class ColorPickerRenderer : ViewRenderer<ColorPicker, Gtk.ColorSelection>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ColorPicker> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var colorPicker = new Gtk.ColorSelection();
                    SetNativeControl(colorPicker);
                }
            }

            if (e.NewElement != null)
            {
                UpdateCurrentColor();
                UpdateHasOpacity();
                UpdateHasPalette();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ColorPicker.CurrentColorProperty.PropertyName)
                UpdateCurrentColor();
            else if (e.PropertyName == ColorPicker.HasOpacityProperty.PropertyName)
                UpdateHasOpacity();
            else if (e.PropertyName == ColorPicker.HasPaletteProperty.PropertyName)
                UpdateHasPalette();

            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateCurrentColor()
        {
            Control.CurrentColor = Element.CurrentColor.ToGtkColor();
        }

        private void UpdateHasOpacity()
        {
            Control.HasOpacityControl = Element.HasOpacity;
        }

        private void UpdateHasPalette()
        {
            Control.HasPalette = Element.HasPalette;
        }
    }
}