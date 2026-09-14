namespace TargetCS_Gaming.org.Services;

public class FileUpdater
{
    private readonly HttpClient _http;

    public FileUpdater()
    {
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(5)
        };

        _http.DefaultRequestHeaders.UserAgent.ParseAdd("TargetCS-Gaming.org/1.0");
    }

    public async Task<string> DownloadFileAsync(
        string url,
        string temporaryDirectory,
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(temporaryDirectory);

        var uri = new Uri(url);
        var fileName = Path.GetFileName(uri.AbsolutePath);

        if (string.IsNullOrWhiteSpace(fileName))
            fileName = "GameMenu.res";

        var destination = Path.Combine(
            temporaryDirectory,
            $"{Guid.NewGuid():N}_{fileName}");

        using var response = await _http.GetAsync(
            uri,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        await using var input = await response.Content.ReadAsStreamAsync(cancellationToken);
        await using var output = File.Create(destination);

        await input.CopyToAsync(output, cancellationToken);

        return destination;
    }

    public string Backup(string target)
    {
        if (!File.Exists(target))
            throw new FileNotFoundException("Target file does not exist.", target);

        var backup = target + ".backup";

        if (File.Exists(backup))
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            backup = $"{target}.backup_{timestamp}";
        }

        File.Copy(target, backup, false);
        return backup;
    }

    public void Replace(string source, string target)
    {
        var directory = Path.GetDirectoryName(target);
        if (!string.IsNullOrWhiteSpace(directory))
            Directory.CreateDirectory(directory);

        var tempTarget = target + ".tmp";

        File.Copy(source, tempTarget, true);

        if (File.Exists(target))
            File.Delete(target);

        File.Move(tempTarget, target);
    }

    public void Restore(string target)
    {
        var directory = Path.GetDirectoryName(target) ?? ".";
        var name = Path.GetFileName(target);

        var backups = Directory.GetFiles(directory, name + ".backup*")
            .OrderByDescending(File.GetLastWriteTime)
            .ToArray();

        if (backups.Length == 0)
            throw new FileNotFoundException("No backup was found.");

        File.Copy(backups[0], target, true);
    }
}
