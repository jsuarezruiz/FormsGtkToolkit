using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

[assembly: ExportRenderer(typeof(Expander), typeof(ExpanderRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class ExpanderRenderer : ViewRenderer<Expander, Gtk.EventBox>
    {
        private Gtk.Expander _expander;

        protected override void OnElementChanged(ElementChangedEventArgs<Expander> e)
        {
            if (Control == null)
            {
                _expander = new Gtk.Expander(string.Empty);
                Add(_expander);
                _expander.ShowAll();

                SetNativeControl(this);
            }

            if (e.NewElement != null)
            {
                UpdateTitle();
                UpdateContent();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Expander.TitleProperty.PropertyName)
                UpdateTitle();
            else if (e.PropertyName == Expander.ContentProperty.PropertyName)
                UpdateContent();

            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateTitle()
        {
            if (_expander != null)
            {
                var title = Element.Title;

                _expander.Label = title;
                _expander.TooltipText = title;
            }
        }

        private void UpdateContent()
        {
            if (_expander != null)
            {
                var content = Element.Content;
                if (content != null)
                {
                    var nativeContent = Platform.CreateRenderer(content);
                    _expander.Add(nativeContent.Container);
                }
            }
        }
    }
}
