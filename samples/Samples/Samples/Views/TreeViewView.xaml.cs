using Xamarin.Forms;
using FormsGtkToolkit.Controls;
using System.Collections.ObjectModel;

namespace FormsGtkToolkit.Samples.Views
{
    public partial class TreeViewView : ContentPage
    {
        public TreeViewView()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            ObservableCollection<Node> items = new ObservableCollection<Node>();
            var node1 = new Node { Name = "element1" };
            var node2 = new Node { Name = "element2" };

            for (int i = 1; i <= 50; i++)
            {
                node2.Children.Add(new Node { Name = "element2_" + i });
            }

            var node3 = new Node { Name = "element3" };
            var node31 = new Node { Name = "element3_1" };

            node3.Children.Add(node31);

            items.Add(node1);
            items.Add(node2);
            items.Add(node3);

            TreeView.ItemsSource = items;
        }
    }
}