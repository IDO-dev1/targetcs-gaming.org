namespace TargetCS_Gaming.org;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;

    private Label lblTitle = null!;
    private Label lblCsStatus = null!;
    private Label lblCzeroStatus = null!;
    private Label lblStatus = null!;
    private Label lblServers = null!;
    private Label lblActivity = null!;
    private Panel topPanel = null!;
    private Panel mainPanel = null!;
    private Panel bottomPanel = null!;

    private TextBox txtLog = null!;
    private CheckedListBox clbServers = null!;

    private Button btnAddServers = null!;
    private Button btnBrowse = null!;
    private Button btnRefresh = null!;
    private Button btnRestoreCs = null!;
    private Button btnRestoreCzero = null!;
    private Button btnFastDl = null!;

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
        lblServers = new Label();
        lblActivity = new Label();

        topPanel = new Panel();
        mainPanel = new Panel();
        bottomPanel = new Panel();

        txtLog = new TextBox();
        clbServers = new CheckedListBox();

        btnAddServers = new Button();
        btnBrowse = new Button();
        btnRefresh = new Button();
        btnRestoreCs = new Button();
        btnRestoreCzero = new Button();
        btnFastDl = new Button();

        SuspendLayout();

        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(14, 14, 18);
        ClientSize = new Size(980, 720);
        ForeColor = Color.White;
        MinimumSize = new Size(900, 680);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "TargetCS-Gaming.org";

        topPanel.Dock = DockStyle.Top;
        topPanel.Height = 150;
        topPanel.BackColor = Color.FromArgb(22, 22, 30);
        topPanel.Padding = new Padding(24);

        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(24, 18);
        lblTitle.Text = "TargetCS-Gaming.org";

        lblCsStatus.AutoSize = true;
        lblCsStatus.ForeColor = Color.Gainsboro;
        lblCsStatus.Location = new Point(26, 78);
        lblCsStatus.Text = "CS 1.6: Checking...";

        lblCzeroStatus.AutoSize = true;
        lblCzeroStatus.ForeColor = Color.Gainsboro;
        lblCzeroStatus.Location = new Point(26, 105);
        lblCzeroStatus.Text = "Condition Zero: Checking...";

        btnBrowse.Location = new Point(720, 24);
        btnBrowse.Size = new Size(100, 36);
        btnBrowse.Text = "Browse";
        btnBrowse.BackColor = Color.FromArgb(40, 40, 52);
        btnBrowse.ForeColor = Color.White;
        btnBrowse.FlatStyle = FlatStyle.Flat;
        btnBrowse.Click += btnBrowse_Click;

        btnRefresh.Location = new Point(832, 24);
        btnRefresh.Size = new Size(100, 36);
        btnRefresh.Text = "Refresh";
        btnRefresh.BackColor = Color.FromArgb(40, 40, 52);
        btnRefresh.ForeColor = Color.White;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Click += btnRefresh_Click;

        topPanel.Controls.Add(lblTitle);
        topPanel.Controls.Add(lblCsStatus);
        topPanel.Controls.Add(lblCzeroStatus);
        topPanel.Controls.Add(btnBrowse);
        topPanel.Controls.Add(btnRefresh);

        mainPanel.Dock = DockStyle.Fill;
        mainPanel.BackColor = Color.FromArgb(16, 16, 22);
        mainPanel.Padding = new Padding(24);

        lblServers.AutoSize = true;
        lblServers.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblServers.ForeColor = Color.WhiteSmoke;
        lblServers.Location = new Point(24, 20);
        lblServers.Text = "Community Servers";

        clbServers.Location = new Point(24, 55);
        clbServers.Size = new Size(900, 220);
        clbServers.BackColor = Color.FromArgb(28, 28, 38);
        clbServers.ForeColor = Color.White;
        clbServers.BorderStyle = BorderStyle.FixedSingle;

        btnAddServers.Location = new Point(24, 300);
        btnAddServers.Size = new Size(280, 56);
        btnAddServers.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnAddServers.Text = "ADD SERVERS";
        btnAddServers.BackColor = Color.FromArgb(0, 120, 215);
        btnAddServers.ForeColor = Color.White;
        btnAddServers.FlatStyle = FlatStyle.Flat;
        btnAddServers.Click += btnAddServers_Click;

        btnFastDl.Location = new Point(324, 300);
        btnFastDl.Size = new Size(180, 56);
        btnFastDl.Text = "Open FastDL";
        btnFastDl.BackColor = Color.FromArgb(45, 45, 60);
        btnFastDl.ForeColor = Color.White;
        btnFastDl.FlatStyle = FlatStyle.Flat;
        btnFastDl.Click += btnFastDl_Click;

        btnRestoreCs.Location = new Point(524, 300);
        btnRestoreCs.Size = new Size(180, 56);
        btnRestoreCs.Text = "Restore CS 1.6";
        btnRestoreCs.BackColor = Color.FromArgb(45, 45, 60);
        btnRestoreCs.ForeColor = Color.White;
        btnRestoreCs.FlatStyle = FlatStyle.Flat;
        btnRestoreCs.Click += btnRestoreCs_Click;

        btnRestoreCzero.Location = new Point(724, 300);
        btnRestoreCzero.Size = new Size(180, 56);
        btnRestoreCzero.Text = "Restore CZ";
        btnRestoreCzero.BackColor = Color.FromArgb(45, 45, 60);
        btnRestoreCzero.ForeColor = Color.White;
        btnRestoreCzero.FlatStyle = FlatStyle.Flat;
        btnRestoreCzero.Click += btnRestoreCzero_Click;

        lblActivity.AutoSize = true;
        lblActivity.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblActivity.ForeColor = Color.WhiteSmoke;
        lblActivity.Location = new Point(24, 380);
        lblActivity.Text = "Activity";

        txtLog.Location = new Point(24, 415);
        txtLog.Size = new Size(900, 130);
        txtLog.Multiline = true;
        txtLog.ReadOnly = true;
        txtLog.ScrollBars = ScrollBars.Vertical;
        txtLog.BackColor = Color.FromArgb(10, 10, 14);
        txtLog.ForeColor = Color.LightGray;
        txtLog.BorderStyle = BorderStyle.FixedSingle;

        mainPanel.Controls.Add(lblServers);
        mainPanel.Controls.Add(clbServers);
        mainPanel.Controls.Add(btnAddServers);
        mainPanel.Controls.Add(btnFastDl);
        mainPanel.Controls.Add(btnRestoreCs);
        mainPanel.Controls.Add(btnRestoreCzero);
        mainPanel.Controls.Add(lblActivity);
        mainPanel.Controls.Add(txtLog);

        bottomPanel.Dock = DockStyle.Bottom;
        bottomPanel.Height = 54;
        bottomPanel.BackColor = Color.FromArgb(20, 20, 26);
        bottomPanel.Padding = new Padding(24, 12, 24, 12);

        lblStatus.AutoSize = true;
        lblStatus.ForeColor = Color.LightGray;
        lblStatus.Location = new Point(24, 16);
        lblStatus.Text = "Ready.";

        bottomPanel.Controls.Add(lblStatus);

        Controls.Add(mainPanel);
        Controls.Add(topPanel);
        Controls.Add(bottomPanel);

        ResumeLayout(false);
        PerformLayout();
    }
}
