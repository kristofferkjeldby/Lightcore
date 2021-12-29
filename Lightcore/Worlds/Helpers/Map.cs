namespace Lightcore.Worlds.Helper
{
    using Lightcore.Common.Models;
    using System;

    public class MapHelper
    {
        public static Tuple<float, Vector>[,] CreateMap (int width, int height, Func<int, int, float> displacement, Func<int, int, Vector> color)
        {
            var result = new Tuple<float, Vector>[width, height];

            for (int x = 0; x < result.GetLength(0); x++)
            { 
                for (int y = 0; y < result.GetLength(1); y++)
                {
                    result[x, y] =
                        new Tuple<float, Vector>
                        (
                            displacement(x, y),
                            color(x, y)
                        );
                }
            }

            return result;
        }
    }
}
