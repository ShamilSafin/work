using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace EMC07.ControlsUI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Startup += AppStart;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void AppStart(object sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
            Views.MainWindow mainView = new Views.MainWindow();
            mainView.Show();
        }
    }
}