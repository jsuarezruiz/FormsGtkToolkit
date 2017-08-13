using FormsGtkToolkit.Controls.Attributes;
using System;
using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public enum ScaleButtonIcon
    {
        [StringValue("about")]
        About,
        [StringValue("add")]
        Add,
        [StringValue("cancel")]
        Cancel,
        [StringValue("delete")]
        Delete,
        [StringValue("edit")]
        Edit,
        [StringValue("file")]
        File,
        [StringValue("find")]
        Find,
        [StringValue("goto-top")]
        GoTop,
        [StringValue("go-down")]
        GoDown,
        [StringValue("go-up")]
        GoUp,
        [StringValue("help")]
        Help,
        [StringValue("home")]
        Home,
        [StringValue("info")]
        Info,
        [StringValue("missing-image")]
        Missing,
        [StringValue("network")]
        Network,
        [StringValue("no")]
        No,
        [StringValue("ok")]
        Ok,
        [StringValue("open")]
        Open,
        [StringValue("preferences")]
        Preferences,
        [StringValue("sort-ascending")]
        SortAsc,
        [StringValue("sort-descending")]
        SortDesc,
        [StringValue("yes")]
        Yes,
        [StringValue("zoom-in")]
        ZoomIn,
        [StringValue("zoom-out")]
        ZoomOut
    }

    public class ScaleButton : ContentView
    {
        public ScaleButtonIcon Icon
        {
            get { return (ScaleButtonIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create("Icon", typeof(ScaleButtonIcon), typeof(ScaleButton), ScaleButtonIcon.ZoomIn,
                BindingMode.TwoWay);

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value", typeof(double), typeof(ScaleButton), 0.0d,
                BindingMode.TwoWay);

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly BindableProperty MinimumProperty =
            BindableProperty.Create("Minimum", typeof(double), typeof(ScaleButton), 0.0d,
                BindingMode.TwoWay);

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly BindableProperty MaximumProperty =
            BindableProperty.Create("Maximum", typeof(double), typeof(ScaleButton), 100.0d,
                BindingMode.TwoWay);

        public double StepIncrement
        {
            get { return (double)GetValue(StepIncrementProperty); }
            set { SetValue(StepIncrementProperty, value); }
        }

        public static readonly BindableProperty StepIncrementProperty =
            BindableProperty.Create("StepIncrement", typeof(double), typeof(ScaleButton), 1.0d,
                BindingMode.TwoWay);

        public event EventHandler ValueChanged;

        public void SendValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}