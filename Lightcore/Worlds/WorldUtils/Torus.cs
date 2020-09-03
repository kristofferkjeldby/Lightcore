namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Textures;
    using System;
    using System.Collections.Generic;

    public partial class WorldUtils
    {
        public static Entity Torus(EntityType entityType, Vector origon, double radius1, double radius2, Tuple<double, Vector>[,] map)
        {
            var polygons = new List<Polygon>();

            var cStepSize = Constants.PI2 / map.GetLength(0);
            var pStepSize = Constants.PI2 / map.GetLength(1); 

            for (int c0 = 0; c0 < map.GetLength(0); c0++)
            {
                var c1 = (c0 == map.GetLength(0) - 1) ? 0 :  c0 + 1;

                var cc0 = new Vector(Math.Cos(c0 * cStepSize), Math.Sin(c0 * cStepSize), 0);
                var cc1 = new Vector(Math.Cos(c1 * cStepSize), Math.Sin(c1 * cStepSize), 0);

                var cc0r = CartesianUtils.Rotate(cc0 % Settings.Unit[Axis.Z], pStepSize);
                var cc1r = CartesianUtils.Rotate(cc1 % Settings.Unit[Axis.Z], pStepSize);

                var cc0pp0 = cc0 * radius2;
                var cc1pp0 = cc1 * radius2;

                for (int p0 = 0; p0 < map.GetLength(1); p0++)
                {
                    var p1 = (p0 == map.GetLength(1) - 1) ? 0 : p0 + 1;

                    var cc0pp1 = cc0r * cc0pp0;
                    var cc1pp1 = cc1r * cc1pp0;

                    var vectorcc0pp0 = (cc0 * radius1) + cc0pp0 + (cc0pp0.Unit() * map[c0, p0].Item1);
                    var vectorcc1pp1 = (cc1 * radius1) + cc1pp1 + (cc1pp1.Unit() * map[c1, p1].Item1);
                    var vectorcc0pp1 = (cc0 * radius1) + cc0pp1 + (cc0pp1.Unit() * map[c0, p1].Item1);
                    var vectorcc1pp0 = (cc1 * radius1) + cc1pp0 + (cc1pp0.Unit() * map[c1, p0].Item1);

                    polygons.Add
                    (
                        new Polygon
                        (
                            ColorTextureStore.Get(map[c0, p0].Item2),
                            new Vector[]
                            {
                                vectorcc0pp0,
                                vectorcc1pp1,
                                vectorcc0pp1
                            }
                        )
                    );

                    polygons.Add
                    (
                        new Polygon
                        (
                            ColorTextureStore.Get(map[c0, p0].Item2),
                            new Vector[]
                            {
                                vectorcc1pp1,
                                vectorcc0pp0,
                                vectorcc1pp0
                            }
                        )
                    );

                    cc0pp0 = cc0pp1;
                    cc1pp0 = cc1pp1;
                }
            }

            return new Entity(entityType, polygons.ToArray());
        }
    }
}
