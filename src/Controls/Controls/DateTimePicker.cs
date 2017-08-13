using System;
using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class DateTimePicker : ContentView
    {
        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        public static readonly BindableProperty FormatProperty =
            BindableProperty.Create("Format", typeof(string), typeof(DateTimePicker), string.Empty,
                BindingMode.TwoWay);

        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }

        public static readonly BindableProperty DateTimeProperty =
            BindableProperty.Create("DateTime", typeof(DateTime), typeof(DateTimePicker), DateTime.Now,
                BindingMode.TwoWay);
    }
}