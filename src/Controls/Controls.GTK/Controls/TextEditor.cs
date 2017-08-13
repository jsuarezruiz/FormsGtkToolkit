using Gtk;
using System;
using System.Collections.Generic;

namespace FormsGtkToolkit.Controls.GTK.Controls
{
    public class TextEditor : EventBox
    {
        private VBox _root;
        private TextView _editor;
        private Toolbar _editToolbar;
        private ToolButton _copyBtn;
        private ToolButton _pasteBtn;
        private ToolButton _cutBtn;
        private ToolButton _undoBtn;
        private ToolButton _redoBtn;
        private Stack<string> _undoStack;
        private Stack<string> _redoStack;
        string _clipboard;

        public TextEditor()
        {
            BuildTextEditor();
        }

        public TextView TextView { get { return _editor; } }

        private void BuildTextEditor()
        {
            _root = new VBox();

            _editToolbar = new Toolbar();
            _copyBtn = new ToolButton(Stock.Copy);
            _copyBtn.TooltipText = "Copy";
            _pasteBtn = new ToolButton(Stock.Paste);
            _pasteBtn.TooltipText = "Paste";
            _cutBtn = new ToolButton(Stock.Cut);
            _cutBtn.TooltipText = "Cut";
            _undoBtn = new ToolButton(Stock.Undo);
            _undoBtn.TooltipText = "Undo";
            _redoBtn = new ToolButton(Stock.Redo);
            _redoBtn.TooltipText = "Redo";

            _editToolbar.Insert(_copyBtn, 0);
            _editToolbar.Insert(_pasteBtn, 1);
            _editToolbar.Insert(_cutBtn, 2);
            _editToolbar.Insert(_undoBtn, 3);
            _editToolbar.Insert(_redoBtn, 4);

            _undoBtn.Sensitive = false;
            _redoBtn.Sensitive = false;
            _pasteBtn.Sensitive = false;

            _editor = new TextView();
            _editor.Buffer.Clear();
            _editor.Buffer.UserActionBegun += OnUserActionBegun;

            _root.PackStart(_editToolbar, false, false, 0);
            _root.PackStart(_editor, true, true, 0);
            Add(_root);

            _undoStack = new Stack<string>();
            _redoStack = new Stack<string>();

            _undoBtn.Clicked += OnUndo;
            _redoBtn.Clicked += OnRedo;
            _copyBtn.Clicked += OnCopy;
            _cutBtn.Clicked += OnCut;
            _pasteBtn.Clicked += OnPaste;
        }

        public void UpdateText(string text)
        {
            _editor.Buffer.Text = text;
        }

        private void OnUserActionBegun(object sender, EventArgs args)
        {
            _undoStack.Push(_editor.Buffer.Text);

            if (_undoBtn.Sensitive == false)
                _undoBtn.Sensitive = true;
        }

        private void OnUndo(object sender, EventArgs args)
        {
            _redoStack.Push(_editor.Buffer.Text);

            if (_redoBtn.Sensitive == false)
                _redoBtn.Sensitive = true;

            if (_undoStack.Count > 0)
                _editor.Buffer.Text = _undoStack.Pop();

            if (_undoStack.Count == 0)
            {
                _undoBtn.Sensitive = false;
            }
        }

        private void OnRedo(object sender, EventArgs args)
        {
            _undoStack.Push(_editor.Buffer.Text);

            if (_undoBtn.Sensitive == false)
                _undoBtn.Sensitive = true;


            if (_redoStack.Count > 0)
                _editor.Buffer.Text = _redoStack.Pop();

            if (_redoStack.Count == 0)
            {
                _redoBtn.Sensitive = false;
            }
        }

        private void OnCopy(object sender, EventArgs args)
        {
            TextIter startIter;
            TextIter finishIter;
            if (_editor.Buffer.GetSelectionBounds(out startIter, out finishIter))
            {
                _clipboard = _editor.Buffer.GetText(startIter, finishIter, true);

                if (_pasteBtn.Sensitive == false)
                    _pasteBtn.Sensitive = true;
            }
        }

        private void OnCut(object sender, EventArgs args)
        {
            TextIter startIter;
            TextIter finishIter;

            if (_editor.Buffer.GetSelectionBounds(out startIter, out finishIter))
            {
                _clipboard = _editor.Buffer.GetText(startIter, finishIter, true);

                if (_pasteBtn.Sensitive == false)
                    _pasteBtn.Sensitive = true;
     
                _undoStack.Push(_editor.Buffer.Text);

                if (_undoBtn.Sensitive == false)
                    _undoBtn.Sensitive = true;

                _editor.Buffer.Delete(ref startIter, ref finishIter);
            }
        }

        private void OnPaste(object sender, EventArgs args)
        {
            _editor.Buffer.InsertAtCursor(_clipboard);
        }
    }
}