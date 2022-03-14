namespace Lightcore.Debug
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class NormalProcessor : Processor
    {
        public override ProcessorMetadata Metadata => new ProcessorMetadata("Normal processor", true, EntityType.World);

        public override void PostProcessor(RenderArgs args)
        {
            List<Entity> normals = new List<Entity>();

            var entities = args.World.Entities.Where(entity => EntityPredicate(entity, args));

            foreach (var entity in entities)
            {
                foreach (var polygon in entity.Elements.Where(polygon => polygon.PolygonType != PolygonType.Line))
                {
                    var origin = polygon.Midpoint();
                    var end = origin + (polygon.Normal().Unit() * 10);

                    normals.Add
                    (
                        new Entity
                        (
                            EntityType.Debug,
                            new Polygon(
                                Settings.DebugTexture,
                                new[] { origin, end }
                            )
                        )
                    );
                }
            }

            args.World.Entities.AddRange(normals);
        }
    }
}
