namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System.Drawing;

    public class TestWorld : WorldBuilder
    {
        public TestWorld()
        {
            Entities.Add(WorldUtils.SimpleSphere(EntityType.Preview, Color.Red.ToVector(), new Vector(0,0,0), 100, 10));

            Lights.Add(new AmbientLight(Color.White.ToVector(), new Vector(200, 200, 200), 400));

            //Entities.Add
            //(
            //    WorldUtils.Line
            //    (
            //        new SimpleTexture(Color.Red.ToVector()),
            //        EntityType.World,
            //        new Vector(0, 0, 0),
            //        new Vector(50, 0, 0)
            //    )
            //);

            //Entities.Add
            //(
            //    WorldUtils.Line
            //    (
            //        new SimpleTexture(Color.Green.ToVector()),
            //        EntityType.World,
            //        new Vector(0, 0, 0),
            //        new Vector(0, 50, 0)
            //    )
            //);

            //Entities.Add
            //(
            //    WorldUtils.Line
            //    (
            //        new SimpleTexture(Color.Blue.ToVector()),
            //        EntityType.World,
            //        new Vector(0, 0, 0),
            //        new Vector(0, 0, 50)
            //    )
            //);

            //Entities.Add
            //(
            //    WorldUtils.Box
            //    (
            //        new SimpleTexture(Color.Red.ToVector()),
            //        EntityType.World,
            //        new Vector(0, 0, 0),
            //        new Vector(50, 0, 0),
            //        new Vector(0, 50, 0),
            //        new Vector(0, 0, 50)
            //    )
            //);
        }
    }
}
