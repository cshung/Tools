namespace EliminateBorder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class EliminateBorderForm : Form
    {
        private Image image;
        private bool isMouseDown = false;
        private int sx = 0;
        private int sy = 0;
        private int ex = 0;
        private int ey = 0;

        public EliminateBorderForm()
        {
            this.InitializeComponent();
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
            this.isMouseDown = false;
            this.Refresh();
        }

        private void OnPictureBoxPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(this.image, 0, 0);
            for (int y = 0; y < this.image.Height; y += 10)
            {
                g.DrawLine(new Pen(Color.LightGray), 0, y, this.image.Width, y);
            }

            for (int x = 0; x < this.image.Width; x += 10)
            {
                g.DrawLine(new Pen(Color.LightGray), x, 0, x, this.image.Height);
            }

            if (this.isMouseDown)
            {
                int small_x = Math.Min(this.sx, this.ex);
                int large_x = Math.Max(this.sx, this.ex);
                int small_y = Math.Min(this.sy, this.ey);
                int large_y = Math.Max(this.sy, this.ey);

                for (int x = small_x; x < large_x; x++)
                {
                    g.FillRectangle(Brushes.Cyan, x, small_y, 10, 10);
                    g.FillRectangle(Brushes.Cyan, x, large_y, 10, 10);
                }

                for (int y = small_y; y < large_y; y++)
                {
                    g.FillRectangle(Brushes.Cyan, small_x, y, 10, 10);
                    g.FillRectangle(Brushes.Cyan, large_x, y, 10, 10);
                }

                this.toolStripStatusLabel.Text = string.Format("({0},{1})-({2},{3})", small_x, small_y, large_x + 10, large_y + 10);
            }
        }

        private void OnEliminateBorderFormLoad(object sender, EventArgs e)
        {
            this.Work();
            this.pictureBox.Width = this.image.Width;
            this.pictureBox.Height = this.image.Height;
            this.ClientSize = new Size(this.pictureBox.Width, this.pictureBox.Height + this.statusStrip.Height);
            this.Invalidate();
        }

        private void Work()
        {
            Image image = Image.FromFile(@"c:\temp\pic.png");
            Bitmap bitmap = new Bitmap(image);
            Color victim = bitmap.GetPixel(0, 0);
            var enqueued = new HashSet<Tuple<int, int>>();
            var queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(Tuple.Create(0, 0));
            enqueued.Add(Tuple.Create(0, 0));
            while (queue.Count > 0)
            {
                var visiting = queue.Dequeue();
                var candidates = new List<Tuple<int, int>>();
                candidates.Add(Tuple.Create(visiting.Item1 - 1, visiting.Item2));
                candidates.Add(Tuple.Create(visiting.Item1 + 1, visiting.Item2));
                candidates.Add(Tuple.Create(visiting.Item1, visiting.Item2 - 1));
                candidates.Add(Tuple.Create(visiting.Item1, visiting.Item2 + 1));
                foreach (var c in candidates)
                {
                    if (c.Item1 >= 0 && c.Item1 < bitmap.Width && c.Item2 >= 0 && c.Item2 < bitmap.Height && bitmap.GetPixel(c.Item1, c.Item2) == victim && !enqueued.Contains(c))
                    {
                        enqueued.Add(c);
                        queue.Enqueue(c);
                    }
                }
            }

            int left = bitmap.Width;
            int right = -1;
            int top = bitmap.Height;
            int bottom = -1;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    if (!enqueued.Contains(Tuple.Create(i, j)))
                    {
                        left = Math.Min(left, i);
                        right = Math.Max(right, i);
                        top = Math.Min(top, j);
                        bottom = Math.Max(bottom, j);
                    }
                }
            }

            Rectangle cropRect = new Rectangle(left, top, right - left + 1, bottom - top + 1);
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(bitmap, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
            }

            this.image = target;
            target.Save(@"c:\temp\out.png");
        }
    }
}
