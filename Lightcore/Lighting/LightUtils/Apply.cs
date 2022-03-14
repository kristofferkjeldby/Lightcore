namespace Lightcore.Lighting
{
    using Lightcore.Common.Models;
    using System;
    using Lightcore.Textures.Models;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Cartesian;
    using Lightcore.Lighting.Models;

    public static partial class LightUtils
    {
        public static Light Apply(Polygon polygon, LightableTexture texture, Light light, float reflectionThreadshold, float visibility)
        {
            var direction = polygon.Midpoint() - light.Position;

            var factor = GetFactor(polygon, light);

            factor *= visibility;

            if (factor <= 0)
                return null;

            texture.Light(light.Color, factor);

            if (texture.Reflectance > 0 && visibility >= 1 && factor * texture.Reflectance > reflectionThreadshold)
            {
                return new AngleLight
                (
                    light.Color,
                    polygon.Midpoint(),
                    CartesianUtils.Reflect(direction, polygon.Normal()),
                    factor * texture.Reflectance,
                    Constants.PI / 6f,
                    polygon.Id,
                    null,
                    light.Generation + 1
                );
            }

            return null;

        }

        public static float GetDirectFactor(Vector position, Light light)
        {
            if (!(light is DirectLight directLight))
                return 1;

            var distance = CartesianUtils.Distance(Line.Create(directLight.Position, directLight.Direction), position);

            switch (light)
            {
                case BeamLight beamLight:
                    return Math.Max(0, 1 - distance/beamLight.Width);
                case AngleLight angleLight:
                    var width = angleLight.Angle * Math.Max(0, (position - angleLight.Position) * angleLight.Direction.Unit());
                    return Math.Max(0, 1 - distance/width);
            }

            return 1;
        }

        public static float GetAmbientFactor(Polygon polygon, Light light)
        {
            var direction = polygon.Midpoint() - light.Position;

            if (direction.Length() == 0)
                return 0;

            return (1 / (float)Math.Pow(direction.Length() / light.Strength, 2) * (polygon.Normal().Unit() * -direction.Unit()));
        }

        public static float GetFactor(Polygon polygon, Light light)
        {
            return GetAmbientFactor(polygon, light) * GetDirectFactor(polygon.Midpoint(), light);
        }
    }
}
