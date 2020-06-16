namespace Alert
{
    using System;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private string text;
        private IPoller poller;
        private int counter = 0;

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
            if (this.counter % 60 == 0)
            {
                string result = this.poller.Poll();
                if (result != null)
                {
                    this.text = result;
                    this.GoFullScreen();
                }
            }
            this.Text = string.Format("{0}: {1} seconds to go until the next poll.", this.counter / 60, 60 - this.counter % 60);
            this.counter++;
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
