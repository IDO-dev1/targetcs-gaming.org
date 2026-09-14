namespace TargetCS_Gaming.org.Models;

public class UpdateManifest
{
    public string Version { get; set; } = "";
    public GameUpdate Cs16 { get; set; } = new();
    public GameUpdate Czero { get; set; } = new();
    public List<ServerEntry> Servers { get; set; } = new();
}

public class GameUpdate
{
    public string GameMenuUrl { get; set; } = "";
}
