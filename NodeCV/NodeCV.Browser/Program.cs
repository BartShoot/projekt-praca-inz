using Avalonia;
using Avalonia.Browser;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using NodeCV;
using System.Runtime.Versioning;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program
{
    private static void Main(string[] args) => BuildAvaloniaApp()
        .UseReactiveUI()
        .SetupBrowserAppAsync();

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}