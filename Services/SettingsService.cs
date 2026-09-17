using System.Text.Json;
using TargetCS_Gaming.org.Models;

namespace TargetCS_Gaming.org.Services;

public class SettingsService
{
    private static readonly string SettingsDirectory =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TargetCS-Gaming.org");

    private static readonly string SettingsPath =
        Path.Combine(SettingsDirectory, "settings.json");

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public AppSettings Load()
    {
        try
        {
            if (!File.Exists(SettingsPath))
                return new AppSettings();

            var json = File.ReadAllText(SettingsPath);
            var settings = JsonSerializer.Deserialize<AppSettings>(json);

            return settings ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }

    public void Save(AppSettings settings)
    {
        Directory.CreateDirectory(SettingsDirectory);

        var json = JsonSerializer.Serialize(settings, _options);
        File.WriteAllText(SettingsPath, json);
    }
}
