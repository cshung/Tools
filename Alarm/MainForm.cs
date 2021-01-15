/**
 * Display a full screen message
 */
namespace Alarm
{
    using System;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private string text;

        public MainForm()
            : this("CheckMail")
        {
        }

        public MainForm(string text)
        {
            this.InitializeComponent();
            this.text = text;
            // The schedule is simply every 30 minutes
            this.timer.Interval = 1000 * 60 * 30;
            this.timer.Enabled = true;
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

        private void OnMainFormClicked(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            GoFullScreen();
        }
    }
}
