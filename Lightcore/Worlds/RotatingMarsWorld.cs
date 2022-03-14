namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Textures.Models;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Helper;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class RotatingMarsWorld : WorldBuilder
    {
        public Tuple<float, Vector>[,] Map { get; set; }

        public Tuple<float, Vector>[,] AtmosphereMap { get; set; }

        public bool Atmosphere { get; set; }

        public RotatingMarsWorld(bool atmosphere = false) : base()
        {
            Atmosphere = atmosphere;

            var colorBitmap = ImageTextureStore.GetImage("MarsColor");
            var heightBitmap = ImageTextureStore.GetImage("MarsHeight");


            Map = MapHelper.CreateMap(
                colorBitmap.Width,
                colorBitmap.Height,
                (x, y) => heightBitmap.GetPixel(x, y).GetBrightness() * 10,
                (x, y) => colorBitmap.GetPixel((x + 130) % colorBitmap.Width, y).ToVector()
            );

            if (atmosphere)
            {

                var atmosphereBitmap = ImageTextureStore.GetImage("MarsAtmosphere");

                AtmosphereMap = MapHelper.CreateMap(
                    atmosphereBitmap.Width,
                    atmosphereBitmap.Height,
                    (x, y) => 0,
                    (x, y) => atmosphereBitmap.GetPixel(x, y).ToVector()
                );
            }
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            var angle = (Constants.PI2 / Settings.AnimateMaxSteps) * animateStep;
            var rotateMars = CartesianUtils.RotateTransformation(new Vector(0, 1, 0).Unit(), angle);

            entities.Add(Shapes.MapSphere(new Vector(0, 0, 0), 100, Map, ColorTextureStore.NormalTexture, renderMode).Transform(rotateMars));

            if (Atmosphere)
            {
                entities.Add(Shapes.MapSphere(new Vector(0, 0, 0), 100, AtmosphereMap, color => new ColorTexture(color, 0.1f, 0.7f, 0.0f, 0.3f), renderMode).Transform(rotateMars));
            }

            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(-500, -500, 500f),
                    900
                )
            );
        }
    }
}
