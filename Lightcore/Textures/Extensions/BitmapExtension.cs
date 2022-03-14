namespace Lightcore.Textures.Extensions
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;

    public static class BitmapExtensions
    {
        public static Bitmap Crop(this Bitmap image, RectangleF rect)
        {
            if (rect.Right > image.Width - 0.05)
            {
                rect.Width = (image.Width - 0.05f) - rect.X;
            }

            if (rect.Top > image.Height - 0.05)
            {
                rect.Height = (image.Height - 0.05f) - rect.Y;
            }

            return image.Clone(rect, image.PixelFormat);
        }

        public static Bitmap Colorize(this Bitmap image, Vector color, float transparency)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] {color[0], 0, 0, 0, 0},
                    new float[] {0, color[1], 0, 0, 0},
                    new float[] {0, 0, color[2], 0, 0},
                    new float[] {0, 0, 0, 1-transparency, 0},
                    new float[] {0, 0, 0, 0, 1},
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            Point[] points =
            {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };

            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the resulting bitmap
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
            }

            return bm;
        }

        public static Bitmap ForwardMap(this Bitmap image, PointF[] points)
        {
            var maxX = points.Max(point => (int)point.X);
            var maxY = points.Max(point => (int)point.Y);
            var minX = points.Min(point => (int)point.X);
            var minY = points.Min(point => (int)point.Y);

            // To prevent the forward mapping in creating hole, a lot of oversampling is introduced here
            var uStepSize = (Settings.TextureOversampling / (maxX - minX));
            var vStepSize = (Settings.TextureOversampling / (maxY - minY));

            var destinationImage = new Bitmap(maxX, maxY);

            var transformation = ImageUtils.SquareToQuadrilateralTransformation(points.Select(p => p.ToVector()).ToArray());

            for (float v = 0; v < 1; v += vStepSize)
            {
                var imageV = (int)(v * image.Height);

                for (float u = 0; u < 1; u += uStepSize)
                {
                    var color = image.GetPixel((int)(u * image.Width), imageV);
                    var xy = transformation.Transform(new Vector(u, v));
                    destinationImage.SetPixel(xy, color);
                }
            }

            return destinationImage;
        }

        public static void SetPixel(this Bitmap image, Vector vector, Color color)
        {
            var point = vector.ToPoint();

            var x = CommonUtils.ToInt(point.X);
            var y = CommonUtils.ToInt(point.Y);

            if (x < image.Width && y < image.Height && x >= 0 && y >= 0)
                image.SetPixel(x, y, color);
        }
    }
}
