namespace Lightcore.Worlds
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Textures.Gradients.Models;
    using Lightcore.Worlds.Helper;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ShadowWorld : WorldBuilder
    {
        public Tuple<float, Vector>[,] Map { get; set; }

        public ShadowWorld(int resolution = 400) : base()
        {
            Map = new Tuple<float, Vector>[20, 20];

            var gradient = new Gradient(Color.Blue.ToVector(), Color.Blue.ToVector());
            gradient.ColorPoints.Add(new ColorPoint(Color.Red.ToVector(), 0.25f, true));
            gradient.ColorPoints.Add(new ColorPoint(Color.Yellow.ToVector(), 0.50f, true));
            gradient.ColorPoints.Add(new ColorPoint(Color.Green.ToVector(), 0.75f, true));

            Map = MapHelper.CreateMap(
                resolution,
                resolution,
                (x, y) => CommonUtils.Sin(x / 10f) * 20,
                (x, y) => gradient.GetColor((float)y / resolution)
            );
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            entities.Add(Shapes.Surface(new Vector(-350, -350, -170), new Vector(700, 0, 0), new Vector(0, 700, 0), Map, ColorTextureStore.ShinyTexture, renderMode));
            entities.Add(Shapes.SimpleSphere(Color.Red.ToVector(), new Vector(0, 0, -100), 50, 200, ColorTextureStore.ShinyTexture, renderMode));
            entities.Add(Shapes.SimpleSphere(Color.Green.ToVector(), new Vector(100, 100, -100), 70, 200, ColorTextureStore.ColorTexture, renderMode));
            entities.Add(Shapes.SimpleSphere(Color.Blue.ToVector(), new Vector(-100, -100, -100), 40, 200, ColorTextureStore.MetallicTexture, renderMode));

            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(-100, -100, 200f),
                    400
                )
            );
        }
    }
}
