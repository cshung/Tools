namespace ImageMeasurer
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class ImageMeasurerForm : Form
    {
        private const int Resolution = 5;
        private Image image;
        private bool isMouseDown = false;
        private int sx = 0;
        private int sy = 0;
        private int ex = 0;
        private int ey = 0;

        public ImageMeasurerForm()
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
            g.DrawImage(this.image, 0, 0);
            for (int y = 0; y < this.image.Height; y += Resolution)
            {
                g.DrawLine(new Pen(Color.LightGray), 0, y, this.image.Width, y);
            }

            for (int x = 0; x < this.image.Width; x += Resolution)
            {
                g.DrawLine(new Pen(Color.LightGray), x, 0, x, this.image.Height);
            }

            if (this.isMouseDown)
            {
                int small_x = Math.Min(this.sx, this.ex);
                int large_x = Math.Max(this.sx, this.ex) + Resolution;
                int small_y = Math.Min(this.sy, this.ey);
                int large_y = Math.Max(this.sy, this.ey) + Resolution;

                g.DrawEllipse(new Pen(Color.Cyan, 1), small_x, small_y, large_x - small_x, large_y - small_y);
                g.DrawRectangle(new Pen(Color.Cyan, 1), small_x, small_y, large_x - small_x, large_y - small_y);

                this.toolStripStatusLabel.Text = string.Format("({0},{1})-({2},{3})", small_x, small_y, large_x, large_y);
            }
        }

        private void OnImageMeasurerFormLoad(object sender, EventArgs e)
        {
            this.image = Image.FromFile(@"c:\temp\pic.png");
            this.pictureBox.Width = this.image.Width;
            this.pictureBox.Height = this.image.Height;
            this.ClientSize = new Size(this.pictureBox.Width, this.pictureBox.Height + this.statusStrip.Height);
            this.Invalidate();
        }
    }
}
