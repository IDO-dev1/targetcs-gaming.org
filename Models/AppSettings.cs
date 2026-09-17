namespace TargetCS_Gaming.org.Models;

public class AppSettings
{
    public string LastManifestVersion { get; set; } = "";
    public string LastThemeName { get; set; } = "";
    public string LastSelfUpdateVersion { get; set; } = "";
    public bool AutoStartEnabled { get; set; }
    public bool SilentMode { get; set; }
    public bool ExitAfterUpdate { get; set; }
}
