using System.Diagnostics;
using System.Security.AccessControl;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SimpleCalculator;

public partial class About : Window
{
    public About()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Button_GHOnClick(object? sender, RoutedEventArgs e)
    {
        var url = "https://github.com/ohshie/SimpleCalculator";
        var psi = new ProcessStartInfo()
        {
            UseShellExecute = true,
            FileName = url
        };
        Process.Start(psi);
    }
    private void Button_FlagOnClick(object? sender, RoutedEventArgs e)
    {
        var url = "https://savelife.in.ua";
        var psi = new ProcessStartInfo()
        {
            UseShellExecute = true,
            FileName = url
        };
        Process.Start(psi);
    }
}