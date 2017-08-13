using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

[assembly: ExportRenderer(typeof(Calendar), typeof(CalendarRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class CalendarRenderer : ViewRenderer<Calendar, Gtk.Calendar>
    {
        private bool _disposed;
        private Gtk.Calendar _calendar;

        protected override void OnElementChanged(ElementChangedEventArgs<Calendar> e)
        {
            if (Control == null)
            {
                _calendar = new Gtk.Calendar();

                _calendar.DaySelected += OnDaySelected;
                _calendar.NextMonth += OnNextMonth;
                _calendar.NextYear += OnNextYear;
                _calendar.PrevMonth += OnPrevMonth;
                _calendar.PrevYear += OnPrevYear;

                Add(_calendar);
                _calendar.ShowAll();

                SetNativeControl(_calendar);
            }

            if (e.NewElement != null)
            {
                UpdateDate();
                UpdateShowDayNames();
                UpdateShowHeading();
                UpdateShowWeekNumbers();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Calendar.DateProperty.PropertyName)
                UpdateDate();
            else if (e.PropertyName == Calendar.ShowDayNamesProperty.PropertyName)
                UpdateShowDayNames();
            else if (e.PropertyName == Calendar.ShowHeadingProperty.PropertyName)
                UpdateShowHeading();
            else if (e.PropertyName == Calendar.ShowWeekNumbersProperty.PropertyName)
                UpdateShowWeekNumbers();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                if (Control != null)
                {
                    Control.DaySelected -= OnDaySelected;
                    Control.NextMonth -= OnNextMonth;
                    Control.NextYear -= OnNextYear;
                    Control.PrevMonth -= OnPrevMonth;
                    Control.PrevYear -= OnPrevYear;
                }
            }

            base.Dispose(disposing);
        }

        private void UpdateDate()
        {
            if(_calendar != null)
            {
                _calendar.Date = Element.Date;
            }
        }

        private void UpdateShowDayNames()
        {
            if (_calendar != null)
            {
                _calendar.ShowDayNames = Element.ShowDayNames;
            }
        }

        private void UpdateShowHeading()
        {
            if (_calendar != null)
            {
                _calendar.ShowHeading = Element.ShowHeading;
            }
        }

        private void UpdateShowWeekNumbers()
        {
            if (_calendar != null)
            {
                _calendar.ShowWeekNumbers = Element.ShowWeekNumbers;
            }
        }

        private void OnDaySelected(object sender, System.EventArgs e)
        {
            Element.SendDaySelected();
        }

        private void OnNextMonth(object sender, System.EventArgs e)
        {
            Element.SendNextMonth();
        }

        private void OnNextYear(object sender, System.EventArgs e)
        {
            Element.SendNextYear();
        }

        private void OnPrevMonth(object sender, System.EventArgs e)
        {
            Element.SendPrevMonth();
        }

        private void OnPrevYear(object sender, System.EventArgs e)
        {
            Element.SendPrevYear();
        }
    }
}