using Gdk;
using Gtk;
using System;

namespace FormsGtkToolkit.Controls.GTK.Controls
{
    public class ImageCheckBox : EventBox
    {
        public event EventHandler CheckedStateChanged;

        private Gtk.Image _imgCheck;
        private Pixbuf _checkedImage;
        private Pixbuf _uncheckedImage;
        private bool _checked;

        public ImageCheckBox()
        {
            BuildImageCheckBox();

            ButtonPressEvent += new ButtonPressEventHandler(OnButtonPress);
        }

        public Pixbuf CheckedImage
        {
            get
            {
                return _checkedImage;
            }
            set
            {
                _checkedImage = value;
                OnCheckedStateChanged();
            }
        }

        public Pixbuf UncheckedImage
        {
            get
            {
                return _uncheckedImage;
            }
            set
            {
                _uncheckedImage = value;
                OnCheckedStateChanged();
            }
        }

        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                OnCheckedStateChanged();
            }
        }

        private void BuildImageCheckBox()
        {
            CanFocus = true;

            _imgCheck = new Gtk.Image();
            Add(_imgCheck);

            ShowAll();
        }

        private void OnButtonPress(object sender, EventArgs args)
        {
            Checked = !Checked;
        }

        private void OnCheckedStateChanged()
        {
            if (Checked)
                _imgCheck.Pixbuf = CheckedImage;
            else
                _imgCheck.Pixbuf = UncheckedImage;

            CheckedStateChanged?.Invoke(this, new EventArgs());
        }
    }
}