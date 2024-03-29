﻿namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Models.Transformations;
    using System;

    public class BeamLight : DirectLight
    {
        public BeamLight(Vector color, Vector position, Vector direction, float strength, float width, Guid? origin = null, Guid? id = null, int generation = 0) 
            : base (color, position, direction, strength, origin, id, generation)
        {
            Width = width;
        }

        public float Width { get; set; }

        public override Light Clone()
        {
            return new BeamLight(Color, Position.Clone(), Direction.Clone(), Strength, Width, PolygonId, Id, Generation);
        }

        public override Light Transform(Transformation transformation)
        {
            Position = transformation.Transform(Position);
            Direction = transformation.Transform(Direction);

            return this;
        }
    }
}
