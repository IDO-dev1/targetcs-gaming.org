using System.Drawing;
using System.Net.Http;
using TargetCS_Gaming.org.Models;

namespace TargetCS_Gaming.org.Services;

public class ThemeManager
{
    private readonly HttpClient _http;

    public ThemeManager()
    {
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        _http.DefaultRequestHeaders.UserAgent.ParseAdd("TargetCS-Gaming.org/1.0");
    }

    public async Task<Image?> DownloadBackgroundAsync(
        ThemeManifest theme,
        CancellationToken cancellationToken = default)
    {
        if (theme == null)
            return null;

        if (string.IsNullOrWhiteSpace(theme.BackgroundUrl))
            return null;

        try
        {
            var bytes = await _http.GetByteArrayAsync(
                theme.BackgroundUrl,
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
}
