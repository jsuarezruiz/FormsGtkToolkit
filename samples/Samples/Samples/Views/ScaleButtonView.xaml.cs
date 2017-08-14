using Xamarin.Forms;

namespace FormsGtkToolkit.Samples.Views
{
    public partial class ScaleButtonView : ContentPage
    {
        public ScaleButtonView()
        {
            InitializeComponent();

            ScaleBtn.ValueChanged += (sender, args) =>
            {
                LabelInfo.Text = 
                string.Format("Value: {0}", ScaleBtn.Value);
            };
        }
    }
}