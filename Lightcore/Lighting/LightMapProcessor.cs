namespace Lightcore.Lighting
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using System.Linq;

    public class LightMapProcessor : Processor
    {
        public override ProcessorMetadata Metadata => new ProcessorMetadata("Lightmap processor", false, EntityType.World);

        public override void PreProcessor(RenderArgs args)
        {
            var lightMap = new LightMap();

            for (int i = 0; i < args.World.Lights.Count; i++)
            {
                var light = args.World.Lights[i];
                var lightMapElements = new LightMapSearchList();
                var transformation = CommonUtils.ReferenceFrameTransformation(args.World.ReferenceFrame, new ReferenceFrame(Settings.Unit, light.Position, ReferenceFrameType.Spherical));

                var polygons =
                    args.World.Entities.
                    Where(entity => EntityPredicate(entity, args)).
                    SelectMany(entity => entity.Elements).
                    Where(polygon => polygon.PolygonType != PolygonType.Line).
                    Where(polygon => LightUtils.GetFactor(polygon, light) > 0).
                    Select(polygon => polygon.Clone()).
                    Select(polygon => { polygon.Transform(transformation); return polygon; }).
                    OrderBy(polygon => polygon.Elements.Min(vector => vector[Axis.R])).ToList();

                for (int j = 0; j < polygons.Count(); j++)
                {
                    args.Status($"{Metadata.Name}: Processing light {i + 1} of {polygons.Count()}, polygon {j + 1} of {polygons.Count} ...");
                    var lightMapElement = new LightMapElement(light, polygons[j]);
                    LightUtils.Visibility(lightMapElement, lightMapElements.Get(lightMapElement));
                    lightMapElements.Add(lightMapElement);
                }

                lightMap.Add(lightMapElements);
            }

            args.RenderMetadata.Metadata.Add("LightMap", lightMap);
        }
    }
}

