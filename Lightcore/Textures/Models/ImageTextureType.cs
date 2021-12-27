namespace Lightcore.Textures.Models
{
    using System.Drawing;

    public class ImageTextureType
    {
        public ImageTextureType(Image image)
        {
            this.Image = image;
        }

        public Image Image { get; set; }

        public Bitmap Bitmap => new Bitmap(Image);

        public float MaxLength { get; set; }
    }
}
