using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EMC07.ControlsUI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
