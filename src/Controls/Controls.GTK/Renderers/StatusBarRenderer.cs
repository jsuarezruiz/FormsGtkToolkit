using System.ComponentModel;
using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Extensions;
using System.Collections.Generic;

[assembly: ExportRenderer(typeof(StatusBar), typeof(StatusBarRenderer))]
namespace FormsGtkToolkit.Controls.GTK.Renderers
{
    public class StatusBarRenderer : ViewRenderer<StatusBar, Gtk.EventBox>
    {
        private bool _disposed;
        private Gtk.Statusbar _statusBar;
        private Stack<uint> _stackContextId;

        protected override void OnElementChanged(ElementChangedEventArgs<StatusBar> e)
        {
            if (Control == null)
            {
                _stackContextId = new Stack<uint>();
                _statusBar = new Gtk.Statusbar();
                Add(_statusBar);
                _statusBar.ShowAll();

                SetNativeControl(this);
            }

            if (e.NewElement != null)
            {
                UpdateHasResizeGrip();
                UpdateTextColor();
                UpdateHasBorder();

                Element.Popped += OnPopped;
                Element.Pushed += OnPushed;
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == StatusBar.HasResizeGripProperty.PropertyName)
                UpdateHasResizeGrip();
            else if (e.PropertyName == StatusBar.TextColorProperty.PropertyName)
                UpdateTextColor();
            else if (e.PropertyName == StatusBar.HasBorderProperty.PropertyName)
                UpdateHasBorder();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;

                if (Control != null)
                {
                    Element.Pushed -= OnPushed;
                    Element.Popped -= OnPopped;
                }
            }

            base.Dispose(disposing);
        }

        private void UpdateHasResizeGrip()
        {
            if (_statusBar != null)
            {
                _statusBar.HasResizeGrip = Element.HasResizeGrip;
            }
        }

        private void UpdateTextColor()
        {
            if (_statusBar != null)
            {
                var textColor = Element.TextColor.ToGtkColor();
                _statusBar.ModifyFg(Gtk.StateType.Normal, textColor);
            }
        }

        private void UpdateHasBorder()
        {
            if (_statusBar != null)
            {
                var border = _statusBar.Children[0] as Gtk.Frame;

                if (border != null)
                {
                    var hasBorder = Element.HasBorder;

                    if (hasBorder)
                    {
                        border.ShadowType = Gtk.ShadowType.In;
                    }
                    else
                    {
                        border.ShadowType = Gtk.ShadowType.None;
                    }
                }
            }
        }

        private void OnPushed(object sender, string message)
        {
            if (_statusBar != null)
            {
                var contextId = _statusBar.GetContextId(message);
                _statusBar.Push(contextId, message);
                _stackContextId.Push(contextId);
            }
        }

        private void OnPopped(object sender, System.EventArgs e)
        {
            if (_statusBar != null)
            {
                if (_stackContextId.Count == 0)
                    return;

                var contextId = _stackContextId.Pop();
                _statusBar.Pop(contextId);
            }
        }
    }
}