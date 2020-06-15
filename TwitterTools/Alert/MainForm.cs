namespace Alert
{
    using System;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private string text;
        private IPoller poller;

        public MainForm(IPoller poller) : base()
        {
            this.poller = poller;
            this.text = "Alert";
            this.InitializeComponent();
        }

        private void GoFullScreen()
        {
            this.textLabel.Text = this.text;
            this.textLabel.Left = (this.ClientSize.Width - this.textLabel.Width) / 2;
            this.textLabel.Top = (this.ClientSize.Height - this.textLabel.Height) / 2;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Focus();
        }

        private void OnPollingTimerTick(object sender, EventArgs e)
        {
            if (this.poller.Poll())
            {
                this.GoFullScreen();
            }
        }

        private void OnMainFormClicked(object sender, MouseEventArgs e)
        {
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }
    }
}
