using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public class Node
    {
        public Node()
        {
            Children = new ObservableCollection<Node>();
        }

        public string Name { get; set; }

        public ObservableCollection<Node> Children { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class NodeEventArgs : EventArgs
    {
        private Node _node;

        public NodeEventArgs(Node node)
        {
            _node = node;
        }

        public Node Node
        {
            get
            {
                return _node;
            }
            set
            {
                _node = value;
            }
        }
    }

    public class TreeView : ContentView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(ObservableCollection<Node>), typeof(TreeView), null);

        public ObservableCollection<Node> ItemsSource
        {
            get { return (ObservableCollection<Node>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty RowHeightProperty =
            BindableProperty.Create(nameof(RowHeight), typeof(int), typeof(TreeView), default(int));

        public int RowHeight
        {
            get { return (int)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }

        public Color RowTextColor
        {
            get { return (Color)GetValue(RowTextColorProperty); }
            set { SetValue(RowTextColorProperty, value); }
        }

        public static readonly BindableProperty RowTextColorProperty =
            BindableProperty.Create(nameof(RowTextColor), typeof(Color), typeof(TreeView),
                Color.Black);

        public event EventHandler RowExpanded;
        public event EventHandler RowCollapsed;
        public event EventHandler RowSelected;

        public void SendRowExpanded(Node node)
        {
            RowExpanded?.Invoke(this, new NodeEventArgs(node));
        }

        public void SendRowCollapsed(Node node)
        {
            RowCollapsed?.Invoke(this, new NodeEventArgs(node));
        }

        public void SendRowSelected(Node node)
        {
            RowSelected?.Invoke(this, new NodeEventArgs(node));
        }
    }
}