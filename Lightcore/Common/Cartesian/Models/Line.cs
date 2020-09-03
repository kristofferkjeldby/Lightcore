namespace Lightcore.Common.Cartesian.Models
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;

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

        public Vector[] Vectors { get; set; }

        public Vector Origin => Vectors[0];

        public Vector End => Vectors[1];

        public Vector Direction => Vectors[1] - Vectors[0];

        public Line Unit()
        {
            return new Line(Origin, Origin + Direction.Unit());
        }

        public double Length
        {
            get {
                return Direction.Length();
            }
            set {
                Vectors[1] = Vectors[0] + Direction.Unit() * value;
            }
        }

        public Line Clone()
        {
            return new Line(Origin.Clone(), End.Clone());
        }
    }
}
