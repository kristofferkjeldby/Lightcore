namespace Lightcore.Textures.Entensions
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public static class ImageExtensions
    {

        public static Bitmap Crop(this Image image, double scale)
        {
            Bitmap src = image as Bitmap;

            Rectangle cropRect = new Rectangle(0, 0, image.Width, (int)(image.Width * scale));

            return new Bitmap(cropRect.Width, cropRect.Height);
        }

        public static Bitmap Darken(Image image, double factor)
        {
            ColorMatrix cm = new ColorMatrix(new float[][]
                {
                    new float[] {(float)factor, 0, 0, 0, 0},
                    new float[] {0, (float)factor, 0, 0, 0},
                    new float[] {0, 0, (float)factor, 0, 0},
                    new float[] {0, 0, 0, (float)factor, 0},
                    new float[] {0, 0, 0, 0, (float)factor},
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            Point[] points =
            {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };

            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the result bitmap.
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
            }

            return bm;
        }

        public static Bitmap Scale(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Low;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
