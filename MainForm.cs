using TargetCS_Gaming.org.Models;
using TargetCS_Gaming.org.Services;

namespace TargetCS_Gaming.org;

public partial class MainForm : Form
{
    private readonly GameDetector _detector = new();
    private readonly FileUpdater _fileUpdater = new();
    private readonly ManifestService _manifestService = new();
    private readonly FavoriteManager _favoriteManager = new();

    private UpdateManifest? _manifest;

    private string? _halfLifeRoot;
    private string? _cs16Root;
    private string? _czeroRoot;

    private const string ManifestUrl =
        "https://targetcs-gaming.org/updater/manifest.json";

    public MainForm()
    {
        InitializeComponent();
        txtManifestUrl.Text = ManifestUrl;
        Shown += MainForm_Shown;
    }

    private async void MainForm_Shown(object? sender, EventArgs e)
    {
        await DetectGamesAsync();
    }

    private async Task DetectGamesAsync()
    {
        SetStatus("Detecting Steam installation...");

        _halfLifeRoot = _detector.DetectHalfLife();

        if (_halfLifeRoot == null)
        {
            SetStatus("CS 1.6 installation not automatically detected.");
            lblCsStatus.Text = "CS 1.6: Not detected";
            lblCzeroStatus.Text = "Condition Zero: Not detected";
            return;
        }

        _cs16Root = Directory.Exists(Path.Combine(_halfLifeRoot, "cstrike"))
            ? Path.Combine(_halfLifeRoot, "cstrike")
            : null;

        _czeroRoot = Directory.Exists(Path.Combine(_halfLifeRoot, "czero"))
            ? Path.Combine(_halfLifeRoot, "czero")
            : null;

        lblCsStatus.Text = _cs16Root != null
            ? $"CS 1.6: {_cs16Root}"
            : "CS 1.6: Not found";

        lblCzeroStatus.Text = _czeroRoot != null
            ? $"Condition Zero: {_czeroRoot}"
            : "Condition Zero: Not found";

        SetStatus("Detection complete.");

        await LoadManifestAsync();
    }

    private async Task LoadManifestAsync()
    {
        try
        {
            SetStatus("Downloading update manifest...");

            _manifest = await _manifestService.DownloadAsync(
                txtManifestUrl.Text.Trim());

            PopulateServers();

            SetStatus($"Manifest loaded: {_manifest.Version}");
        }
        catch (Exception ex)
        {
            SetStatus($"Manifest error: {ex.Message}");
        }
    }

    private void PopulateServers()
    {
        if (_manifest == null)
            return;

        clbServers.Items.Clear();

        foreach (var server in _manifest.Servers)
        {
            clbServers.Items.Add(
                $"{server.Name}  [{server.Address}]",
                true);
        }
    }

    private async void btnUpdate_Click(object sender, EventArgs e)
    {
        await UpdateEverythingAsync();
    }

    private async Task UpdateEverythingAsync()
    {
        if (_manifest == null)
        {
            MessageBox.Show(
                "The update manifest has not been loaded.",
                "Updater",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        btnUpdate.Enabled = false;

        try
        {
            var temporaryDirectory = Path.Combine(
                Path.GetTempPath(),
                "TargetCS-Gaming.org");

            Directory.CreateDirectory(temporaryDirectory);

            if (chkCs16.Checked && _cs16Root != null)
            {
                await UpdateGameMenuAsync(
                    "CS 1.6",
                    _cs16Root,
                    _manifest.Cs16,
                    temporaryDirectory);
            }

            if (chkCzero.Checked && _czeroRoot != null)
            {
                await UpdateGameMenuAsync(
                    "Condition Zero",
                    _czeroRoot,
                    _manifest.Czero,
                    temporaryDirectory);
            }

            if (chkFavorites.Checked)
            {
                UpdateFavorites();
            }

            SetStatus("Update completed successfully.");

            MessageBox.Show(
                "The selected updates were installed successfully.",
                "Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            SetStatus($"Update failed: {ex.Message}");

            MessageBox.Show(
                ex.ToString(),
                "Update failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            btnUpdate.Enabled = true;
        }
    }

    private async Task UpdateGameMenuAsync(
        string gameName,
        string gameRoot,
        GameUpdate update,
        string temporaryDirectory)
    {
        if (string.IsNullOrWhiteSpace(update.GameMenuUrl))
        {
            SetStatus($"{gameName}: no GameMenu URL configured.");
            return;
        }

        var target = Path.Combine(
            gameRoot,
            "resource",
            "GameMenu.res");

        SetStatus($"{gameName}: downloading GameMenu.res...");

        var downloaded = await _fileUpdater.DownloadFileAsync(
            update.GameMenuUrl,
            temporaryDirectory);

        if (File.Exists(target))
        {
            SetStatus($"{gameName}: creating backup...");
            _fileUpdater.Backup(target);
        }

        SetStatus($"{gameName}: installing GameMenu.res...");

        _fileUpdater.Replace(downloaded, target);
    }

    private void UpdateFavorites()
    {
        if (_manifest == null)
            return;

        if (_halfLifeRoot == null)
        {
            SetStatus("Favorites: Half-Life installation not found.");
            return;
        }

        var browser = _detector.FindServerBrowserFromHalfLife(_halfLifeRoot);

        if (browser == null)
        {
            SetStatus("Favorites: ServerBrowser.vdf not found.");
            return;
        }

        var selected = new List<ServerEntry>();

        for (var i = 0; i < clbServers.Items.Count; i++)
        {
            if (!clbServers.GetItemChecked(i))
                continue;

            if (i < _manifest.Servers.Count)
                selected.Add(_manifest.Servers[i]);
        }

        if (selected.Count == 0)
        {
            SetStatus("Favorites: no servers selected.");
            return;
        }

        SetStatus("Favorites: creating backup...");
        _favoriteManager.Backup(browser);

        SetStatus("Favorites: adding servers...");

        var added = _favoriteManager.AddServers(browser, selected);

        SetStatus($"Favorites: {added} new server(s) added.");
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select your Half-Life / CS 1.6 installation folder."
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        _halfLifeRoot = dialog.SelectedPath;

        var cs = Path.Combine(_halfLifeRoot, "cstrike");
        var czero = Path.Combine(_halfLifeRoot, "czero");

        _cs16Root = Directory.Exists(cs) ? cs : null;
        _czeroRoot = Directory.Exists(czero) ? czero : null;

        lblCsStatus.Text = _cs16Root != null
            ? $"CS 1.6: {_cs16Root}"
            : "CS 1.6: Not found";

        lblCzeroStatus.Text = _czeroRoot != null
            ? $"Condition Zero: {_czeroRoot}"
            : "Condition Zero: Not found";

        SetStatus("Installation selected manually.");
    }

    private void btnRestoreCs_Click(object sender, EventArgs e)
    {
        try
        {
            if (_cs16Root == null)
                throw new Exception("CS 1.6 is not detected.");

            var target = Path.Combine(
                _cs16Root,
                "resource",
                "GameMenu.res");

            _fileUpdater.Restore(target);

            SetStatus("CS 1.6 GameMenu restored.");

            MessageBox.Show(
                "CS 1.6 GameMenu.res restored.",
                "Restore",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Restore failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnRestoreCzero_Click(object sender, EventArgs e)
    {
        try
        {
            if (_czeroRoot == null)
                throw new Exception("Condition Zero is not detected.");

            var target = Path.Combine(
                _czeroRoot,
                "resource",
                "GameMenu.res");

            _fileUpdater.Restore(target);

            SetStatus("Condition Zero GameMenu restored.");

            MessageBox.Show(
                "Condition Zero GameMenu.res restored.",
                "Restore",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Restore failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private async void btnRefresh_Click(object sender, EventArgs e)
    {
        await DetectGamesAsync();
    }

    private void SetStatus(string text)
    {
        if (InvokeRequired)
        {
            Invoke(() => SetStatus(text));
            return;
        }

        lblStatus.Text = text;

        txtLog.AppendText(
            $"[{DateTime.Now:HH:mm:ss}] {text}{Environment.NewLine}");
    }
}
