namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using System;

    public class BeamLight : DirectLight
    {
        public BeamLight(Vector color, Vector position, Vector direction, double strength, double width, Guid? origin = null, Guid? id = null, int generation = 0) 
            : base (color, position, direction, strength, origin, id, generation)
        {
            Width = width;
        }

        public double Width { get; set; }

        public override Light Clone()
        {
            return new BeamLight(Color, Position.Clone(), Direction.Clone(), Strength, Width, PolygonId, Id, Generation);
        }

        public override void Transform(Func<Vector, Vector> transformation)
        {
            Position = transformation(Position);
            Direction = transformation(Direction);
        }
    }
}
