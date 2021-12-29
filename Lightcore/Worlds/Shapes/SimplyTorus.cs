namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;

    public partial class Shapes
    {
        public static Entity SimpleTorus(Vector color, Vector origon, float radius1, float radius2, int segments1, int segments2, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            if (renderMode?.Preview ?? false)
                segments1 = segments2 = Settings.PreviewResolution;

            var polygons = new List<Polygon>();

            var cStepSize = Constants.PI2 / segments1;
            var pStepSize = Constants.PI2 / segments2; 

            for (int c0 = 0; c0 < segments1; c0++)
            {
                var c1 = (c0 == segments1 - 1) ? 0 :  c0 + 1;

                var cc0 = new Vector((float)Math.Cos(c0 * cStepSize), (float)Math.Sin(c0 * cStepSize), 0);
                var cc1 = new Vector((float)Math.Cos(c1 * cStepSize), (float)Math.Sin(c1 * cStepSize), 0);

                var cc0r = CartesianUtils.Rotate(cc0 % Settings.Unit[Axis.Z], pStepSize);
                var cc1r = CartesianUtils.Rotate(cc1 % Settings.Unit[Axis.Z], pStepSize);

                var cc0pp0 = cc0 * radius2;
                var cc1pp0 = cc1 * radius2;

                for (int p0 = 0; p0 < segments2; p0++)
                {
                    var p1 = (p0 == segments2 - 1) ? 0 : p0 + 1;

                    var cc0pp1 = cc0r * cc0pp0;
                    var cc1pp1 = cc1r * cc1pp0;

                    var vectorcc0pp0 = (cc0 * radius1) + cc0pp0 + (cc0pp0.Unit());
                    var vectorcc1pp1 = (cc1 * radius1) + cc1pp1 + (cc1pp1.Unit());
                    var vectorcc0pp1 = (cc0 * radius1) + cc0pp1 + (cc0pp1.Unit());
                    var vectorcc1pp0 = (cc1 * radius1) + cc1pp0 + (cc1pp0.Unit());

                    polygons.Add
                    (
                        new Polygon
                        (
                            texture(color),
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
                            texture(color),
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

            return new Entity(EntityType.World, polygons.ToArray());
        }
    }
}
