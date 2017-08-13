using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Extensions;

[assembly: ExportRenderer(typeof(Separator), typeof(SeparatorRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class SeparatorRenderer : ViewRenderer<Separator, Gtk.EventBox>
    {
        private Gtk.Separator _separator;

        protected override void OnElementChanged(ElementChangedEventArgs<Separator> e)
        {
            if (Control == null)
            {
                _separator = new Gtk.HSeparator();
                Add(_separator);
                _separator.ShowAll();

                SetNativeControl(this);
            }

            if (e.NewElement != null)
            {
                RecreateSeparator();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Separator.OrientationProperty.PropertyName)
                RecreateSeparator();

            base.OnElementPropertyChanged(sender, e);
        }

        private void RecreateSeparator()
        {
            if (_separator != null)
            {
                this.RemoveFromContainer(_separator);
            }

            var orientation = Element.Orientation;

            switch (orientation)
            {
                case SeparatorOrientation.Horizontal:
                    _separator = new Gtk.HSeparator();
                    break;
                case SeparatorOrientation.Vertical:
                    _separator = new Gtk.VSeparator();
                    break;
            }

            Add(_separator);
            _separator.ShowAll();
        }
    }
}