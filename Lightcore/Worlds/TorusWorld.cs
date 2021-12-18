namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System.Drawing;

    public class TorusWorld : WorldBuilder
    {
        public TorusWorld(int seed = 0, int maxseed = 100) : base()
        {
            Lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(300, 300, 500),
                    700
                )
            );

            Entities.Add(WorldUtils.SimpleTorus(EntityType.Preview, Color.Red.ToVector(), new Vector(0, 0, 0), 100, 50, 100, 50));
            Entities.Add(WorldUtils.SimpleTorus(EntityType.World, Color.Red.ToVector(), new Vector(0, 0, 0), 100, 50, 400, 200));
        }
    }
}
