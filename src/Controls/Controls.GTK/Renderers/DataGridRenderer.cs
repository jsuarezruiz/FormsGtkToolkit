using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Gtk;
using System.Linq;
using System;
using System.Collections;
using System.Reflection;
using Xamarin.Forms.Platform.GTK.Extensions;

[assembly: ExportRenderer(typeof(DataGrid), typeof(DataGridRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class DataGridRenderer : ViewRenderer<DataGrid, Gtk.TreeView>
    {
        private const int DefaultRowHeight = 40;

        private bool _disposed;
        private Gtk.TreeView _treeView;
        private ListStore _model;

        protected override void OnElementChanged(ElementChangedEventArgs<DataGrid> e)
        {
            if (Control == null)
            {
                _treeView = new Gtk.TreeView();

                _treeView.Selection.Changed += OnSelectionChanged;

                SetNativeControl(_treeView);
            }

            if (e.NewElement != null)
            {
                UpdateColumns();
                UpdateItems();
                UpdateRowHeight();
                UpdateGridLines();
                UpdateCellBackgroundColor();
                UpdateCellTextColor();
                UpdateSelectionMode();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == DataGrid.ColumnsProperty.PropertyName)
                UpdateColumns();
            else if (e.PropertyName == DataGrid.ItemsSourceProperty.PropertyName)
                UpdateItems();
            else if (e.PropertyName == DataGrid.RowHeightProperty.PropertyName)
                UpdateRowHeight();
            else if (e.PropertyName == DataGrid.EnableGridLinesProperty.PropertyName)
                UpdateGridLines();
            else if (e.PropertyName == DataGrid.CellBackgroundColorProperty.PropertyName)
                UpdateCellBackgroundColor();
            else if (e.PropertyName == DataGrid.CellTextColorProperty.PropertyName)
                UpdateCellTextColor();
            else if (e.PropertyName == DataGrid.SelectionModeProperty.PropertyName)
                UpdateSelectionMode();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                if (Control != null)
                {
                    Control.Selection.Changed -= OnSelectionChanged;
                }
            }

            base.Dispose(disposing);
        }

        private void UpdateColumns()
        {
            if (_treeView != null)
            {
                var columns = Element.Columns;

                int index = 0;
                foreach (var col in columns)
                {
                    TreeViewColumn column = new TreeViewColumn();
                    column.Title = col.Title;

                    if (col.ColumnWidth > 0)
                    {
                        column.MinWidth = col.ColumnWidth;
                    }

                    column.Resizable = col.Resizable;

                    CellRendererText cell = new CellRendererText();
                    column.PackStart(cell, true);

                    _treeView.AppendColumn(column);

                    column.AddAttribute(cell, "text", index);

                    index++;
                }
            }
        }

        private void UpdateItems()
        {
            if (_treeView != null)
            {
                var items = Element.ItemsSource;

                _model = CreateItems(items);
                _treeView.Model = _model;

                _treeView.ShowAll();
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

        private void UpdateGridLines()
        {
            if (Element.EnableGridLines)
                _treeView.EnableGridLines = TreeViewGridLines.Both;
            else
                _treeView.EnableGridLines = TreeViewGridLines.None;
        }

        private void UpdateCellBackgroundColor()
        {
            if (_treeView == null)
            {
                return;
            }

            if (Element.CellBackgroundColor.IsDefault)
            {
                return;
            }

            var backgroundColor = Element.CellBackgroundColor.ToGtkColor();

            foreach (var column in _treeView.Columns)
            {
                foreach (var cell in column.Cells)
                {
                    cell.CellBackgroundGdk = backgroundColor;
                }
            }
        }

        private void UpdateCellTextColor()
        {
            if (_treeView == null)
            {
                return;
            }

            if (Element.CellTextColor.IsDefault)
            {
                return;
            }

            var textColor = Element.CellTextColor.ToGtkColor();

            foreach (var column in _treeView.Columns)
            {
                foreach (var cell in column.Cells)
                {
                    var cellText = cell as CellRendererText;

                    if (cellText != null)
                    {
                        cellText.ForegroundGdk = textColor;
                    }
                }
            }
        }

        private void UpdateSelectionMode()
        {
            if (_treeView == null)
            {
                return;
            }

            var selectionMode = Element.SelectionMode;

            switch (selectionMode)
            {
                case DataGridSelectionMode.None:
                    _treeView.Selection.Mode = SelectionMode.None;
                    break;
                case DataGridSelectionMode.Single:
                    _treeView.Selection.Mode = SelectionMode.Single;
                    break;
                case DataGridSelectionMode.Multiple:
                    _treeView.Selection.Mode = SelectionMode.Multiple;
                    break;
            }
        }

        private int GetPropertiesCount(IEnumerable items)
        {
            var enumerator = items.GetEnumerator();
            enumerator.MoveNext();
            var current = enumerator.Current;
            Type cellType = current.GetType();
            var cellProperties = cellType.GetProperties();

            return cellProperties.Count();
        }

        private Type[] CreateListStore(IEnumerable items)
        {
            var enumerator = items.GetEnumerator();
            enumerator.MoveNext();
            var current = enumerator.Current;
            Type cellType = current.GetType();
            var cellProperties = cellType.GetProperties();
            Type[] types = new Type[GetPropertiesCount(items)];

            int index = 0;
            foreach (var prop in cellProperties)
            {
                var propType = prop.PropertyType;
                types[index] = propType;
                index++;
            }

            return types;
        }

        private ListStore CreateItems(IEnumerable items)
        {
            var types = CreateListStore(items);
            ListStore listStore = new ListStore(types);

            int count = GetPropertiesCount(items);

            foreach (var item in items)
            {
                object[] arrayItems = new object[count];
                Type type = item.GetType();
                PropertyInfo[] properties = type.GetProperties();

                int index = 0;
                foreach (PropertyInfo p in properties)
                {
                    if (p.GetValue(item, null) != null)
                    {
                        arrayItems[index] = p.GetValue(item, null);

                        index++;
                    }
                }

                listStore.AppendValues(arrayItems);
            }

            return listStore;
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            TreeIter selected;

            if (_treeView.Selection.GetSelected(out selected))
            {
                Console.WriteLine("SELECTED ITEM: {0}", _model.GetValue(selected, 0));
            }
        }
    }
}