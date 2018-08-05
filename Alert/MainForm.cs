/**
 * Display a full screen message
 */
namespace Alert
{
    using System;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private string text;

        public MainForm()
            : this("Done")
        {
        }

        public MainForm(string text)
        {
            this.InitializeComponent();
            this.text = text;
        }

        private void OnMainFormLoaded(object sender, EventArgs e)
        {
            this.GoFullScreen();
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
            Application.Exit();
        }
    }
}
