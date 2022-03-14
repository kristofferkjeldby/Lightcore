namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System.Collections.Generic;
    using System.Drawing;

    public class TextureWorld : WorldBuilder
    {
        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            var surface = Shapes.TextureSurface(Color.White.ToVector(), new Vector(-50, -50, 0), new Vector(100, 0, 0), new Vector(0, 100, 0), 10, ImageTextureStore.TextureBuilder("Test"));

            //var box = Shapes.TextureBox(Color.White.ToVector(), new Vector(-50, -50, 50), new Vector(100, 0, 0), new Vector(0, 100, 0), new Vector(0, 0, -100), 10, ImageTextureStore.TextureBuilder("Cat"));

            //box.Transform(CartesianUtils.Rotate(new Vector(1, 0, 0).Unit(), Constants.PIfouth / 2));
            //box.Transform(CartesianUtils.Rotate(new Vector(1, 1, 0).Unit(), (Constants.PI2 / Settings.AnimateMaxSteps) * animateStep));

            entities.Add(surface);

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
