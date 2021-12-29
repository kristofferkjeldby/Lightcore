namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using Lightcore.Worlds.Extensions;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity Surface(Vector origin, Vector axis1, Vector axis2, Tuple<float, Vector>[,] map, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            if (renderMode?.Preview ?? false)
                map = map.Reduce(Settings.PreviewResolution);

            var normal = (axis1 % axis2).Unit();

            var polygons = new List<Polygon>();

            var xStep = axis1 / map.GetLength(0);
            var yStep = axis2 / map.GetLength(1);

            for (int x = 0; x < map.GetLength(0) - 1; x++)
            {
                for (int y = 0; y < map.GetLength(1) - 1; y++)
                {
                    var guid = Guid.NewGuid();

                    var start = origin + xStep * x + yStep * y;

                    polygons.Add(
                        new Polygon
                        (
                            texture(map[x, y].Item2),
                            new Vector[] {
                                start + map[x, y].Item1 * normal,
                                start + xStep + map[x+1, y].Item1 * normal,
                                start + yStep + map[x, y+1].Item1 * normal,
                            }
                            ,
                            guid
                        )
                    );

                    polygons.Add(
                        new Polygon
                        (
                            texture(map[x, y].Item2),
                            new Vector[]
                            {
                                start + xStep + yStep + map[x+1, y+1].Item1 * normal,
                                start + yStep + map[x, y+1].Item1 * normal,
                                start + xStep + map[x+1, y].Item1 * normal,
                            },
                            guid
                         )
                    );
                }
            }

            return new Entity(EntityType.World, polygons.ToArray());
        }
    }
}
