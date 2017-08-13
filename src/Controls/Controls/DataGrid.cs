using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FormsGtkToolkit.Controls
{
    public enum DataGridSelectionMode
    {
        None,
        Single,
        Multiple
    };

    public class DataGridColumn : BindableObject
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(DataGridColumn), string.Empty);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty ColumnWidthProperty =
            BindableProperty.Create(nameof(ColumnWidth), typeof(int), typeof(DataGridColumn), default(int));

        public int ColumnWidth
        {
            get { return (int)GetValue(ColumnWidthProperty); }
            set { SetValue(ColumnWidthProperty, value); }
        }

        public static readonly BindableProperty ResizableProperty =
            BindableProperty.Create(nameof(Resizable), typeof(bool), typeof(DataGridColumn), true);

        public bool Resizable
        {
            get { return (bool)GetValue(ResizableProperty); }
            set { SetValue(ResizableProperty, value); }
        }
    }

    public sealed class ColumnCollection : List<DataGridColumn>
    {

    }

    public class DataGrid : ContentView
    {
        public static readonly BindableProperty ColumnsProperty =
            BindableProperty.Create(nameof(Columns), typeof(ColumnCollection), typeof(DataGrid),
                defaultValueCreator: bindable => { return new ColumnCollection(); });

        public ColumnCollection Columns
        {
            get { return (ColumnCollection)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(DataGrid), null);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty RowHeightProperty =
            BindableProperty.Create(nameof(RowHeight), typeof(int), typeof(DataGrid), default(int));

        public int RowHeight
        {
            get { return (int)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }

        public static readonly BindableProperty EnableGridLinesProperty =
            BindableProperty.Create(nameof(RowHeight), typeof(bool), typeof(DataGrid), true);

        public bool EnableGridLines
        {
            get { return (bool)GetValue(EnableGridLinesProperty); }
            set { SetValue(EnableGridLinesProperty, value); }
        }

        public static readonly BindableProperty CellBackgroundColorProperty =
            BindableProperty.Create(nameof(CellBackgroundColor), typeof(Color), typeof(DataGrid), Color.White);

        public Color CellBackgroundColor
        {
            get { return (Color)GetValue(CellBackgroundColorProperty); }
            set { SetValue(CellBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty CellTextColorProperty =
            BindableProperty.Create(nameof(CellTextColor), typeof(Color), typeof(DataGrid), Color.Black);

        public Color CellTextColor
        {
            get { return (Color)GetValue(CellTextColorProperty); }
            set { SetValue(CellTextColorProperty, value); }
        }

        public static readonly BindableProperty SelectionModeProperty =
            BindableProperty.Create(nameof(SelectionMode), typeof(DataGridSelectionMode), typeof(DataGrid), 
                DataGridSelectionMode.Single);

        public DataGridSelectionMode SelectionMode
        {
            get { return (DataGridSelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }
    }
}