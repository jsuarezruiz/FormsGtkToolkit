using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Renderers;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(ImageCheckBox), typeof(ImageCheckBoxRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class ImageCheckBoxRenderer : ViewRenderer<ImageCheckBox, Controls.ImageCheckBox>
    {
        private bool _disposed;

        protected override async void OnElementChanged(ElementChangedEventArgs<ImageCheckBox> e)
        {
            if (Control == null)
            {
                Controls.ImageCheckBox imageCheckBox = new Controls.ImageCheckBox();
                imageCheckBox.CheckedStateChanged += OnCheckedStateChanged;
                SetNativeControl(imageCheckBox);
            }

            if (e.NewElement != null)
            {
                await UpdateCheckedImageAsync();
                await UpdateUnCheckedImageAsync();
            }

            base.OnElementChanged(e);
        }

        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ImageCheckBox.CheckedImageProperty.PropertyName)
                await UpdateCheckedImageAsync();
            else if (e.PropertyName == ImageCheckBox.UncheckedImageProperty.PropertyName)
                await UpdateUnCheckedImageAsync();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (Control != null)
                {
                    Control.CheckedStateChanged -= OnCheckedStateChanged;
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }

        private async Task UpdateCheckedImageAsync()
        {
            var checkedImage = Element.CheckedImage;

            if (checkedImage != null)
            {
                Control.CheckedImage = await LoadImageAsync(checkedImage);
            }
        }

        private async Task UpdateUnCheckedImageAsync()
        {
            var unCheckedImage = Element.UncheckedImage;

            if (unCheckedImage != null)
            {
                Control.UncheckedImage = await LoadImageAsync(unCheckedImage);
            }
        }

        private async Task<Gdk.Pixbuf> LoadImageAsync(ImageSource imageSource)
        {
            IImageSourceHandler handler =
                Registrar.Registered.GetHandler<IImageSourceHandler>(imageSource.GetType());

            var image = await handler.LoadImageAsync(imageSource);

            return image;
        }

        private void OnCheckedStateChanged(object sender, System.EventArgs e)
        {
            // TODO:
        }
    }
}