namespace ImageOverlay
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class ImageOverlayForm : Form
    {
        private Image fix;
        private Image move;
        private bool isMouseDown;
        private int sx;
        private int sy;
        private int ex;
        private int ey;
        private int ox;
        private int oy;

        public ImageOverlayForm()
        {
            this.InitializeComponent();
        }

        private void OnImageOverlayFormLoad(object sender, EventArgs e)
        {
            this.fix = Image.FromFile(@"c:\temp\capture.png").SetOpacity(0.5f);
            this.move = Image.FromFile(@"c:\temp\pic.png").SetOpacity(0.5f);
            this.pictureBox.Width = this.fix.Width;
            this.pictureBox.Height = this.fix.Height;
            this.ClientSize = new Size(this.pictureBox.Width, this.pictureBox.Height);
            this.Invalidate();
        }

        private void OnPictureBoxPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(this.fix, 0, 0);
            g.DrawImage(this.move, this.ox + (this.ex - this.sx), this.oy + (this.ey - this.sy));
        }

        private void OnPictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            this.sx = (e.X / 10) * 10;
            this.sy = (e.Y / 10) * 10;
            this.ex = this.sx;
            this.ey = this.sy;
            this.isMouseDown = true;
            this.Refresh();
        }

        private void OnPictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMouseDown)
            {
                int nx = (e.X / 10) * 10;
                int ny = (e.Y / 10) * 10;
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
            this.ox += this.ex - this.sx;
            this.oy += this.ey - this.sy;
            this.sx = this.ex = 0;
            this.sy = this.ey = 0;
            this.isMouseDown = false;
            this.Refresh();
        }
    }
}
