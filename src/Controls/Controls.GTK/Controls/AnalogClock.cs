using System;
using Gtk;
using Gdk;
using FormsGtkToolkit.Controls.GTK.Helpers;

/// <summary>
/// AnalogClock.cs created with MonoDevelop
/// User: dantes at 6:50 PM 5/6/2008
/// </summary>
namespace FormsGtkToolkit.Controls.GTK.Controls
{
    internal class AnalogClock : DrawingArea
    {
        const float PI = 3.141592654F;

        private float _fRadius;
        private float _fCenterX;
        private float _fCenterY;
        private float _fHourLength;
        private float _fMinLength;
        private float _fSecLength;
        private double _fHourTickness = 3;
        private double _fMinTickness = 2;
        private double _fSecTickness = 0.9;
        private bool _bDraw5MinuteTicks = true;
        private bool _bDraw1MinuteTicks = true;
        private float _fTicksThickness = 1;

        Cairo.Color _hrColor = CairoUtil.ColorFromHexa("#3d3b33", 0.5);
        Cairo.Color _minColor = CairoUtil.ColorFromHexa("#3d3b33");
        Cairo.Color _secColor = CairoUtil.ColorFromHexa("#8f8a74");
        Cairo.Color _ticksColor = CairoUtil.ColorFromHexa("#8f8a74");

        DateTime _datetime;
        bool is_timer_started = false;

        public AnalogClock()
        {
            _datetime = DateTime.Now;
            QueueResize();
            GLib.Timeout.Add(1000, this.on_timer_tick);
        }

        public void Start()
        {
            is_timer_started = true;
        }

        public void Stop()
        {
            is_timer_started = false;
        }

        public DateTime Datetime
        {
            get
            {
                return _datetime;
            }
            set
            {
                _datetime = value;
                QueueDraw();
            }
        }

        private bool on_timer_tick()
        {
            if (is_timer_started)
            {
                _datetime = DateTime.Now;
                QueueDraw();
            }

            return true;
        }

        private void DrawCenterFilledCircle(Cairo.PointD center, double radius, Cairo.Context e)
        {
            e.Arc(center.X, center.Y, radius, 0, 360);
            Cairo.Gradient pat = new Cairo.LinearGradient(100, 200, 200, 100);
            pat.AddColorStop(0, CairoUtil.ColorFromRgb(240, 235, 229, 0.3));
            pat.AddColorStop(1, CairoUtil.ColorFromRgb(0, 0, 0, 0.2));
            e.LineWidth = 0.1;
            e.Pattern = pat;
            e.FillPreserve();
            e.Stroke();
        }

        private void DrawLine(double fThickness, float fLength, Cairo.Color color,
                              float fRadians, Cairo.Context e)
        {
            Cairo.PointD p1, p2;
            p1 = new Cairo.PointD(_fCenterX - (double)(fLength / 9 * Math.Sin(fRadians)), _fCenterY + (double)(fLength / 9 * System.Math.Cos(fRadians)));
            p2 = new Cairo.PointD(_fCenterX + (double)(fLength * Math.Sin(fRadians)), _fCenterY - (double)(fLength * System.Math.Cos(fRadians)));
            e.MoveTo(p1);
            e.LineTo(p2);
            e.ClosePath();
            e.LineCap = Cairo.LineCap.Round;
            e.LineJoin = Cairo.LineJoin.Round;
            e.Color = color;
            e.LineWidth = fThickness;
            e.Stroke();
        }

        protected override bool OnExposeEvent(EventExpose evnt)
        {
            base.OnExposeEvent(evnt);
            Cairo.Context e = CairoHelper.Create(evnt.Window);

            DateTime dateTime = _datetime;
            float fRadHr = (dateTime.Hour % 12 + dateTime.Minute / 60F) * 30 * PI / 180;
            float fRadMin = (dateTime.Minute) * 6 * PI / 180;
            float fRadSec = (dateTime.Second) * 6 * PI / 180;

            Cairo.PointD center = new Cairo.PointD(_fCenterX, _fCenterY);

            DrawLine(_fHourTickness, _fHourLength, _hrColor, fRadHr, e);
            DrawLine(_fMinTickness, _fMinLength, _minColor, fRadMin, e);
            DrawLine(_fSecTickness, _fSecLength, _secColor, fRadSec, e);

            for (int i = 0; i < 60; i++)
            {
                if (_bDraw5MinuteTicks == true && i % 5 == 0)
                {
                    e.LineWidth = _fTicksThickness;
                    e.Color = _ticksColor;
                    Cairo.PointD p1 = new Cairo.PointD(
                                      _fCenterX + (double)(_fRadius / 1.50F * System.Math.Sin(i * 6 * PI / 180)),
                                      _fCenterY - (double)(_fRadius / 1.50F * System.Math.Cos(i * 6 * PI / 180)));
                    Cairo.PointD p2 = new Cairo.PointD(
                                      _fCenterX + (double)(_fRadius / 1.65F * System.Math.Sin(i * 6 * PI / 180)),
                                      _fCenterY - (double)(_fRadius / 1.65F * System.Math.Cos(i * 6 * PI / 180)));

                    e.MoveTo(p1);
                    e.LineTo(p2);
                    e.ClosePath();
                    e.Stroke();
                }
                else if (_bDraw1MinuteTicks == true)
                {
                    e.LineWidth = _fTicksThickness;
                    e.Color = _ticksColor;
                    Cairo.PointD p1 = new Cairo.PointD
                    (
                        _fCenterX + (double)(_fRadius / 1.50F * System.Math.Sin(i * 6 * PI / 180)),
                        _fCenterY - (double)(_fRadius / 1.50F * System.Math.Cos(i * 6 * PI / 180))
                    );
                    Cairo.PointD p2 = new Cairo.PointD
                    (
                         _fCenterX + (double)(_fRadius / 1.55F * Math.Sin(i * 6 * PI / 180)),
                         _fCenterY - (double)(_fRadius / 1.55F * Math.Cos(i * 6 * PI / 180))
                    );
                    e.MoveTo(p1);
                    e.LineTo(p2);
                    e.ClosePath();
                    e.Stroke();
                }
            }

            DrawCenterFilledCircle(center, (_fRadius / 2) + 17, e);
            DrawCenterFilledCircle(center, 8, e);

            ((IDisposable)e.Target).Dispose();
            ((IDisposable)e).Dispose();

            return true;
        }

        protected override void OnSizeAllocated(Rectangle allocation)
        {
            base.OnSizeAllocated(allocation);
            _fRadius = allocation.Height / 2;
            _fCenterX = allocation.Width / 2;
            _fCenterY = allocation.Height / 2;
            _fHourLength = allocation.Height / 3 / 1.65F;
            _fMinLength = allocation.Height / 3 / 1.20F;
            _fSecLength = allocation.Height / 3 / 1.15F;
        }
    }
}