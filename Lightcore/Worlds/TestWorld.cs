namespace Lightcore.Worlds
{
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
        Entity sphere;

        public TestWorld()
        {
            sphere = Shapes.SimpleSphere(Color.Red.ToVector(), new Vector(0, 0, 0), 150, 200, ColorTextureStore.ShinyTexture);
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            entities.Add(sphere.Clone());

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
