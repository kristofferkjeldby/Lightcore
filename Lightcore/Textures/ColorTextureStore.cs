namespace Lightcore.Textures
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;
    using System;

    public static class ColorTextureStore
    {
        public static ColorTexture NormalTexture(Vector color)
        {
            return new ColorTexture(color, 0.2f, 0, 0, 0.2f);
        }

        public static ColorTexture ShinyTexture(Vector color)
        {
            return new ColorTexture(color, 0.2f, 0, 0, 0.75f);
        }

        public static ColorTexture MetallicTexture(Vector color)
        {
            return new ColorTexture(color, 0.2f, 0, 0.5f, 0.75f);
        }

        public static SimpleColorTexture SimpleTexture(Vector color)
        {
            return new SimpleColorTexture(color);
        }

        public static Func<Vector, ColorTexture> ColorTexture(float reflection, float transparency, float metallicity, float shiny)
        {
            return (color) => new ColorTexture(color, 0.2f, 0, 0, 0.2f);
        }
    }
}
