using System.Drawing;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using TargetCS_Gaming.org.Models;

namespace TargetCS_Gaming.org.Services;

public class ThemeManager
{
    private readonly HttpClient _http;

    private static readonly string ThemeCacheDirectory =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TargetCS-Gaming.org",
            "Themes");

    public ThemeManager()
    {
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        _http.DefaultRequestHeaders.UserAgent.ParseAdd("TargetCS-Gaming.org/1.0");
    }

    public async Task<Image?> GetThemeBackgroundAsync(
        ThemeManifest theme,
        CancellationToken cancellationToken = default)
    {
        if (theme == null)
            return null;

        if (string.IsNullOrWhiteSpace(theme.BackgroundUrl))
            return null;

        Directory.CreateDirectory(ThemeCacheDirectory);

        var cacheFile = GetCacheFilePath(theme);

        if (File.Exists(cacheFile))
        {
            try
            {
                using var stream = File.OpenRead(cacheFile);
                return Image.FromStream(stream);
            }
            catch
            {
                File.Delete(cacheFile);
            }
        }

        try
        {
            var bytes = await _http.GetByteArrayAsync(
                theme.BackgroundUrl,
                cancellationToken);

            await File.WriteAllBytesAsync(
                cacheFile,
                bytes,
                cancellationToken);

            using var stream = new MemoryStream(bytes);
            return Image.FromStream(stream);
        }
        catch
        {
            return null;
        }
    }

    public Color ParseAccentColor(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Color.White;

        try
        {
            return ColorTranslator.FromHtml(value);
        }
        catch
        {
            return Color.White;
        }
    }

    private string GetCacheFilePath(ThemeManifest theme)
    {
        var key = $"{theme.Name}|{theme.BackgroundUrl}";
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(key));
        var hash = Convert.ToHexString(hashBytes).ToLowerInvariant();

        var extension = Path.GetExtension(
            new Uri(theme.BackgroundUrl).AbsolutePath);

        if (string.IsNullOrWhiteSpace(extension))
            extension = ".png";

        return Path.Combine(
            ThemeCacheDirectory,
            $"{theme.Name}_{hash}{extension}");
    }
}
