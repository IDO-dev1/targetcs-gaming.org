namespace TargetCS_Gaming.org.Services;

public class FastDlService
{
    private readonly HttpClient _http;

    public FastDlService()
    {
        _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(20)
        };

        _http.DefaultRequestHeaders.UserAgent.ParseAdd("TargetCS-Gaming.org/1.0");
    }

    public async Task<bool> IsReachableAsync(
        string baseUrl,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            return false;

        try
        {
            using var response = await _http.GetAsync(
                baseUrl,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public void OpenInBrowser(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return;

        System.Diagnostics.Process.Start(
            new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
    }
}
