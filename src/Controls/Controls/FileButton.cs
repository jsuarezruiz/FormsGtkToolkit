using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public enum FileButtonAction
    {
        Open,
        Save,
        SelectFolder,
        CreateFolder
    };

    public class FileButton : ContentView
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(FileButton), string.Empty,
                BindingMode.TwoWay);

        public FileButtonAction FileAction
        {
            get { return (FileButtonAction)GetValue(FileActionProperty); }
            set { SetValue(FileActionProperty, value); }
        }

        public static readonly BindableProperty FileActionProperty =
            BindableProperty.Create("FileAction", typeof(FileButtonAction), typeof(FileButton), FileButtonAction.Open,
                BindingMode.TwoWay);

        public string CurrentFolder
        {
            get { return (string)GetValue(CurrentFolderProperty); }
            set { SetValue(CurrentFolderProperty, value); }
        }

        public static readonly BindableProperty CurrentFolderProperty =
            BindableProperty.Create("CurrentFolder", typeof(string), typeof(FileButton), string.Empty,
                BindingMode.TwoWay);

        public bool ShowHidden
        {
            get { return (bool)GetValue(ShowHiddenProperty); }
            set { SetValue(ShowHiddenProperty, value); }
        }

        public static readonly BindableProperty ShowHiddenProperty =
            BindableProperty.Create("ShowHidden", typeof(bool), typeof(FileButton), false,
                BindingMode.TwoWay);
    }
}