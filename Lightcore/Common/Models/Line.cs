namespace Lightcore.Common.Models
{
    public class Line : IClonable<Line>
    {
        public Line(Vector origin, Vector end)
        {
            Vectors = new Vector[2];
            Vectors[0] = origin;
            Vectors[1] = end;
        }

        public static Line Create(Vector origin, Vector direction)
        {
            return new Line(origin, origin + direction);
        }

        public static Line Create2D(Vector origin, float slope)
        {
            return new Line(origin, origin + new Vector(1, slope));
        }

        public Vector[] Vectors { get; set; }

        public Vector Origin => Vectors[0];

        public Vector End => Vectors[1];

        public Vector Direction => Vectors[1] - Vectors[0];

        public Line Clone()
        {
            return new Line(Origin.Clone(), End.Clone());
        }
    }
}
