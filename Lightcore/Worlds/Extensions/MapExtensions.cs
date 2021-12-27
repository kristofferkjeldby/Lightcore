namespace Lightcore.Worlds.Extensions
{
    using Lightcore.Common.Models;
    using System;

    public static class MapExtensions
    {
        public static Tuple<float, Vector>[,] Reduce(this Tuple<float, Vector>[,] map, int width)
        {
            var height = (int)(map.GetLength(1) * ((float)width / map.GetLength(0)));

            var xStepSize = map.GetLength(0) / width;

            var yStepSize = (int)map.GetLength(1) / height;

            var reducedMap = new Tuple<float, Vector>[width, height];

            for (int x = 0; x < reducedMap.GetLength(0); x++)
            {
                for (int y = 0; y < reducedMap.GetLength(1); y++)
                {
                    reducedMap[x, y] = map[x * xStepSize, y * yStepSize];
                }
            }

            return reducedMap;
        }

        public static Tuple<float, Vector>[,] Split(this Tuple<float, Vector>[,] map)
        {

            var splitedMap = new Tuple<float, Vector>[map.GetLength(0), map.GetLength(1)];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                var newX = ((map.GetLength(0) / 2) + x) % map.GetLength(0);

                for (int y = 0; y < splitedMap.GetLength(1); y++)
                {
                    splitedMap[x, y] = map[newX, y];
                }
            }

            return splitedMap;
        }

        public static int Size(this Tuple<float, Vector>[,] map)
        {
            return map.GetLength(0) * map.GetLength(1);
        }
    }
}
