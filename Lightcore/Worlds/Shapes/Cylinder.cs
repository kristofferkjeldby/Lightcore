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
        public static Entity Cylinder(RenderMode renderMode, Vector origin, float radius, float height, Tuple<float, Vector>[,] map, Func<Vector, Texture> texture)
        {
            if (renderMode.Preview)
                map = map.Reduce(Settings.PreviewResolution);

            var polygons = new List<Polygon>();

            var xAngleStepSize = 2 * Constants.PI / map.GetLength(0);
            var yStepSize = height / map.GetLength(1);

            var xOffset = origin[0];
            var yOffset = origin[1] - 0.5f * height;
            var zOffset = origin[2];

            for (int y = 0; y < map.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    var nextX = (x == map.GetLength(0)-1) ?  0 : x;

                    var x0Factor = CommonUtils.Cos(xAngleStepSize * x);
                    var z0Factor = CommonUtils.Sin(xAngleStepSize * x);

                    var x1Factor = CommonUtils.Cos(xAngleStepSize * (x + 1));
                    var z1Factor = CommonUtils.Sin(xAngleStepSize * (x + 1));

                    var vector0 = new Vector(
                                x0Factor * (radius+map[x,y].Item1) + xOffset,
                                yStepSize * y + yOffset,
                                z0Factor * (radius+map[x,y].Item1) + zOffset);

                    var vector2 = new Vector(
                                x1Factor * (radius + map[nextX, y].Item1) + xOffset,
                                yStepSize * y + yOffset,
                                z1Factor * (radius + map[nextX, y].Item1) + zOffset);

                    var vector1 = new Vector(
                                vector0[0],
                                yStepSize * (y + 1) + yOffset,
                                z0Factor * (radius + map[x, y+1].Item1) + zOffset);


                    polygons.AddRange(
                        Square
                        (
                            vector0,
                            vector1 - vector0,
                            vector2 - vector0,
                            texture(map[x, y].Item2)
                        ).Elements
                    );
                }
            }

            return new Entity(EntityType.World, polygons.ToArray());
        }
    }
}
