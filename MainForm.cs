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
        Shown += MainForm_Shown;
    }

    private async void MainForm_Shown(object? sender, EventArgs e)
    {
        await DetectGamesAsync();
    }

    private async Task DetectGamesAsync()
    {
        SetStatus("Checking your game installation...");

        _halfLifeRoot = _detector.DetectHalfLife();

        if (_halfLifeRoot == null)
        {
            SetStatus("Game installation was not found automatically.");
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
            ? "CS 1.6: Found"
            : "CS 1.6: Not found";

        lblCzeroStatus.Text = _czeroRoot != null
            ? "Condition Zero: Found"
            : "Condition Zero: Not found";

        SetStatus("Game check complete.");

        await LoadManifestAsync();
    }

    private async Task LoadManifestAsync()
    {
        try
        {
            SetStatus("Loading community data...");

            _manifest = await _manifestService.DownloadAsync(ManifestUrl);

            PopulateServers();

            SetStatus("Community data loaded.");
        }
        catch (Exception ex)
        {
            SetStatus("Could not load community data.");
            txtLog.AppendText($"Details: {ex.Message}{Environment.NewLine}");
        }
    }

    private void PopulateServers()
    {
        if (_manifest == null)
            return;

        clbServers.Items.Clear();

        foreach (var server in _manifest.Servers)
        {
            clbServers.Items.Add(server.Name, true);
        }
    }

    private async void btnAddServers_Click(object sender, EventArgs e)
    {
        await AddServersAsync();
    }

    private async Task AddServersAsync()
    {
        if (_manifest == null)
        {
            MessageBox.Show(
                "Community data is not loaded yet.",
                "TargetCS-Gaming.org",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        btnAddServers.Enabled = false;

        try
        {
            var temporaryDirectory = Path.Combine(
                Path.GetTempPath(),
                "TargetCS-Gaming.org");

            Directory.CreateDirectory(temporaryDirectory);

            if (_cs16Root != null)
            {
                await UpdateGameMenuAsync(
                    "CS 1.6",
                    _cs16Root,
                    _manifest.Cs16,
                    temporaryDirectory);
            }

            if (_czeroRoot != null)
            {
                await UpdateGameMenuAsync(
                    "Condition Zero",
                    _czeroRoot,
                    _manifest.Czero,
                    temporaryDirectory);
            }

            UpdateFavorites();

            SetStatus("Everything is ready.");

            MessageBox.Show(
                "Community content was installed successfully.",
                "TargetCS-Gaming.org",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            SetStatus("Something went wrong.");
            txtLog.AppendText($"Details: {ex.Message}{Environment.NewLine}");

            MessageBox.Show(
                "The update could not be completed.",
                "TargetCS-Gaming.org",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            btnAddServers.Enabled = true;
        }
    }

    private async Task UpdateGameMenuAsync(
        string gameName,
        string gameRoot,
        GameUpdate update,
        string temporaryDirectory)
    {
        if (string.IsNullOrWhiteSpace(update.GameMenuUrl))
            return;

        var target = Path.Combine(
            gameRoot,
            "resource",
            "GameMenu.res");

        SetStatus($"Updating {gameName} menu...");

        var downloaded = await _fileUpdater.DownloadFileAsync(
            update.GameMenuUrl,
            temporaryDirectory);

        if (File.Exists(target))
            _fileUpdater.Backup(target);

        _fileUpdater.Replace(downloaded, target);
    }

    private void UpdateFavorites()
    {
        if (_manifest == null)
            return;

        if (_halfLifeRoot == null)
        {
            SetStatus("Game installation was not found.");
            return;
        }

        var browser = _detector.FindServerBrowserFromHalfLife(_halfLifeRoot);

        if (browser == null)
        {
            SetStatus("Saved server list was not found.");
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
            SetStatus("No servers were selected.");
            return;
        }

        SetStatus("Saving selected servers...");

        _favoriteManager.Backup(browser);

        var added = _favoriteManager.AddServers(browser, selected);

        SetStatus($"{added} new server(s) were added.");
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
            ? "CS 1.6: Found"
            : "CS 1.6: Not found";

        lblCzeroStatus.Text = _czeroRoot != null
            ? "Condition Zero: Found"
            : "Condition Zero: Not found";

        SetStatus("Game folder selected.");
    }

    private void btnRestoreCs_Click(object sender, EventArgs e)
    {
        try
        {
            if (_cs16Root == null)
                throw new Exception("CS 1.6 was not found.");

            var target = Path.Combine(
                _cs16Root,
                "resource",
                "GameMenu.res");

            _fileUpdater.Restore(target);

            SetStatus("CS 1.6 menu restored.");

            MessageBox.Show(
                "CS 1.6 menu restored.",
                "TargetCS-Gaming.org",
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
                throw new Exception("Condition Zero was not found.");

            var target = Path.Combine(
                _czeroRoot,
                "resource",
                "GameMenu.res");

            _fileUpdater.Restore(target);

            SetStatus("Condition Zero menu restored.");

            MessageBox.Show(
                "Condition Zero menu restored.",
                "TargetCS-Gaming.org",
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
        txtLog.AppendText($"{text}{Environment.NewLine}");
    }
}
