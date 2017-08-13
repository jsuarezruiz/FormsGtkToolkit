using System;
using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class ToggleButton : ContentView
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty TextProperty =  
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ToggleButton), string.Empty);

        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }

        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(ToggleButton), false);

        public event EventHandler IsToggledChanged;

        public void SendIsToggledChanged()
        {
            IsToggledChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}