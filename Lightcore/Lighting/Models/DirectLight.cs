namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using System;

    public abstract class DirectLight : Light
    {
        public DirectLight(Vector color, Vector position, Vector direction, double strength, Guid? origin = null, Guid? id = null, int generation = 0) 
            : base (color, position, strength, origin, id, generation)
        {
            Direction = direction;
        }

        public Vector Direction { get; set; }
    }
}
