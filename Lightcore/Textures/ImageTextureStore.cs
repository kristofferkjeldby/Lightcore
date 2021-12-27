namespace Lightcore.Textures
{
    using Lightcore.Textures.Models;
    using System.Collections.Generic;
    using System.Drawing;

    public static class ImageTextureStore
    {
        public static Dictionary<string, ImageTextureType> ImageTexturesTypes =
            new Dictionary<string, ImageTextureType>()
            {
                { "Checkerboard", new ImageTextureType(Image.FromFile("./Textures/Images/Checkerboard.png")) },
                { "Reacher", new ImageTextureType(Image.FromFile("./Textures/Images/Reacher.jpg")) },
                { "Oxygen", new ImageTextureType(Image.FromFile("./Textures/Images/Oxygen.jpg")) },
                { "Earth", new ImageTextureType(Image.FromFile("./Textures/Images/Earth.jpg")) },
                { "Cat", new ImageTextureType(Image.FromFile("./Textures/Images/Cat.jpg")) },
                { "Marble", new ImageTextureType(Image.FromFile("./Textures/Images/Marble.jpg")) },
                { "Abstract", new ImageTextureType(Image.FromFile("./Textures/Images/Abstract.jpg")) },
                { "Moon", new ImageTextureType(Image.FromFile("./Textures/Images/Moon.png")) },
                { "MarsColor", new ImageTextureType(Image.FromFile("./Textures/Images/MarsColor.jpg")) },
                { "MarsHeight", new ImageTextureType(Image.FromFile("./Textures/Images/MarsHeight.jpg")) },
                { "Dots", new ImageTextureType(Image.FromFile("./Textures/Images/Dots.jpg")) },
                { "Leopard", new ImageTextureType(Image.FromFile("./Textures/Images/Leopard.jpg")) },
                { "Doughnut", new ImageTextureType(Image.FromFile("./Textures/Images/Doughnut.jpg")) },
            };


        public static ImageTexture Get(string name)
        {
            return new ImageTexture(ImageTexturesTypes[name]);
        }
    }
}
