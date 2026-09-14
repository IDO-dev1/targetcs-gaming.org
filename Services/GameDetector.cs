using Microsoft.Win32;

namespace TargetCS_Gaming.org.Services;

public class GameDetector
{
    public string? DetectSteamRoot()
    {
        var candidates = new List<string>();

        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
            var steamPath = key?.GetValue("SteamPath")?.ToString();

            if (!string.IsNullOrWhiteSpace(steamPath))
                candidates.Add(steamPath);
        }
        catch
        {
        }

        candidates.Add(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
            "Steam"));

        candidates.Add(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "Steam"));

        foreach (var candidate in candidates.Distinct())
        {
            if (Directory.Exists(candidate))
                return candidate;
        }

        return null;
    }

    public string? DetectHalfLife()
    {
        var steam = DetectSteamRoot();
        if (steam == null)
            return null;

        foreach (var library in GetSteamLibraries(steam))
        {
            var path = Path.Combine(library, "steamapps", "common", "Half-Life");
            if (Directory.Exists(path))
                return path;
        }

        return null;
    }

    public List<string> GetSteamLibraries(string steamRoot)
    {
        var result = new List<string> { steamRoot };

        var libraryFile = Path.Combine(steamRoot, "steamapps", "libraryfolders.vdf");
        if (!File.Exists(libraryFile))
            return result;

        try
        {
            foreach (var rawLine in File.ReadAllLines(libraryFile))
            {
                var line = rawLine.Trim();
                if (!line.StartsWith("\"path\""))
                    continue;

                var parts = line.Split('"');
                if (parts.Length < 4)
                    continue;

                var path = parts[3].Replace("\\\\", "\\");

                if (Directory.Exists(path) &&
                    !result.Contains(path, StringComparer.OrdinalIgnoreCase))
                {
                    result.Add(path);
                }
            }
        }
        catch
        {
        }

        return result;
    }

    public string? FindServerBrowserFromHalfLife(string halfLifeRoot)
    {
        var candidates = new[]
        {
            Path.Combine(halfLifeRoot, "platform", "config", "ServerBrowser.vdf"),
            Path.Combine(halfLifeRoot, "config", "ServerBrowser.vdf"),
            Path.Combine(halfLifeRoot, "config", "rev_ServerBrowser.vdf")
        };

        return candidates.FirstOrDefault(File.Exists);
    }
}
