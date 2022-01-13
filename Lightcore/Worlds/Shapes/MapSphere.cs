namespace Lightcore.Worlds
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using Lightcore.Worlds.Extensions;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity MapSphere(Vector origon, float radius, Tuple<float, Vector>[,] map, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            if (renderMode?.Preview ?? false)
                map = map.Reduce(Settings.PreviewResolution);

            var polygons = new List<Polygon>();

            var tStepSize = Constants.PI / map.GetLength(1); 
            var pStepSize = Constants.PI2 / map.GetLength(0);
            var splitedMap = map.Split();

            for (int t = 0; t < map.GetLength(1); t++)
            {
                var t1 = (t == map.GetLength(1) - 1) ? 0 :  t + 1;

                for (int p = 0; p < map.GetLength(0); p++)
                {
                    var p1 = (p == map.GetLength(0) - 1) ? 0 : p + 1;

                    var vector00 = new Vector(radius + splitedMap[p, t].Item1, t * tStepSize, p * pStepSize);
                    var vector11 = new Vector(radius + splitedMap[p1, t1].Item1, (t + 1) * tStepSize, (p + 1) * pStepSize);
                    var vector01 = new Vector(radius + splitedMap[p1, t].Item1, t * tStepSize, (p + 1) * pStepSize) ;
                    var vector10 = new Vector(radius + splitedMap[p, t1].Item1, (t + 1) * tStepSize, p * pStepSize);

                    polygons.Add
                    (
                        new Polygon
                        (
                            texture(splitedMap[p, t].Item2),
                            new Vector[]
                            {
                                vector00,
                                vector11,
                                vector01
                            }
                        )
                    );

                    polygons.Add
                    (
                        new Polygon
                        (
                            texture(splitedMap[p, t].Item2),
                            new Vector[]
                            {
                                vector11,
                                vector00,
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
