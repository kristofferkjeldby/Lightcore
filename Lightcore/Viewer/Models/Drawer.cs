namespace Lightcore.View.Models
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class Drawer : IDisposable
    {
        public Drawer(PictureBox pictureBox, string filename = null)
        {
            PictureBox = pictureBox;
            Filename = filename;
            Bitmap = new Bitmap(PictureBox.Width, PictureBox.Height, PixelFormat.Format32bppArgb);
            Graphics = Graphics.FromImage(Bitmap);
            Graphics.Clear(Color.Black);
        }

        public Graphics Graphics { get; set; }
        public string Filename { get; set; }
        public PictureBox PictureBox { get; set; }
        public Bitmap Bitmap { get; set; }


        public void Dispose()
        {
            if (!String.IsNullOrEmpty(Filename))
                Bitmap.Save(Filename, ImageFormat.Png);
            PictureBox.Image = Bitmap;
            Graphics.Dispose();
        }
    }
}
