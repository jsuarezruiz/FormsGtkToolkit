using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

[assembly: ExportRenderer(typeof(FileButton), typeof(FileButtonRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class FileButtonRenderer : ViewRenderer<FileButton, Gtk.FileChooserButton>
    {
        private bool _disposed;
        private Gtk.FileChooserButton _fileButton;

        protected override void OnElementChanged(ElementChangedEventArgs<FileButton> e)
        {
            if (Control == null)
            {
                _fileButton = new Gtk.FileChooserButton(string.Empty, Gtk.FileChooserAction.Open);
                Add(_fileButton);

                _fileButton.ShowAll();

                SetNativeControl(_fileButton);
            }

            if (e.NewElement != null)
            {
                UpdateTitle();
                UpdateFileAction();
                UpdateCurrentFolder();
                UpdateShowHidden();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FileButton.TitleProperty.PropertyName)
                UpdateTitle();
            else if (e.PropertyName == FileButton.FileActionProperty.PropertyName)
                UpdateFileAction();
            else if (e.PropertyName == FileButton.CurrentFolderProperty.PropertyName)
                UpdateCurrentFolder();
            else if (e.PropertyName == FileButton.ShowHiddenProperty.PropertyName)
                UpdateShowHidden();

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

        private void UpdateTitle()
        {
            if(_fileButton != null)
            {
                _fileButton.Title = Element.Title;
            }
        }

        private void UpdateFileAction()
        {
            if (_fileButton != null)
            {
                var fileAction = Element.FileAction;

                switch(fileAction)
                {
                    case FileButtonAction.Open:
                        _fileButton.Action = Gtk.FileChooserAction.Open;
                        break;
                    case FileButtonAction.CreateFolder:
                        _fileButton.Action = Gtk.FileChooserAction.CreateFolder;
                        break;
                    case FileButtonAction.Save:
                        _fileButton.Action = Gtk.FileChooserAction.Save;
                        break;
                    case FileButtonAction.SelectFolder:
                        _fileButton.Action = Gtk.FileChooserAction.SelectFolder;
                        break;
                }
            }
        }

        private void UpdateCurrentFolder()
        {
            if (_fileButton != null)
            {
                _fileButton.SetCurrentFolder(Element.CurrentFolder);
            }
        }

        private void UpdateShowHidden()
        {
            if (_fileButton != null)
            {
                _fileButton.ShowHidden = Element.ShowHidden;
            }
        }
    }
}