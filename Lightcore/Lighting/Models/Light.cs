namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Models.Transformations;
    using System;
    using System.Collections.Generic;

    public abstract class Light : IClonable<Light>, ITransformable<Light>, IIdentifiable
    {
        public Light(Vector color, Vector position, float strength, Guid? polygonId = null, Guid? id = null, int generation = 0)
        {
            Color = color;
            Position = position;
            Strength = strength;
            Generation = generation;
            PolygonId = polygonId.GetValueOrDefault(Guid.Empty);
            Id = id.GetValueOrDefault(Guid.Empty);
        }

        public float Strength { get; set; }

        public Vector Color { get; set; }

        public abstract Light Clone();

        public abstract Light Transform(Transformation transformation);

        public virtual Vector Position { get; set; }

        public int Generation { get; set; }

        public Guid PolygonId { get; set; }

        public List<LightMapElement> Shadows { get; set; }

        public Guid Id { get; set; }
    }
}
