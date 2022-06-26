namespace Lightcore.Lighting
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShineProcessor : Processor
    {
        public override ProcessorMetadata Metadata => new ProcessorMetadata("Shine processor", false, EntityType.World);
        private Vector viewer;
        private Dictionary<Guid, List<Light>> reflections;

        public override void PreProcessor(RenderArgs args)
        {
            viewer = args.Camera.ReferenceFrame.Origon - Settings.ViewDistance * args.Camera.ReferenceFrame.Base[Axis.Z];
            reflections = args.World.Lights.Where(light => light.Generation != 0).GroupBy(light => light.PolygonId).ToDictionary(group => group.Key, group => group.ToList());
        }

        public override Polygon PolygonProcessor(Polygon polygon, RenderArgs args)
        {
            if (!(polygon.Texture is LightableTexture lightableTexture))
                return polygon;

            if (reflections.ContainsKey(polygon.Id))
            { 
                foreach (var reflection in reflections[polygon.Id])
                {
                    var factor = LightUtils.GetDirectFactor(viewer, reflection);

                    if (factor > 0)
                        lightableTexture.Shine(reflection.Color, factor);
                }
            }

            return polygon;
        }
    }
}

