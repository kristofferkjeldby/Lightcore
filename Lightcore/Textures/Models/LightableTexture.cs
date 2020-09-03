namespace Lightcore.Textures.Models
{
    using Lightcore.Common.Models;

    public abstract class LightableTexture : Texture
    {
        public LightableTexture(double reflection)
        {
            Reflection = reflection;
        }

        public double Reflection { get; set; }

        public abstract void Light(Vector otherColor, double factor);

        public abstract void Shine(Vector otherColor, double factor);

        public abstract void Dark(double factor);
    }
}
