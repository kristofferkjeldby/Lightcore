namespace Lightcore.Textures
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class ImageTextureStore
    {
        public static Dictionary<string, Bitmap> Bitmaps =
            new Dictionary<string, Bitmap>()
            {
                { "Checkerboard", Image.FromFile("./Textures/Images/Checkerboard.png") as Bitmap },
                { "Reacher", Image.FromFile("./Textures/Images/Reacher.jpg") as Bitmap},
                { "Oxygen", Image.FromFile("./Textures/Images/Oxygen.jpg") as Bitmap},
                { "Earth", Image.FromFile("./Textures/Images/Earth.jpg") as Bitmap},
                { "Cat", Image.FromFile("./Textures/Images/Cat.jpg") as Bitmap},
                { "Marble", Image.FromFile("./Textures/Images/Marble.jpg") as Bitmap },
                { "Abstract", Image.FromFile("./Textures/Images/Abstract.jpg") as Bitmap},
                { "Moon", Image.FromFile("./Textures/Images/Moon.png") as Bitmap },
                { "MarsColor", Image.FromFile("./Textures/Images/MarsColor.png") as Bitmap },
                { "MarsHeight", Image.FromFile("./Textures/Images/MarsHeight.png") as Bitmap},
                { "MarsAtmosphere", Image.FromFile("./Textures/Images/MarsAtmosphere.jpg") as Bitmap},
                { "Dots", Image.FromFile("./Textures/Images/Dots.jpg") as Bitmap},
                { "Leopard", Image.FromFile("./Textures/Images/Leopard.jpg") as Bitmap },
                { "Doughnut", Image.FromFile("./Textures/Images/Doughnut.jpg") as Bitmap },
                { "Test", Image.FromFile("./Textures/Images/Test.png")as Bitmap },
            };


        public static Bitmap GetImage(string name)
        {
            var image = Bitmaps[name].Clone() as Bitmap;
            image.RotateFlip(RotateFlipType.Rotate180FlipX);
            return image;
        }

        public static Func<Vector, SimpleImageTexture> SimpleImageTexture(string name)
        {
            return (color) => new SimpleImageTexture(GetImage(name));
        }

        public static ImageTextureBuilder TextureBuilder(string name)
        {
            return new ImageTextureBuilder(GetImage(name));
        }

    }
}
