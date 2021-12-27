namespace Lightcore.Textures.Models
{
    using System.Drawing;
    using Lightcore.Textures.Extensions;
    using Lightcore.Common.Models;

    public class ColorTexture : LightableTexture
    {
        public Vector ProcessedColor { get; set; }

        public Vector Color { get; set; }

        public float Transparency { get; set; }

        public float Metallicity { get; set; }

        public float Shiny { get; set; }

        public ColorTexture(Vector color, float reflection, float transparency, float metallicity, float shiny, Vector processedColor = null) : base(reflection)
        {
            Color = color;
            Reflection = reflection;
            Transparency = transparency;
            Metallicity = metallicity;
            Shiny = shiny;

            if (processedColor != null)
                ProcessedColor = processedColor;
            else
                ProcessedColor = new Vector(0, 0, 0);
        }

        public override Brush GetBrush()
        {
            return new SolidBrush(ProcessedColor.ToColor(Transparency));
        }

        public override void Light(Vector otherColor, float factor)
        {
            ProcessedColor = ProcessedColor + (factor * (1-Metallicity) * otherColor & Color) + (factor * otherColor * Metallicity) ;
        }

        public override void Dark(float factor)
        {
            ProcessedColor = factor * ProcessedColor;
        }

        public override void Shine(Vector otherColor, float factor)
        {
            ProcessedColor = (factor * otherColor * Shiny) + ProcessedColor;
        }

        public override Texture Clone()
        {
            return new ColorTexture(Color, Reflection, Transparency, Metallicity, Shiny, ProcessedColor);
        }
    }
}
