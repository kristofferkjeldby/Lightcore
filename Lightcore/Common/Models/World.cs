namespace Lightcore.Common.Models
{
    using Lightcore.Lighting.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class World : IClonable<World>, IIdentifiable
    {
        public World(ReferenceFrame referenceFrame, List<Entity> entities = null, List<Light> lights = null, Guid? id = null)
        {
            Entities = entities ?? new List<Entity>();
            Lights = lights ?? new List<Light>();
            ReferenceFrame = referenceFrame;
            Id = id.GetValueOrDefault(Guid.NewGuid());
        }

        public List<Entity> Entities { get; set; }

        public List<Light> Lights { get; set;}

        public ReferenceFrame ReferenceFrame { get; set; }

        public Guid Id { get; set; }

        public void Transform(ReferenceFrame destinationReferenceFrame)
        {
            var transformation = CommonUtils.ReferenceFrameTransformation(ReferenceFrame, destinationReferenceFrame);

            foreach (var entity in Entities)
            {
                entity.Transform(transformation);
            }

            foreach (var light in Lights)
            {
                light.Transform(transformation);
            }

            ReferenceFrame = destinationReferenceFrame;
        }

        public World Clone()
        {
            return new World
                (
                    ReferenceFrame,
                    Entities.Select(entity => entity.Clone()).ToList(),
                    Lights.Select(light => light.Clone()).ToList(),
                    Id
                );
        }
    }
}