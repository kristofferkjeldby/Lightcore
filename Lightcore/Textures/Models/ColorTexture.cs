namespace Lightcore.Textures.Models
{
    using System.Drawing;
    using Lightcore.Textures.Extensions;
    using Lightcore.Common.Models;

    public class ColorTexture : LightableTexture
    {
        public Vector ProcessedColor { get; set; }

        public Vector Color { get; set; }

        public double Transparency { get; set; }

        public double Metallicity { get; set; }

        public double Shiny { get; set; }

        public ColorTexture(Vector color, double reflection, double transparency, double metallicity, double shiny, Vector processedColor = null) : base(reflection)
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

        public override void Light(Vector otherColor, double factor)
        {
            ProcessedColor = ProcessedColor + (factor * (1-Metallicity) * otherColor & Color) + (factor * otherColor * Metallicity) ;
        }

        public override void Dark(double factor)
        {
            ProcessedColor = factor * ProcessedColor;
        }

        public override void Shine(Vector otherColor, double factor)
        {
            ProcessedColor = (factor * otherColor * Shiny) + ProcessedColor;
        }

        public override Texture Clone()
        {
            return new ColorTexture(Color, Reflection, Transparency, Metallicity, Shiny, ProcessedColor);
        }
    }
}
