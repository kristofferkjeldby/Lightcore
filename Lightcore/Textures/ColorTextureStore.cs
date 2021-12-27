namespace Lightcore.Textures
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;

    public static class ColorTextureStore
    {
        public static ColorTexture ColorTexture(Vector color)
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
    }
}
