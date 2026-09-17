using System.Diagnostics;
using System.Text.Json;
using TargetCS_Gaming.org.Models;

namespace TargetCS_Gaming.org.Services;

public class SelfUpdateService
{
    private readonly HttpClient _http;

    private static readonly string UpdateDirectory =
        Path.Combine(
            Path.GetTempPath(),
            "TargetCS-Gaming.org-SelfUpdate");

    public SelfUpdateService()
    {
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(5)
        };

        _http.DefaultRequestHeaders.UserAgent.ParseAdd("TargetCS-Gaming.org/1.0");
    }

    public bool IsNewVersionAvailable(
        string currentVersion,
        string latestVersion)
    {
        if (string.IsNullOrWhiteSpace(latestVersion))
            return false;

        return !string.Equals(
            currentVersion.Trim(),
            latestVersion.Trim(),
            StringComparison.OrdinalIgnoreCase);
    }

    public async Task<string?> DownloadUpdateAsync(
        string downloadUrl,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(downloadUrl))
            return null;

        Directory.CreateDirectory(UpdateDirectory);

        var fileName = Path.GetFileName(new Uri(downloadUrl).AbsolutePath);

        if (string.IsNullOrWhiteSpace(fileName))
            fileName = "TargetCS-Gaming.org.exe";

        var destination = Path.Combine(
            UpdateDirectory,
            $"{Guid.NewGuid():N}_{fileName}");

        using var response = await _http.GetAsync(
            downloadUrl,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        await using var input =
            await response.Content.ReadAsStreamAsync(cancellationToken);

        await using var output = File.Create(destination);

        await input.CopyToAsync(output, cancellationToken);

        return destination;
    }

    public void LaunchUpdaterAndExit(string downloadedExe)
    {
        var currentExe = Process.GetCurrentProcess().MainModule?.FileName;
        if (string.IsNullOrWhiteSpace(currentExe))
            return;

        var updaterScript = Path.Combine(UpdateDirectory, "update.cmd");

        var script =
            "@echo off\r\n" +
            "timeout /t 2 /nobreak >nul\r\n" +
            $"taskkill /f /im \"{Path.GetFileName(currentExe)}\" >nul 2>&1\r\n" +
            $"copy /y \"{downloadedExe}\" \"{currentExe}\" >nul\r\n" +
            $"start \"\" \"{currentExe}\"\r\n" +
            "del /f /q \"%~f0\"\r\n";

        File.WriteAllText(updaterScript, script);

        Process.Start(new ProcessStartInfo
        {
            FileName = updaterScript,
            CreateNoWindow = true,
            UseShellExecute = false
        });

        Environment.Exit(0);
    }
}
