namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Helper;
    using Lightcore.Worlds.Models;
    using System.Collections.Generic;
    using System.Drawing;

    public class SurfaceWorld : WorldBuilder
    {
        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            var simpleSurface = Shapes.SimpleSurface(
                Color.White.ToVector(),
                new Vector(-160, -50, 0),
                new Vector(100, 0, 0),
                new Vector(0, 100, 0),
                2,
                ColorTextureStore.NormalTexture
            );

            var textureSurface = Shapes.TextureSurface(
                Color.White.ToVector(),
                new Vector(-50, -50, 0),
                new Vector(100, 0, 0),
                new Vector(0, 100, 0),
                2,
                ImageTextureStore.TextureBuilder("Test")
            );

            var bitmap = ImageTextureStore.GetImage("Test");

            var map = MapHelper.CreateMap(
                bitmap.Width,
                bitmap.Height,
                (x, y) => 0,
                (x, y) => bitmap.GetPixel(x, y).ToVector()
            );

            var mapSurface = Shapes.MapSurface(
                new Vector(60, -50, 0),
                new Vector(100, 0, 0),
                new Vector(0, 100, 0),
                map,
                ColorTextureStore.NormalTexture
            );


            entities.Add(simpleSurface);
            entities.Add(textureSurface);
            entities.Add(mapSurface);

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
