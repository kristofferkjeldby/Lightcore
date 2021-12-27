namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using System;

    public class AngleLight : DirectLight
    {
        public AngleLight (Vector color, Vector position, Vector direction, float strength, float angle, Guid? origin = null, Guid? id = null, int generation = 0) : base (color, position, direction, strength, origin, id, generation)
        {
            Angle = angle;
        }

        public float Angle { get; set; }

        public override Light Clone()
        {
            return new AngleLight(Color, Position.Clone(), Direction.Clone(), Strength, Angle, PolygonId, Id, Generation);
        }

        public override Light Transform(Func<Vector, Vector> transformation)
        {
            Position = transformation(Position);
            Direction = transformation(Direction);

            return this;
        }

        public override Light Transform(Matrix transformation)
        {
            return Transform(a => transformation * a);
        }
    }
}
