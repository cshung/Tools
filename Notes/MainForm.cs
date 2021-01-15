namespace Notes
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private Bitmap image;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OnExitMenuItemClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnOpenMenuItemClicked(object sender, EventArgs e)
        {
            DialogResult dialogResult = this.openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.image = Image.FromFile(this.openFileDialog.FileName) as Bitmap;
                this.pictureBox.Width = this.image.Width;
                this.pictureBox.Height = this.image.Height;
                this.ClientSize = new Size(this.pictureBox.Width, this.pictureBox.Height + this.menuBar.Height);
                this.Invalidate();
            }
        }

        private void OnPictureBoxPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (this.image != null)
            {
                g.DrawImage(this.image, 0, 0);
            }
        }
    }
}
