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

    private TextBox txtLog = null!;
    private CheckedListBox clbServers = null!;

    private Button btnAddServers = null!;
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
        lblServers = new Label();
        lblActivity = new Label();

        txtLog = new TextBox();
        clbServers = new CheckedListBox();

        btnAddServers = new Button();
        btnBrowse = new Button();
        btnRefresh = new Button();
        btnRestoreCs = new Button();
        btnRestoreCzero = new Button();

        SuspendLayout();

        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(18, 18, 24);
        ClientSize = new Size(900, 620);
        ForeColor = Color.White;
        MinimumSize = new Size(800, 560);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "TargetCS-Gaming.org";

        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblTitle.Location = new Point(30, 25);
        lblTitle.Text = "TargetCS-Gaming.org";

        lblCsStatus.AutoSize = true;
        lblCsStatus.Location = new Point(32, 82);
        lblCsStatus.Text = "CS 1.6: Checking...";

        lblCzeroStatus.AutoSize = true;
        lblCzeroStatus.Location = new Point(32, 108);
        lblCzeroStatus.Text = "Condition Zero: Checking...";

        btnBrowse.Location = new Point(690, 74);
        btnBrowse.Size = new Size(90, 35);
        btnBrowse.Text = "Browse";
        btnBrowse.Click += btnBrowse_Click;

        btnRefresh.Location = new Point(790, 74);
        btnRefresh.Size = new Size(80, 35);
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += btnRefresh_Click;

        lblServers.AutoSize = true;
        lblServers.Location = new Point(30, 155);
        lblServers.Text = "Community Servers";

        clbServers.Location = new Point(30, 185);
        clbServers.Size = new Size(840, 180);
        clbServers.BackColor = Color.FromArgb(28, 28, 36);
        clbServers.ForeColor = Color.White;

        btnAddServers.Location = new Point(30, 390);
        btnAddServers.Size = new Size(400, 55);
        btnAddServers.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnAddServers.Text = "ADD SERVERS";
        btnAddServers.Click += btnAddServers_Click;

        btnRestoreCs.Location = new Point(460, 390);
        btnRestoreCs.Size = new Size(195, 45);
        btnRestoreCs.Text = "Restore CS 1.6";
        btnRestoreCs.Click += btnRestoreCs_Click;

        btnRestoreCzero.Location = new Point(675, 390);
        btnRestoreCzero.Size = new Size(195, 45);
        btnRestoreCzero.Text = "Restore CZ";
        btnRestoreCzero.Click += btnRestoreCzero_Click;

        lblActivity.AutoSize = true;
        lblActivity.Location = new Point(30, 465);
        lblActivity.Text = "Activity";

        txtLog.Location = new Point(30, 495);
        txtLog.Size = new Size(840, 90);
        txtLog.Multiline = true;
        txtLog.ReadOnly = true;
        txtLog.ScrollBars = ScrollBars.Vertical;
        txtLog.BackColor = Color.FromArgb(12, 12, 16);
        txtLog.ForeColor = Color.LightGray;

        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(30, 595);
        lblStatus.Text = "Ready.";

        Controls.Add(lblTitle);
        Controls.Add(lblCsStatus);
        Controls.Add(lblCzeroStatus);
        Controls.Add(btnBrowse);
        Controls.Add(btnRefresh);
        Controls.Add(lblServers);
        Controls.Add(clbServers);
        Controls.Add(btnAddServers);
        Controls.Add(btnRestoreCs);
        Controls.Add(btnRestoreCzero);
        Controls.Add(lblActivity);
        Controls.Add(txtLog);
        Controls.Add(lblStatus);

        ResumeLayout(false);
        PerformLayout();
    }
}
