using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Lightcore.Textures.Models
{
    unsafe public class UnsafeImageWrapper
    {
        private struct PixelData
        {
            public byte Blue { get; set; }
            public byte Green { get; set; }
            public byte Red { get; set; }
            public byte Alpha { get; set; }
    }

        private Bitmap Image { get; set; }

        private int width = 0;

        private BitmapData ImageData { get; set; }

        private Byte* pBase = null;

        public UnsafeImageWrapper(Bitmap inputBitmap)
        {
            Image = inputBitmap;
        }

        public void LockImage()
        {
            //Size
            Rectangle bounds = new Rectangle(Point.Empty, Image.Size);

            width = (int)(bounds.Width * sizeof(PixelData));
            if (width % 4 != 0) width = 4 * (width / 4 + 1);

            //Lock Image
            ImageData = Image.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            pBase = (byte*)ImageData.Scan0.ToPointer();
        }

        private PixelData* pixelData = null;

        public Color GetPixel(int x, int y)
        {
            pixelData = (PixelData*)(pBase + y * width + x * sizeof(PixelData));
            return Color.FromArgb(pixelData->Alpha, pixelData->Red, pixelData->Green, pixelData->Blue);
        }

        public Color GetPixelNext()
        {
            pixelData++;
            return Color.FromArgb(pixelData->Alpha, pixelData->Red, pixelData->Green, pixelData->Blue);
        }

        public void SetPixel(int x, int y, Color color)
        {
            PixelData* data = (PixelData*)(pBase + y * width + x * sizeof(PixelData));
            data->Alpha = color.A;
            data->Green = color.G;
            data->Blue = color.B;
            data->Red = color.R;
        }

        public void UnlockImage()
        {
            Image.UnlockBits(ImageData);
            ImageData = null;
            pBase = null;
        }
    }
}
