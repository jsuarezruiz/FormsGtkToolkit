using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using System.Linq;
using System;
using Xamarin.Forms.Platform.GTK.Extensions;

[assembly: ExportRenderer(typeof(TreeView), typeof(TreeViewRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class TreeViewRenderer : ViewRenderer<TreeView, Gtk.TreeView>
    {
        private const int DefaultRowHeight = 24;

        private bool _disposed;
        private Gtk.TreeView _treeView;
        private Gtk.TreeStore _treeStore;

        protected override void OnElementChanged(ElementChangedEventArgs<TreeView> e)
        {
            if (Control == null)
            {
                _treeView = new Gtk.TreeView();
                _treeView.HeadersVisible = false;

                Gtk.TreeViewColumn column = new Gtk.TreeViewColumn();
                Gtk.CellRendererText cell = new Gtk.CellRendererText();
                column.PackStart(cell, true);
                column.AddAttribute(cell, "text", 0);
                column.SetCellDataFunc(cell, new Gtk.TreeCellDataFunc(OnTextFunc));

                _treeView.AppendColumn(column);

                _treeView.RowExpanded += OnRowExpanded;
                _treeView.RowCollapsed += OnRowCollapsed;
                _treeView.Selection.Changed += OnSelectionChanged;

                Add(_treeView);
                _treeView.ShowAll();

                SetNativeControl(_treeView);
            }

            if (e.NewElement != null)
            {
                UpdateItems();
                UpdateRowHeight();
                UpdateRowTextColor();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TreeView.ItemsSourceProperty.PropertyName)
                UpdateItems();
            else if (e.PropertyName == TreeView.RowHeightProperty.PropertyName)
                UpdateRowHeight();
            else if (e.PropertyName == TreeView.RowTextColorProperty.PropertyName)
                UpdateRowTextColor();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                if (Control != null)
                {
                    Control.RowExpanded -= OnRowExpanded;
                    Control.RowCollapsed -= OnRowCollapsed;
                    Control.Selection.Changed -= OnSelectionChanged;
                }
            }

            base.Dispose(disposing);
        }

        private void UpdateItems()
        {
            if (_treeView != null)
            {
                var items = Element.ItemsSource;

                if (items == null)
                {
                    return;
                }

                _treeStore = new Gtk.TreeStore(typeof(Node));

                foreach (var item in items)
                {
                    AddNodes(item);
                }

                _treeView.Model = _treeStore;
                _treeView.ShowAll();
            }
        }


        private void AddNodes(Node node, Gtk.TreeIter iter = new Gtk.TreeIter())
        {
            if (iter.Stamp == 0)
                iter = _treeStore.AppendValues(node);

            foreach (var child in node.Children)
            {
                _treeStore.AppendValues(iter, child);

                if (child.Children.Any())
                {
                    AddNodes(child, iter);
                }
            }
        }

        private void UpdateRowHeight()
        {
            var rowHeight = Element.RowHeight;

            var column = _treeView.Columns.FirstOrDefault();

            if (column != null)
            {
                var cell = column.Cells.FirstOrDefault();

                if (cell != null)
                {
                    cell.Height = rowHeight > 0 ? rowHeight : DefaultRowHeight;
                }
            }
        }

        private void UpdateRowTextColor()
        {
            if (_treeView == null)
            {
                return;
            }

            if (Element.RowTextColor.IsDefault)
            {
                return;
            }

            var textColor = Element.RowTextColor.ToGtkColor();

            foreach (var column in _treeView.Columns)
            {
                foreach (var cell in column.Cells)
                {
                    var cellText = cell as Gtk.CellRendererText;

                    if (cellText != null)
                    {
                        cellText.ForegroundGdk = textColor;
                    }
                }
            }
        }

        private void OnTextFunc(
                        Gtk.TreeViewColumn col, Gtk.CellRenderer cell,
                        Gtk.TreeModel model, Gtk.TreeIter iter)
        {
            Node node = GetNodeFromIter(iter);
            Gtk.CellRendererText c = cell as Gtk.CellRendererText;
            c.Text = node.Name;
        }

        public Node GetNodeFromIter(Gtk.TreeIter iter)
        {
            Node ret = null;

            try
            {
                ret = _treeStore.GetValue(iter, 0) as Node;
            }
            catch
            {
                ret = null;
            }

            return ret;
        }

        private void OnRowExpanded(object o, Gtk.RowExpandedArgs args)
        {
            var iter = args.Iter;
            var node = _treeStore.GetValue(iter, 0) as Node;

            if(node != null)
            {
                Element.SendRowExpanded(node);
            }
        }

        private void OnRowCollapsed(object o, Gtk.RowCollapsedArgs args)
        {
            var iter = args.Iter;
            var node = _treeStore.GetValue(iter, 0) as Node;

            if (node != null)
            {
                Element.SendRowCollapsed(node);
            }
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            Gtk.TreeIter selected;

            if (_treeView.Selection.GetSelected(out selected))
            {
                var node = _treeStore.GetValue(selected, 0) as Node;

                if (node != null)
                {
                    Element.SendRowSelected(node);
                }
            }
        }
    }
}