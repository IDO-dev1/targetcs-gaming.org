namespace TargetCS_Gaming.org.Models;

public class SelfUpdateManifest
{
    public bool Enabled { get; set; }
    public string LatestVersion { get; set; } = "";
    public string DownloadUrl { get; set; } = "";
    public string ReleaseNotes { get; set; } = "";
}
