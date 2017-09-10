namespace ImageOverlay
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public static class BitmapExtensions
    {
        public static Image SetOpacity(this Image image, float opacity)
        {
            ColorMatrix colorMatrix = new ColorMatrix();
            colorMatrix.Matrix33 = opacity;
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            var output = new Bitmap(image.Width, image.Height);
            using (var gfx = Graphics.FromImage(output))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            }

            return output;
        }
    }
}
