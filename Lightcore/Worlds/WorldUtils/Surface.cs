namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;

    public partial class WorldUtils
    {
        public static Entity Surface(EntityType entityType, Vector origin, float width, float height, Tuple<float, Vector>[,] map, Func<Vector, Texture> texture)
        {
            var polygons = new List<Polygon>();

            var xStepSize = width / map.GetLength(0);
            var yStepSize = height / map.GetLength(0);

            var xOffset = origin[0] - width / 2;
            var yOffset = origin[1] - height / 2;
            var zOffset = origin[2];

            for (int x = 0; x < map.GetLength(0) - 1; x++)
            {
                for (int y = 0; y < map.GetLength(1) - 1; y++)
                {
                    polygons.Add(
                        new Polygon
                        (
                            texture(map[x, y].Item2),
                            new Vector[] {
                            new Vector(xOffset + xStepSize * x, yOffset + yStepSize * y, zOffset + map[x, y].Item1),
                            new Vector(xOffset + xStepSize * (x+1), yOffset + yStepSize * y, zOffset + map[x+1, y].Item1),
                            new Vector(xOffset + xStepSize * x, yOffset + yStepSize * (y+1), zOffset + map[x, y+1].Item1)
                            }
                        )
                    );

                    polygons.Add(
                        new Polygon
                        (
                            texture(map[x, y].Item2),
                            new Vector[] 
                            {
                                new Vector(xOffset + xStepSize * (x+1), yOffset + yStepSize * (y+1), zOffset + map[x+1, y+1].Item1),
                                new Vector(xOffset + xStepSize * x, yOffset + yStepSize * (y+1), zOffset + map[x, y+1].Item1),
                                new Vector(xOffset + xStepSize * (x+1), yOffset + yStepSize * y, zOffset + map[x+1, y].Item1)
                            }
                         )
                    );
                }
            }

            return new Entity(entityType, polygons.ToArray());
        }
    }
}
