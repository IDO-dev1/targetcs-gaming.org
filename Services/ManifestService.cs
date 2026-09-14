using System.Text.Json;
using TargetCS_Gaming.org.Models;

namespace TargetCS_Gaming.org.Services;

public class ManifestService
{
    private readonly HttpClient _http;

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ManifestService()
    {
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        _http.DefaultRequestHeaders.UserAgent.ParseAdd("TargetCS-Gaming.org/1.0");
    }

    public async Task<UpdateManifest> DownloadAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new InvalidDataException("Invalid manifest URL.");
        }

        var json = await _http.GetStringAsync(uri, cancellationToken);

        var manifest = JsonSerializer.Deserialize<UpdateManifest>(json, _options);

        if (manifest == null)
            throw new InvalidDataException("The update manifest is invalid.");

        return manifest;
    }
}
