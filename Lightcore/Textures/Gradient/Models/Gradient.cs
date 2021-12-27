namespace Lightcore.Textures.Gradients.Models
{
    using Lightcore.Common.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Gradient
    {
        public List<ColorPoint> ColorPoints { get; set; }

        public Gradient(Vector startColor, Vector endColor)
        {
            ColorPoints = new List<ColorPoint>();
            ColorPoints.Add(new ColorPoint(startColor, 0, true));
            ColorPoints.Add(new ColorPoint(endColor, 1, true));
        }

        public Vector GetColor(float value)
        {
            var match = ColorPoints.FirstOrDefault(c => c.Value.Equals(value));
            if (match != null)
                return match.Color;

            int i;
            var sortedColorPoints = ColorPoints.OrderBy(c => c.Value).ToArray();

            for (i = 0; i < sortedColorPoints.Length; i++)
            {
                if (sortedColorPoints[i].Value > value)
                    break;
            }

            if (i == 0)
                return sortedColorPoints[0].Color;

            var left = sortedColorPoints[i - 1];
            var right = sortedColorPoints[i];

            var percent = (value - left.Value) / (right.Value - left.Value);

            var red = left.Color[0] + (right.Color[0] - left.Color[0]) * percent;
            var green = left.Color[1] + (right.Color[1] - left.Color[1]) * percent;
            var blue = left.Color[2] + (right.Color[2] - left.Color[2]) * percent;

            return new Vector(red, green, blue);
        }
    }
}
