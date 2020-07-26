namespace ImageHighlighter
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class ImageHighlighterForm : Form
    {
        private const int Resolution = 5;
        private Bitmap image;
        private bool isMouseDown = false;
        private bool isMouseEver = false;
        private int sx = 0;
        private int sy = 0;
        private int ex = 0;
        private int ey = 0;

        public ImageHighlighterForm()
        {
            this.InitializeComponent();
        }

        private void OnPictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            this.sx = (e.X / Resolution) * Resolution;
            this.sy = (e.Y / Resolution) * Resolution;
            this.ex = this.sx;
            this.ey = this.sy;
            this.isMouseDown = true;
            this.isMouseEver = true;
            this.Refresh();
        }

        private void OnPictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMouseDown)
            {
                int nx = (e.X / Resolution) * Resolution;
                int ny = (e.Y / Resolution) * Resolution;
                if (this.ex != nx || this.ey != ny)
                {
                    this.ex = nx;
                    this.ey = ny;
                    this.Refresh();
                }
            }
        }

        private void OnPictureBoxMouseUp(object sender, MouseEventArgs e)
        {
            this.isMouseDown = false;
            this.Refresh();
        }

        private void OnPictureBoxPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (this.image != null)
            {
                g.DrawImage(this.image, 0, 0);
                if (this.isMouseDown || this.isMouseEver)
                {
                    int small_x = Math.Min(this.sx, this.ex);
                    int large_x = Math.Max(this.sx, this.ex) + Resolution;
                    int small_y = Math.Min(this.sy, this.ey);
                    int large_y = Math.Max(this.sy, this.ey) + Resolution;
                    g.DrawEllipse(new Pen(Color.Cyan, 1), small_x, small_y, large_x - small_x, large_y - small_y);
                }
            }
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
                this.ClientSize = new Size(this.pictureBox.Width, this.pictureBox.Height + this.menuStrip.Height);
                this.Invalidate();
            }
        }

        private void OnSaveMenuItemClicked(object sender, EventArgs e)
        {
            if (this.image != null)
            {
                DialogResult dialogResult = this.saveFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    Bitmap toSave = new Bitmap(this.image.Width, this.image.Height);
                    for (int x = 0; x < this.image.Width; x++)
                    {
                        for (int y = 0; y < this.image.Height; y++)
                        {
                            Color c = this.image.GetPixel(x, y);
                            double cx = (this.ex + this.sx) / 2.0;
                            double rx = (this.ex - this.sx) / 2.0;
                            double cy = (this.ey + this.sy) / 2.0;
                            double ry = (this.ey - this.sy) / 2.0;
                            double check = ((x - cx) * (x - cx) / (rx * rx)) + ((y - cy) * (y - cy) / (ry * ry));
                            double factor = check > 1 ? 0.9 : 1;
                            int r = (int)(c.R * factor);
                            int g = (int)(c.G * factor);
                            int b = (int)(c.B * factor);
                            toSave.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                    }

                    toSave.Save(this.saveFileDialog.FileName);
                }
            }
        }
    }
}
