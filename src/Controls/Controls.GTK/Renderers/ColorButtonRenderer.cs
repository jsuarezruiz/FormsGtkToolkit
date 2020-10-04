using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Extensions;

[assembly: ExportRenderer(typeof(ColorButton), typeof(ColorButtonRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class ColorButtonRenderer : ViewRenderer<ColorButton, Gtk.ColorButton>
    {
        private bool _disposed;
        private Gtk.ColorButton _colorButton;

        protected override void OnElementChanged(ElementChangedEventArgs<ColorButton> e)
        {
            if (Control == null)
            {
                _colorButton = new Gtk.ColorButton();
                _colorButton.ColorSet += OnColorSet;

                Add(_colorButton);
                _colorButton.ShowAll();

                SetNativeControl(_colorButton);
            }

            if (e.NewElement != null)
            {
                UpdateTitle();
                UpdateColor();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ColorButton.TitleProperty.PropertyName)
                UpdateTitle();
            else if (e.PropertyName == ColorButton.ColorProperty.PropertyName)
                UpdateColor();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                if(Control != null)
                {
                    Control.ColorSet -= OnColorSet;
                }
            }

            base.Dispose(disposing);
        }

        private void UpdateTitle()
        {
            var title = Element.Title;

            Control.Title = title;
        }

        private void UpdateColor()
        {
            var color = Element.Color;

            Control.Color = color.ToGtkColor();
        }

        private void OnColorSet(object sender, System.EventArgs e)
        {
            var selectedColor = Control.Color;
            var gdkMaxVal = 65535;

            Element.Color = new Color(selectedColor.Red / (double)gdkMaxVal, selectedColor.Green / (double)gdkMaxVal, selectedColor.Blue / (double)gdkMaxVal, 255);
            Element.SendColorChanged();
        }
    }
}