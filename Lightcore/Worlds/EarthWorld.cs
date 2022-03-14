namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System.Collections.Generic;
    using System.Drawing;

    public class EarthWorld : WorldBuilder
    {
        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            var earth = Shapes.TextureSphere(Color.White.ToVector(), new Vector(0, 0, 0), 100, 50, ImageTextureStore.TextureBuilder("Earth"));

            earth.Transform(CartesianUtils.RotateTransformation(new Vector(0, 1, 0), Constants.PI));

            entities.Add(earth);

            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(0, 0, 400),
                    400
                )
            );
        }
    }
}
