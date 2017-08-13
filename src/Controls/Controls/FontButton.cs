using System;
using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class FontButton : ContentView
    {
        public string FontName
        {
            get { return (string)GetValue(FontNameProperty); }
            set { SetValue(FontNameProperty, value); }
        }

        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create(nameof(FontName), typeof(string), typeof(FontButton), string.Empty);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(FontButton), string.Empty);

        public bool ShowSize
        {
            get { return (bool)GetValue(ShowSizeProperty); }
            set { SetValue(ShowSizeProperty, value); }
        }

        public static readonly BindableProperty ShowSizeProperty =
            BindableProperty.Create(nameof(ShowSize), typeof(bool), typeof(FontButton), true);

        public bool ShowStyle
        {
            get { return (bool)GetValue(ShowStyleProperty); }
            set { SetValue(ShowStyleProperty, value); }
        }

        public static readonly BindableProperty ShowStyleProperty =
            BindableProperty.Create(nameof(ShowStyle), typeof(bool), typeof(FontButton), true);

        public event EventHandler FontChanged;

        public void SendFontChanged()
        {
            FontChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
