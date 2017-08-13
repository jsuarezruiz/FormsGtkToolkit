using System;
using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class Calendar : ContentView
    {
        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create("Date", typeof(DateTime), typeof(Calendar), default(DateTime),
                BindingMode.TwoWay);

        public bool ShowHeading
        {
            get { return (bool)GetValue(ShowHeadingProperty); }
            set { SetValue(ShowHeadingProperty, value); }
        }

        public static readonly BindableProperty ShowHeadingProperty =
            BindableProperty.Create("ShowHeading", typeof(bool), typeof(Calendar),true,
                BindingMode.TwoWay);

        public bool ShowDayNames
        {
            get { return (bool)GetValue(ShowDayNamesProperty); }
            set { SetValue(ShowDayNamesProperty, value); }
        }

        public static readonly BindableProperty ShowDayNamesProperty =
            BindableProperty.Create("ShowDayNames", typeof(bool), typeof(Calendar), true,
                BindingMode.TwoWay);

        public bool ShowWeekNumbers
        {
            get { return (bool)GetValue(ShowWeekNumbersProperty); }
            set { SetValue(ShowWeekNumbersProperty, value); }
        }

        public static readonly BindableProperty ShowWeekNumbersProperty =
            BindableProperty.Create("ShowWeekNumbers", typeof(bool), typeof(Calendar), true,
                BindingMode.TwoWay);

        public event EventHandler DaySelected;
        public event EventHandler NextMonth;
        public event EventHandler NextYear;
        public event EventHandler PrevMonth;
        public event EventHandler PrevYear;

        public void SendDaySelected()
        {
            DaySelected?.Invoke(this, EventArgs.Empty);
        }

        public void SendNextMonth()
        {
            NextMonth?.Invoke(this, EventArgs.Empty);
        }

        public void SendNextYear()
        {
            NextYear?.Invoke(this, EventArgs.Empty);
        }

        public void SendPrevMonth()
        {
            PrevMonth?.Invoke(this, EventArgs.Empty);
        }

        public void SendPrevYear()
        {
            PrevYear?.Invoke(this, EventArgs.Empty);
        }
    }
}