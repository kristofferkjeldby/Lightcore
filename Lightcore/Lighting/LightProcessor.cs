namespace Lightcore.Lighting
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LightProcessor : Processor
    {
        public override ProcessorMetadata Metadata => new ProcessorMetadata("Light processor", false, EntityType.Preview, EntityType.World);
        private LightMap lightMap;
        private readonly int generation;
        private List<Light> lights;

        public LightProcessor(int generation = 0)
        {
            this.generation = generation;
        }

        public override void PreProcessor(RenderArgs args)
        {
            if (args.RenderMetadata.Metadata.ContainsKey("LightMap"))
            {
                lightMap = args.RenderMetadata.Metadata["LightMap"] as LightMap;
            }

            this.lights = args.World.Lights.Where(light => light.Generation == generation).ToList();
        }

        public override Polygon PolygonProcessor(Polygon polygon, RenderArgs args)
        {
            if (!(polygon.Texture is LightableTexture lightableTexture))
                return polygon;

            foreach (var light in lights)
            {
                var visibility = lightMap?.GetVisibility(light, polygon) ?? 1;

                var reflection = LightUtils.Apply(polygon, lightableTexture, light, Settings.ReflectionTreadshold, visibility);

                if (reflection != null)
                {
                    args.World.Lights.Add(reflection);
                }
            }

            return polygon;
        }

        public override void PostProcessor(RenderArgs args)
        {
            if (args.RenderMetadata.Metadata.ContainsKey("LightMap"))
            {
                args.RenderMetadata.Metadata["LightMap"] = null;
            }

            this.lights = args.World.Lights.Where(light => light.Generation == generation).ToList();
        }
    }
}

