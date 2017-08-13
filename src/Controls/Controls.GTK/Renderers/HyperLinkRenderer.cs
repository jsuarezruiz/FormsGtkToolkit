using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Extensions;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(HyperLink), typeof(HyperLinkRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class HyperLinkRenderer : ViewRenderer<HyperLink, Gtk.LinkButton>
    {
        private bool _disposed;

        protected override void OnElementChanged(ElementChangedEventArgs<HyperLink> e)
        {
            if (Control == null)
            {
                Gtk.LinkButton linkButton = new Gtk.LinkButton(string.Empty);
                linkButton.BorderWidth = 0;

                linkButton.Clicked += OnHyperLinkClicked;

                SetNativeControl(linkButton);
            }

            if (e.NewElement != null)
            {
                UpdateText();
                UpdateUri();
                UpdateBackground();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == HyperLink.TextProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == HyperLink.UriProperty.PropertyName)
                UpdateUri();
            else if (e.PropertyName == HyperLink.BackgroundColorProperty.PropertyName)
                UpdateBackground();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                if (Control != null)
                {
                    Control.Clicked -= OnHyperLinkClicked;
                }
            }

            base.Dispose(disposing);
        }

        private void UpdateText()
        {
            Control.Label = Element.Text;
        }

        private void UpdateUri()
        {
            Control.Uri = Element.Uri;
        }

        private void UpdateBackground()
        {
            var backgroundColor = Element.BackgroundColor;

            Control.ModifyBg(Gtk.StateType.Normal, backgroundColor.ToGtkColor());
        }

        private void OnHyperLinkClicked(object sender, System.EventArgs e)
        {
            if (Control != null && !string.IsNullOrEmpty(Control.Uri))
            {
                Process.Start(Control.Uri);
            }
        }
    }
}