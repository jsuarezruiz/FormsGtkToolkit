using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using FormsGtkToolkit.Controls.Extensions;

[assembly: ExportRenderer(typeof(ScaleButton), typeof(ScaleButtonRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class ScaleButtonRenderer : ViewRenderer<ScaleButton, Gtk.ScaleButton>
    {
        private bool _disposed;
        private Gtk.ScaleButton _scaleButton;
        private double _minimum;
        private double _maximum;
        private double _step;
        private string[] _icons;

        protected override void OnElementChanged(ElementChangedEventArgs<ScaleButton> e)
        {
            if (Control == null)
            {
                _minimum = Element.Minimum;
                _maximum = Element.Maximum;
                _step = Element.StepIncrement;
                _icons = new string[1];

                _scaleButton = new Gtk.ScaleButton(
                    Gtk.IconSize.Button,
                    _minimum,
                    _maximum,
                    _step,
                    _icons);

                _scaleButton.ValueChanged += OnValueChanged;

                Add(_scaleButton);

                _scaleButton.ShowAll();

                SetNativeControl(_scaleButton);
            }

            if (e.NewElement != null)
            {
                UpdateIcon();
                UpdateMinimum();
                UpdateMaximum();
                UpdateStepIncrement();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ScaleButton.IconProperty.PropertyName)
                UpdateIcon();
            else if (e.PropertyName == ScaleButton.MaximumProperty.PropertyName)
                UpdateMaximum();
            else if (e.PropertyName == ScaleButton.MinimumProperty.PropertyName)
                UpdateMinimum();
            else if (e.PropertyName == ScaleButton.StepIncrementProperty.PropertyName)
                UpdateStepIncrement();
            else if (e.PropertyName == ScaleButton.ValueProperty.PropertyName)
                UpdateValue();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                if(Control != null)
                {
                    Control.ValueChanged -= OnValueChanged;
                }
            }

            base.Dispose(disposing);
        }

        private void UpdateIcon()
        {
            var icon = Element.Icon;
            _icons[0] = icon.GetStringValue();
            Control.Icons = _icons;
        }

        private void UpdateMaximum()
        {
            _maximum = Element.Maximum;

            Control.Adjustment.Lower = _minimum;
        }

        private void UpdateMinimum()
        {
            _minimum = Element.Minimum;

            Control.Adjustment.Upper = _maximum;
        }

        private void UpdateStepIncrement()
        {
            _step = Element.StepIncrement;

            Control.Adjustment.StepIncrement = _step;
        }

        private void UpdateValue()
        {
            var value = Element.Value;

            Control.Adjustment.Value = value;
        }

        private void OnValueChanged(object o, Gtk.ValueChangedArgs args)
        {
            var value = args.Value;

            Element.Value = args.Value;

            Element.SendValueChanged();
        }
    }
}