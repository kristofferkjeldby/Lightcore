namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System.Collections.Generic;
    using System.Drawing;

    public class SphereWorld : WorldBuilder
    {
        public int Resolution { get; set; }

        public int PreviewResolution { get; set; }

        public SphereWorld(int resolution = 200, int previewResolution = 40) : base()
        {
            Resolution = resolution;
            PreviewResolution = previewResolution;
        }

        public override void Create(List<Entity> entities, List<Light> lights, int animateStep = 0)
        {
            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(100, 100, 400),
                    400
                )
            );

            entities.Add(WorldUtils.SimpleSphere(EntityType.Preview, Color.Red.ToVector(), new Vector(0, 0, 0), 150, PreviewResolution));
            entities.Add(WorldUtils.SimpleSphere(EntityType.World, Color.Red.ToVector(), new Vector(0, 0, 0), 150, Resolution));
        }
    }
}
