namespace TargetCS_Gaming.org;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;

    private Label lblTitle = null!;
    private Label lblCsStatus = null!;
    private Label lblCzeroStatus = null!;
    private Label lblStatus = null!;

    private TextBox txtManifestUrl = null!;
    private TextBox txtLog = null!;

    private CheckedListBox clbServers = null!;

    private CheckBox chkCs16 = null!;
    private CheckBox chkCzero = null!;
    private CheckBox chkFavorites = null!;

    private Button btnUpdate = null!;
    private Button btnBrowse = null!;
    private Button btnRefresh = null!;
    private Button btnRestoreCs = null!;
    private Button btnRestoreCzero = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        lblTitle = new Label();
        lblCsStatus = new Label();
        lblCzeroStatus = new Label();
        lblStatus = new Label();

        txtManifestUrl = new TextBox();
        txtLog = new TextBox();

        clbServers = new CheckedListBox();

        chkCs16 = new CheckBox();
        chkCzero = new CheckBox();
        chkFavorites = new CheckBox();

        btnUpdate = new Button();
        btnBrowse = new Button();
        btnRefresh = new Button();
        btnRestoreCs = new Button();
        btnRestoreCzero = new Button();

        SuspendLayout();

        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(18, 18, 24);
        ClientSize = new Size(900, 700);
        ForeColor = Color.White;
        MinimumSize = new Size(800, 600);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "TargetCS-Gaming.org";

        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblTitle.Location = new Point(30, 25);
        lblTitle.Text = "TargetCS-Gaming.org";

        lblCsStatus.AutoSize = true;
        lblCsStatus.Location = new Point(32, 78);
        lblCsStatus.Text = "CS 1.6: Detecting...";

        lblCzeroStatus.AutoSize = true;
        lblCzeroStatus.Location = new Point(32, 105);
        lblCzeroStatus.Text = "Condition Zero: Detecting...";

        btnBrowse.Location = new Point(690, 72);
        btnBrowse.Size = new Size(90, 35);
        btnBrowse.Text = "Browse";
        btnBrowse.Click += btnBrowse_Click;

        btnRefresh.Location = new Point(790, 72);
        btnRefresh.Size = new Size(80, 35);
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += btnRefresh_Click;

        var lblManifest = new Label
        {
            AutoSize = true,
            Location = new Point(32, 145),
            Text = "Manifest URL:"
        };

        txtManifestUrl.Location = new Point(140, 141);
        txtManifestUrl.Size = new Size(700, 27);

        var groupOptions = new GroupBox
        {
            Location = new Point(30, 185),
            Size = new Size(400, 160),
            Text = "Update Components",
            ForeColor = Color.White
        };

        chkCs16.AutoSize = true;
        chkCs16.Checked = true;
        chkCs16.Location = new Point(20, 35);
        chkCs16.Text = "Update CS 1.6 GameMenu.res";

        chkCzero.AutoSize = true;
        chkCzero.Checked = true;
        chkCzero.Location = new Point(20, 70);
        chkCzero.Text = "Update Condition Zero GameMenu.res";

        chkFavorites.AutoSize = true;
        chkFavorites.Checked = true;
        chkFavorites.Location = new Point(20, 105);
        chkFavorites.Text = "Add community servers to Favorites";

        groupOptions.Controls.Add(chkCs16);
        groupOptions.Controls.Add(chkCzero);
        groupOptions.Controls.Add(chkFavorites);

        var lblServers = new Label
        {
            AutoSize = true,
            Location = new Point(460, 190),
            Text = "Community Servers"
        };

        clbServers.Location = new Point(460, 220);
        clbServers.Size = new Size(410, 125);
        clbServers.BackColor = Color.FromArgb(28, 28, 36);
        clbServers.ForeColor = Color.White;

        btnUpdate.Location = new Point(30, 370);
        btnUpdate.Size = new Size(400, 55);
        btnUpdate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnUpdate.Text = "INSTALL / UPDATE";
        btnUpdate.Click += btnUpdate_Click;

        btnRestoreCs.Location = new Point(460, 370);
        btnRestoreCs.Size = new Size(195, 45);
        btnRestoreCs.Text = "Restore CS 1.6";
        btnRestoreCs.Click += btnRestoreCs_Click;

        btnRestoreCzero.Location = new Point(675, 370);
        btnRestoreCzero.Size = new Size(195, 45);
        btnRestoreCzero.Text = "Restore CZ";
        btnRestoreCzero.Click += btnRestoreCzero_Click;

        var lblLog = new Label
        {
            AutoSize = true,
            Location = new Point(30, 445),
            Text = "Activity"
        };

        txtLog.Location = new Point(30, 475);
        txtLog.Size = new Size(840, 160);
        txtLog.Multiline = true;
        txtLog.ReadOnly = true;
        txtLog.ScrollBars = ScrollBars.Vertical;
        txtLog.BackColor = Color.FromArgb(12, 12, 16);
        txtLog.ForeColor = Color.LightGray;

        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(30, 650);
        lblStatus.Text = "Ready.";

        Controls.Add(lblTitle);
        Controls.Add(lblCsStatus);
        Controls.Add(lblCzeroStatus);
        Controls.Add(btnBrowse);
        Controls.Add(btnRefresh);
        Controls.Add(lblManifest);
        Controls.Add(txtManifestUrl);
        Controls.Add(groupOptions);
        Controls.Add(lblServers);
        Controls.Add(clbServers);
        Controls.Add(btnUpdate);
        Controls.Add(btnRestoreCs);
        Controls.Add(btnRestoreCzero);
        Controls.Add(lblLog);
        Controls.Add(txtLog);
        Controls.Add(lblStatus);

        ResumeLayout(false);
        PerformLayout();
    }
}
