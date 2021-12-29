namespace Lightcore.Debug
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Cartesian.Models;
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Textures.Models;
    using System.Collections.Generic;
    using System.Drawing;
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
                foreach (var polygon in entity.Elements.Where(polygon => polygon.PolygonType == PolygonType.Triangle))
                {
                    var normal = Line.Create(polygon.Midpoint(), polygon.Normal());
                    normal.Length = 10;

                    normals.Add
                    (
                        new Entity
                        (
                            EntityType.Debug,
                            normal.ToPolygon(new SimpleTexture(Color.Gray.ToVector()))
                        )
                    );
                }
            }

            args.World.Entities.AddRange(normals);
        }
    }
}
