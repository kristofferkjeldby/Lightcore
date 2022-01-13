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
            var box = Shapes.TextureBox(Color.White.ToVector(), new Vector(-50, -50, 50), new Vector(100, 0, 0), new Vector(0, 100, 0), new Vector(0, 0, -100), 1, ImageTextureStore.TextureBuilder("Checkerboard"));

            box.Transform(CartesianUtils.Rotate(new Vector(1, 0, 0).Unit(), Constants.PIfouth));
            box.Transform(CartesianUtils.Rotate(new Vector(1, 1, 0).Unit(), -Constants.PIfouth / 2));

            entities.Add(box.Clone());

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
