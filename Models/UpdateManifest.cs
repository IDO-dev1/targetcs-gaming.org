namespace TargetCS_Gaming.org.Models;

public class UpdateManifest
{
    public string Version { get; set; } = "";
    public GameUpdate Cs16 { get; set; } = new();
    public GameUpdate Czero { get; set; } = new();
    public List<ServerEntry> Servers { get; set; } = new();
    public ThemeManifest Theme { get; set; } = new();
    public FastDlManifest FastDl { get; set; } = new();
    public StartupOptions Startup { get; set; } = new();
    public SelfUpdateManifest SelfUpdate { get; set; } = new();
}

public class GameUpdate
{
    public string GameMenuUrl { get; set; } = "";
}
