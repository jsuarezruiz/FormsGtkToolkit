using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

[assembly: ExportRenderer(typeof(ToggleButton), typeof(ToggleButtonRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class ToggleButtonRenderer : ViewRenderer<ToggleButton, Gtk.ToggleButton>
    {
        private bool _disposed;
        private Gtk.ToggleButton _toggleButton;

        protected override void OnElementChanged(ElementChangedEventArgs<ToggleButton> e)
        {
            if (Control == null)
            {
                _toggleButton = new Gtk.ToggleButton(Element.Text);
                _toggleButton.Inconsistent = false;

                _toggleButton.Toggled += OnToggled;

                Add(_toggleButton);

                _toggleButton.ShowAll();

                SetNativeControl(_toggleButton);
            }

            if (e.NewElement != null)
            {
                UpdateIsToggled();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == StatusBar.HasResizeGripProperty.PropertyName)
                UpdateIsToggled();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                if (Control != null)
                {
                    Control.Toggled -= OnToggled;
                }
            }

            base.Dispose(disposing);
        }

        private void UpdateIsToggled()
        {
            if (_toggleButton != null)
            {
                _toggleButton.Active = Element.IsToggled;
            }
        }

        private void OnToggled(object sender, System.EventArgs e)
        {
            Element.SendIsToggledChanged();
        }
    }
}