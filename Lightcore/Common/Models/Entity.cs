namespace Lightcore.Common.Models
{
    using System;
    using System.Linq;

    public class Entity : Indexable<Polygon>, IClonable<Entity>, ITransformable<Entity>
    {
        public Entity(EntityType entityType, Guid? id = null, params Polygon[] polygons) : base(polygons)
        {
            EntityType = entityType;
            Id = id.GetValueOrDefault(Guid.NewGuid());
        }

        public Entity(EntityType entityType, params Polygon[] polygons) : base(polygons)
        {
            EntityType = entityType;
            Id = Guid.NewGuid();
        }

        public Entity Clone()
        {
            return new Entity(EntityType, Id, Elements.Select(polygon => polygon.Clone()).ToArray());
        }

        public Entity Transform(Func<Vector, Vector> transformation)
        {
            foreach (var polygon in Elements)
                polygon.Transform(transformation);
            return this;
        }

        public Entity Transform(Matrix transformation)
        {
            return Transform(e => transformation * e);
        }

        public EntityType EntityType { get; set; }

        public Guid Id { get; set; }
    }
}
