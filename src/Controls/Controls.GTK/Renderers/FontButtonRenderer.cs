using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

[assembly: ExportRenderer(typeof(FontButton), typeof(FontButtonRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class FontButtonRenderer : ViewRenderer<FontButton, Gtk.FontButton>
    {
        private bool _disposed;
        private Gtk.FontButton _fontButton;

        protected override void OnElementChanged(ElementChangedEventArgs<FontButton> e)
        {
            if (Control == null)
            {
                _fontButton = new Gtk.FontButton();
                _fontButton.UseFont = true;
                _fontButton.UseSize = true;

                _fontButton.FontSet += OnFontSet;

                Add(_fontButton);

                _fontButton.ShowAll();

                SetNativeControl(_fontButton);
            }

            if (e.NewElement != null)
            {
                UpdateFontName();
                UpdateTitle();
                UpdateShowSize();
                UpdateShowStyle();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FontButton.FontNameProperty.PropertyName)
                UpdateFontName();
            else if (e.PropertyName == FontButton.TitleProperty.PropertyName)
                UpdateTitle();
            else if (e.PropertyName == FontButton.ShowSizeProperty.PropertyName)
                UpdateShowSize();
            else if (e.PropertyName == FontButton.ShowStyleProperty.PropertyName)
                UpdateShowStyle();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                if (Control != null)
                {
                    Control.FontSet -= OnFontSet;
                }
            }

            base.Dispose(disposing);
        }

        private void UpdateFontName()
        {
            if (_fontButton != null)
            {
                var fontName = Element.FontName;
                _fontButton.FontName = fontName;
            }
        }

        private void UpdateTitle()
        {
            if (_fontButton != null)
            {
                var title = Element.Title;
                _fontButton.Title = title;
            }
        }

        private void UpdateShowSize()
        {
            if (_fontButton != null)
            {
                var showSize = Element.ShowSize;
                _fontButton.ShowSize = showSize;
            }
        }

        private void UpdateShowStyle()
        {
            if (_fontButton != null)
            {
                var showStyle = Element.ShowStyle;
                _fontButton.ShowStyle = showStyle;
            }
        }

        private void OnFontSet(object sender, System.EventArgs e)
        {
            Element.SendFontChanged();
        }
    }
}