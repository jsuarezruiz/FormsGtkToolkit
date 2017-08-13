using Gtk;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Extensions;

[assembly: ExportRenderer(typeof(GridSplitter), typeof(GridSplitterRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class GridSplitterRenderer : ViewRenderer<GridSplitter, EventBox>
    {
        private bool _disposed;
        private Paned _paned;

        protected override void OnElementChanged(ElementChangedEventArgs<GridSplitter> e)
        {
            if (Control == null)
            {
                _paned = new HPaned();
                Add(_paned);
                _paned.ShowAll();

                SetNativeControl(this);
            }

            if (e.NewElement != null)
            {
                RecreateGridSplitter();
                UpdateContent1();
                UpdateContent2();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GridSplitter.OrientationProperty.PropertyName)
                RecreateGridSplitter();
            else if (e.PropertyName == GridSplitter.Content1Property.PropertyName)
                UpdateContent1();
            else if (e.PropertyName == GridSplitter.Content2Property.PropertyName)
                UpdateContent2();

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

        private void UpdateContent1()
        {
            if (_paned != null)
            {
                var content1 = Element.Content1;

                if (content1 != null)
                {
                    var nativeContent1 = Platform.CreateRenderer(content1);
                    _paned.Pack1(nativeContent1.Container, true, true);
                }
            }
        }

        private void UpdateContent2()
        {
            if (_paned != null)
            {
                var content2 = Element.Content2;

                if (content2 != null)
                {
                    var nativeContent2 = Platform.CreateRenderer(content2);
                    _paned.Pack2(nativeContent2.Container, true, true);
                }
            }
        }

        private void RecreateGridSplitter()
        {
            if (_paned != null)
            {
                this.RemoveFromContainer(_paned);
            }

            var orientation = Element.Orientation;

            switch (orientation)
            {
                case GridSplitterOrientation.Horizontal:
                    _paned = new HPaned();
                    _paned.CanFocus = true;
                    break;
                case GridSplitterOrientation.Vertical:
                    _paned = new VPaned();
                    _paned.CanFocus = true;
                    break;
            }

            Add(_paned);
            _paned.ShowAll();
        }
    }
}