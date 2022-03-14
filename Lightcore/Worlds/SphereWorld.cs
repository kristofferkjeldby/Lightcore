namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Helper;
    using Lightcore.Worlds.Models;
    using System.Collections.Generic;
    using System.Drawing;

    public class SphereWorld : WorldBuilder
    {
        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            entities.Add(Shapes.SimpleSphere(
                Color.White.ToVector(),
                new Vector(-110, 0, 0),
                50,
                50,
                ColorTextureStore.NormalTexture
            ));

            entities.Add(Shapes.TextureSphere(
                Color.White.ToVector(),
                new Vector(0, 0, 0),
                50,
                50,
                ImageTextureStore.TextureBuilder("Earth")
            ));

            var bitmap = ImageTextureStore.GetImage("Earth");

            var map = MapHelper.CreateMap(
                bitmap.Width,
                bitmap.Height,
                (x, y) => 0,
                (x, y) => bitmap.GetPixel(x, y).ToVector()
            );

            entities.Add(Shapes.MapSphere(
                new Vector(110, 0, 0),
                50,
                map,
                ColorTextureStore.NormalTexture
            ));

            var transformation = CartesianUtils.RotateTransformation(new Vector(0, 1, 0), Constants.PI);

            entities.ForEach(p => p.Transform(transformation));

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
