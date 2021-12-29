namespace Lightcore.Worlds
{
    using Lightcore.Common;
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System.Collections.Generic;
    using System.Drawing;

    public class TestWorld : WorldBuilder
    {
        Entity box;
        Entity surface;

        public TestWorld()
        {
            box = Shapes.SimpleSurface(Color.Red.ToVector(), new Vector(-50, -50, 0), new Vector(100, 0, 0), new Vector(0, 100, 0), 2, ColorTextureStore.ShinyTexture);

            // Move box (-50, -50, 0)
            //box.Transform(v => v + new Vector(-50, -50, -00));



        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {

            entities.Add(box.Clone());

            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(100, 100, 400),
                    400
                )
            );
        }
    }
}
