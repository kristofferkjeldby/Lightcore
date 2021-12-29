namespace Lightcore.Worlds
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Textures.Gradients.Models;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Helper;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ShadowWorld : WorldBuilder
    {
        public Tuple<float, Vector>[,] Map { get; set; }

        public Tuple<float, Vector>[,] PreviewMap { get; set; }

        public ShadowWorld(int resolution = 400, int previewResolution = 40) : base()
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

            PreviewMap = Map.Reduce(previewResolution);
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            entities.Add(Shapes.Surface(renderMode, new Vector(0, 0, -170), 700, 700, Map, ColorTextureStore.ShinyTexture));
            entities.Add(Shapes.SimpleSphere(renderMode, Color.Red.ToVector(), new Vector(0, 0, -100), 50, 200, ColorTextureStore.ShinyTexture));
            entities.Add(Shapes.SimpleSphere(renderMode, Color.Green.ToVector(), new Vector(100, 100, -100), 70, 200, ColorTextureStore.ColorTexture));
            entities.Add(Shapes.SimpleSphere(renderMode, Color.Blue.ToVector(), new Vector(-100, -100, -100), 40, 200, ColorTextureStore.MetallicTexture));

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
