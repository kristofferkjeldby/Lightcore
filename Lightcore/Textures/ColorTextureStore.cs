namespace Lightcore.Textures
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;

    public static class ColorTextureStore
    {
        public static ColorTexture Get(Vector color)
        {
            return new ColorTexture(color, 0.2f, 0, 0, 0.75f);
        }
    }
}
