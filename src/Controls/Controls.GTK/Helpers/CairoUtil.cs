using System;
using Cairo;

namespace FormsGtkToolkit.Controls.GTK.Helpers
{
    /// <summary>
    /// CairoUtil.cs created with MonoDevelop
    /// User: dantes at 1:07 PM 5/7/2008
    /// </summary>
    public class CairoUtil
    {
        public static Color ColorFromHexa(string str, double alpha)
        {
            Gdk.Color c = new Gdk.Color();
            Gdk.Color.Parse(str, ref c);
            return ColorFromRgb(c.Red / 255, c.Green / 255, c.Blue / 255, alpha);
        }

        public static Color ColorFromHexa(string str)
        {
            return ColorFromHexa(str, 1);
        }

        public static Color ColorFromRgb(int r, int g, int b)
        {
            return ColorFromRgb(r, g, b, 1);
        }

        public static Color ColorFromRgb(int r, int g, int b, double alpha)
        {
            double _r = Math.Round((double)r / 255, 2);
            double _g = Math.Round((double)g / 255, 2);
            double _b = Math.Round((double)b / 255, 2);

            return new Color(_r, _g, _b, alpha);
        }
    }
}
