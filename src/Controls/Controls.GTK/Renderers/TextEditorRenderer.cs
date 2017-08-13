using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Extensions;
using Gtk;

[assembly: ExportRenderer(typeof(TextEditor), typeof(TextEditorRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class TextEditorRenderer : ViewRenderer<TextEditor, Controls.TextEditor>
    {
        private const string TextColorTagName = "text-color";

        private bool _disposed;
        private Controls.TextEditor _textEditor;

        protected override void OnElementChanged(ElementChangedEventArgs<TextEditor> e)
        {
            if (Control == null)
            {
                _textEditor = new Controls.TextEditor();
                _textEditor.TextView.Buffer.TagTable.Add(new TextTag(TextColorTagName));

                Add(_textEditor);

                _textEditor.ShowAll();

                SetNativeControl(_textEditor);
            }

            if (e.NewElement != null)
            {
                UpdateText();
                UpdateTextColor();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TextEditor.TextProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == TextEditor.TextColorProperty.PropertyName)
                UpdateTextColor();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
            }

            base.Dispose(disposing);
        }

        private void UpdateText()
        {
            Control.UpdateText(Element.Text);
        }

        private void UpdateTextColor()
        {
            if (!Element.TextColor.IsDefault)
            {
                var textColor = Element.TextColor.ToGtkColor();

                TextBuffer buffer = Control.TextView.Buffer;
                TextTag tag = buffer.TagTable.Lookup(TextColorTagName);
                tag.ForegroundGdk = Element.IsEnabled ? textColor : Control.Style.Foregrounds[(int)StateType.Normal];
                Control.TextView.Buffer.ApplyTag(tag, buffer.StartIter, buffer.EndIter);
            }
        }
    }
}