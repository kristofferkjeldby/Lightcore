namespace Lightcore.Worlds
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity SimpleSphere(Vector color, Vector origon, float radius, int segments, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            if (renderMode?.Preview ?? false)
                segments = Settings.PreviewResolution;

            var polygons = new List<Polygon>();

            var tStepSize = Constants.PI / segments; 
            var pStepSize = Constants.PI2 / segments;

            for (int t = 0; t < segments; t++)
            {
                var t1 = t + 1;

                for (int p = 0; p < segments; p++)
                {
                    var p1 = p + 1;

                    var vector00 = new Vector(radius, t * tStepSize, p * pStepSize);
                    var vector11 = new Vector(radius, t1 * tStepSize, p1 * pStepSize);
                    var vector01 = new Vector(radius, t * tStepSize, p1 * pStepSize) ;
                    var vector10 = new Vector(radius, t1 * tStepSize, p * pStepSize);

                    polygons.Add
                    (
                        new Polygon
                        (
                            texture(color),
                            new Vector[]
                            {
                                vector00,
                                vector10,
                                vector01
                            }
                        )
                    );

                    polygons.Add
                    (
                        new Polygon
                        (
                            texture(color),
                            new Vector[]
                            {
                                vector11,
                                vector01,
                                vector10

                            }
                        )
                    );
                }
            }

            var source = new ReferenceFrame(
                new Matrix(
                    new Vector(0, 0, 1),
                    new Vector(1, 0, 0),
                    new Vector(0, 1, 0)
                    ),
                Settings.Origon,
                ReferenceFrameType.Spherical);

            var destination = new ReferenceFrame(Settings.Unit, -origon, ReferenceFrameType.Cartesian);
            var transformation = CommonUtils.ReferenceFrameTransformation(source, destination);

            polygons.ForEach(polygon => polygon.Transform(transformation));

            return new Entity(EntityType.World, polygons.ToArray());
        }
    }
}
