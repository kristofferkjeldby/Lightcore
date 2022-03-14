namespace Lightcore.Textures.Extensions
{
    using Lightcore.Common.Models;

    public static class LineExtensions
    {
        public static Angle Angle2D(this Line line)
        {
            return line.Direction.Angle2D();
        }

        public static float Slope2D(this Line line)
        {
            return line.Direction.Slope2D();
        }

        public static Vector Intersection(this Line line, Line otherLine)
        {
            var lineSlope = line.Slope2D();
            var otherLineSlope = otherLine.Slope2D();

            if (line.Origin[Axis.X] == otherLine.Origin[Axis.X] && line.Origin[Axis.Y] == otherLine.Origin[Axis.Y])
                return line.Origin;

            if (lineSlope == otherLineSlope)
                return null;

            float x = float.Epsilon;
            float y = float.Epsilon;

            if (float.IsInfinity(lineSlope))
            {
                x = line.Origin[Axis.X];
                y = otherLine.Origin[Axis.Y] + otherLineSlope * (-otherLine.Origin[Axis.X] + line.Origin[Axis.X]);
            }

            if (float.IsInfinity(otherLineSlope))
            {
                x = otherLine.Origin[Axis.X];
                y = line.Origin[Axis.Y] + lineSlope * (-line.Origin[Axis.X] + otherLine.Origin[Axis.X]);
            }

            if (x == float.Epsilon)
            {
                float q1 = line.Origin[Axis.Y] - lineSlope * line.Origin[Axis.X];
                float q2 = otherLine.Origin[Axis.Y] - otherLineSlope * otherLine.Origin[Axis.X];
                x = (q1 - q2) / (otherLineSlope - lineSlope);
                y = lineSlope * x + q1;
            }

            if (float.IsInfinity(x) || float.IsInfinity(y))
                return null;
            else
            {
                return new Vector(x, y);
            }
        }
    }
}
