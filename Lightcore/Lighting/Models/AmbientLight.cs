namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Models.Transformations;
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

        public override Light Transform(Transformation transformation)
        {
            Position = transformation.Transform(Position);

            return this;
        }
    }
}
