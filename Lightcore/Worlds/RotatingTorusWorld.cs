namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Cartesian.Extensions;
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

    public class RotatingTorusWorld : WorldBuilder
    {
        public Tuple<float, Vector>[,] Map { get; set; }

        public RotatingTorusWorld(int resolution = 200, int animateStep = 0) : base()
        {

            // Setup gradient
            var gradient = new Gradient(Color.Blue.ToVector(), Color.Blue.ToVector());
            gradient.ColorPoints.Add(new ColorPoint(Color.Red.ToVector(), 0.25f, true));
            gradient.ColorPoints.Add(new ColorPoint(Color.Yellow.ToVector(), 0.50f, true));
            gradient.ColorPoints.Add(new ColorPoint(Color.Green.ToVector(), 0.75f, true));

            Map = MapHelper.CreateMap(
                resolution,
                resolution,
                (x, y) => (float)Math.Sin(((8 * Constants.PI2) / resolution) * x) * 10,
                (x, y) => gradient.GetColor((float)x / resolution)
            );
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            // Setup rotation
            var angle = (Constants.PI2 / Settings.AnimateMaxSteps) * animateStep;
            var rotate = CartesianUtils.Rotate(new Vector(0, 1, 0).Unit(), angle);

            entities.Add(Shapes.Torus(new Vector(0, 0, 0), 90, 50, Map, ColorTextureStore.ShinyTexture, renderMode).Transform(rotate));

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
