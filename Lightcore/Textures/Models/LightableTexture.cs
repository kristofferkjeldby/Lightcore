namespace Lightcore.Textures.Models
{
    using Lightcore.Common.Models;

    public abstract class LightableTexture : Texture
    {
        public LightableTexture(float reflection)
        {
            Reflection = reflection;
        }

        public float Reflection { get; set; }

        public abstract void Light(Vector otherColor, float factor);

        public abstract void Shine(Vector otherColor, float factor);

        public abstract void Dark(float factor);
    }
}
