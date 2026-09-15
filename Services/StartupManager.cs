using Microsoft.Win32;

namespace TargetCS_Gaming.org.Services;

public class StartupManager
{
    private const string RunKeyPath =
        @"Software\Microsoft\Windows\CurrentVersion\Run";

    private const string AppName =
        "TargetCS-Gaming.org";

    public void EnableStartup(string executablePath)
    {
        using var key = Registry.CurrentUser.OpenSubKey(
            RunKeyPath,
            writable: true);

        key?.SetValue(
            AppName,
            $"\"{executablePath}\" --startup --silent --exit-after-update");
    }

    public void DisableStartup()
    {
        using var key = Registry.CurrentUser.OpenSubKey(
            RunKeyPath,
            writable: true);

        key?.DeleteValue(AppName, throwOnMissingValue: false);
    }

    public bool IsStartupEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(
            RunKeyPath);

        return key?.GetValue(AppName) != null;
    }
}
