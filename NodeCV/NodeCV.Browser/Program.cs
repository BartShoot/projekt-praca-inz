using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using NodeCV;
using System.Runtime.Versioning;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program
{
    private static void Main(string[] args) => BuildAvaloniaApp()
        .UseReactiveUI()
        .SetupBrowserAppAsync(new BrowserPlatformOptions());

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}