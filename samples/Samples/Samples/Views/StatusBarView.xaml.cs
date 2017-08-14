using Xamarin.Forms;

namespace FormsGtkToolkit.Samples.Views
{
    public partial class StatusBarView : ContentPage
    {
        private int _counter;

        public StatusBarView()
        {
            InitializeComponent();

            PushBtn.Clicked += (sender, args) =>
            {
                StatusBar.Push(string.Format("Message {0}", _counter + 1));
                _counter++;
            };

            PopBtn.Clicked += (sender, args) =>
            {
                StatusBar.Pop();
            };
        }
    }
}