namespace Lightcore.Common.Models
{
    using System;
    using System.Linq;

    public class Entity : Indexable<Polygon>, IClonable<Entity>, ITransformable
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

        public void Transform(Func<Vector, Vector> transformation)
        {
            foreach (var polygon in Elements)
                polygon.Transform(transformation);
        }

        public EntityType EntityType { get; set; }

        public Guid Id { get; set; }
    }
}
