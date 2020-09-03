namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using System;
    using System.Collections.Generic;

    public abstract class Light : IClonable<Light>, ITransformable, IIdentifiable
    {
        public Light(Vector color, Vector position, double strength, Guid? polygonId = null, Guid? id = null, int generation = 0)
        {
            Color = color;
            Position = position;
            Strength = strength;
            Generation = generation;
            PolygonId = polygonId.GetValueOrDefault(Guid.Empty);
            Id = id.GetValueOrDefault(Guid.Empty);
        }

        public double Strength { get; set; }

        public Vector Color { get; set; }

        public abstract Light Clone();

        public abstract void Transform(Func<Vector, Vector> transformation);

        public virtual Vector Position { get; set; }

        public int Generation { get; set; }

        public Guid PolygonId { get; set; }

        public List<LightMapElement> Shadows { get; set; }

        public Guid Id { get; set; }
    }
}
