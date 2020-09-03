namespace Lightcore.Textures.Models
{
    using System.Drawing;

    public class ImageTexture : Texture
    {
        public ImageTexture(ImageTextureType imageTextureType)
        {
            this.ImageTextureType = imageTextureType;
        }

        public ImageTextureType ImageTextureType { get; set; }

        public override Texture Clone()
        {
            return new ImageTexture(ImageTextureType);
        }

        public override Brush GetBrush()
        {
            return new TextureBrush(ImageTextureType.Image);
        }
    }
}
