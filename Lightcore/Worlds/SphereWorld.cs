namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System.Drawing;

    public class SphereWorld : WorldBuilder
    {
        public SphereWorld(int resolution = 200, int previewResolution = 40) : base()
        {
            Lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(100, 100, 400),
                    400
                )
            );

            Entities.Add(WorldUtils.SimpleSphere(EntityType.Preview, Color.Red.ToVector(), new Vector(0, 0, 0), 150, previewResolution));
            Entities.Add(WorldUtils.SimpleSphere(EntityType.World, Color.Red.ToVector(), new Vector(0, 0, 0), 150, resolution));
        }
    }
}
