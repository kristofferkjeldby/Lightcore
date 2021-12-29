namespace Lightcore.Lighting
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;

    public class DarkProcessor : Processor
    {
        public override Polygon PolygonProcessor(Polygon polygon, RenderArgs args)
        {
            if (!(polygon.Texture is LightableTexture lightableTexture))
                return polygon;

            lightableTexture.Dark(1 - (float)Math.Pow(polygon.Midpoint()[2] / Settings.ViewDistance, 2));
            return polygon;
        }

        public override ProcessorMetadata Metadata => new ProcessorMetadata("Dark processor", false, EntityType.World);
    }
}
