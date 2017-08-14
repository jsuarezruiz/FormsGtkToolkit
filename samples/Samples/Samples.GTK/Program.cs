using FormsGtkToolkit.Controls;
using FormsGtkToolkit.Controls.GTK.Renderers;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace FormsGtkToolkit.Samples.GTK
{
    class Program
    {
        static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init(new List<Assembly>
            {
                typeof(GridSplitter).Assembly,
                typeof(GridSplitterRenderer).Assembly
            });
            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("FormsGtkToolkit Samples");
            window.Show();
            Gtk.Application.Run();
        }
    }
}