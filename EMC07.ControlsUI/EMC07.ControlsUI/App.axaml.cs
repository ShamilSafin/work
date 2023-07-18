using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace EMC07.ControlsUI
{
    public partial class App : Application
    {
        private void AppStart(object sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
            View.MainView mainview = new();
            mainview.Show();
        }
    }
}