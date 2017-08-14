using Xamarin.Forms;

namespace FormsGtkToolkit.Samples.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            CalendarBtn.Clicked += (sender, args) => Navigation.PushAsync(new CalendarView());
            ColorButtonBtn.Clicked += (sender, args) => Navigation.PushAsync(new ColorButtonView());
            ColorPickerBtn.Clicked += (sender, args) => Navigation.PushAsync(new ColorPickerView());
            DataGridBtn.Clicked += (sender, args) => Navigation.PushAsync(new DataGridView());
            DateTimePickerBtn.Clicked += (sender, args) => Navigation.PushAsync(new DateTimePickerView());
            ExpanderBtn.Clicked += (sender, args) => Navigation.PushAsync(new ExpanderView());
            FileButtonBtn.Clicked += (sender, args) => Navigation.PushAsync(new FileButtonView());
            FontButtonBtn.Clicked += (sender, args) => Navigation.PushAsync(new FontButtonView());
            GridSplitterBtn.Clicked += (sender, args) => Navigation.PushAsync(new GridSplitterView());
            HyperLinkBtn.Clicked += (sender, args) => Navigation.PushAsync(new HyperLinkView());
            ImageCheckBoxBtn.Clicked += (sender, args) => Navigation.PushAsync(new ImageCheckBoxView());
            ScaleButtonBtn.Clicked += (sender, args) => Navigation.PushAsync(new ScaleButtonView());
            SeparatorBtn.Clicked += (sender, args) => Navigation.PushAsync(new SeparatorView());
            StatusBarBtn.Clicked += (sender, args) => Navigation.PushAsync(new StatusBarView());
            TextEditorBtn.Clicked += (sender, args) => Navigation.PushAsync(new TextEditorView());
            ToggleButtonBtn.Clicked += (sender, args) => Navigation.PushAsync(new ToggleButtonView());
            TreeViewBtn.Clicked += (sender, args) => Navigation.PushAsync(new TreeViewView());
        }
    }
}