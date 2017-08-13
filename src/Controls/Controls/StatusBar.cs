using System;
using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class StatusBar : ContentView
    {
        public bool HasResizeGrip
        {
            get { return (bool)GetValue(HasResizeGripProperty); }
            set { SetValue(HasResizeGripProperty, value); }
        }

        public static readonly BindableProperty HasResizeGripProperty =
            BindableProperty.Create(nameof(HasResizeGrip), typeof(bool), typeof(StatusBar), false);

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(StatusBar), true);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(StatusBar), Color.Black);

        public event EventHandler<string> Pushed;

        public event EventHandler Popped;

        public void Push(string message)
        {
            Pushed?.Invoke(this, message);
        }

        public void Pop()
        {
            Popped?.Invoke(this, null);
        }
    }
}