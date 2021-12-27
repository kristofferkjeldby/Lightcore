namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using System;

    public class AmbientLight : Light
    {
        public AmbientLight(Vector color, Vector position, float strength, Guid? origin = null, Guid? id = null, int generation = 0) : base(color, position, strength, origin, id, generation)
        {
        }

        public override Light Clone()
        {
            return new AmbientLight(Color, Position.Clone(), Strength, PolygonId, Id, Generation);
        }

        public override Light Transform(Func<Vector, Vector> transformation)
        {
            Position = transformation(Position);

            return this;
        }

        public override Light Transform(Matrix transformation)
        {
            return Transform(a => transformation * a);
        }
    }
}
