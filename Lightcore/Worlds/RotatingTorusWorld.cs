namespace Lightcore.Worlds
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Textures.Gradients.Models;
    using Lightcore.Worlds.Extensions;
    using Lightcore.Worlds.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class RotatingTorusWorld : WorldBuilder
    {
        public int Resolution { get; set; }

        public int PreviewResolution { get; set; }

        public Tuple<double, Vector>[,] Map { get; set; }

        public RotatingTorusWorld(int resolution = 100, int previewResolution = 20, int animateStep = 0) : base()
        {
            Resolution = resolution;
            PreviewResolution = previewResolution;

            // Setup gradient
            var gradient = new Gradient(Color.Blue.ToVector(), Color.Blue.ToVector());
            gradient.ColorPoints.Add(new ColorPoint(Color.Red.ToVector(), 0.25, true));
            gradient.ColorPoints.Add(new ColorPoint(Color.Yellow.ToVector(), 0.50, true));
            gradient.ColorPoints.Add(new ColorPoint(Color.Green.ToVector(), 0.75, true));

            Map = new Tuple<double, Vector>[resolution, resolution];

            for (int w = 0; w < resolution; w++)
            {
                var displacement = Math.Sin(((8 * Constants.PI2) / resolution) * w) * 10;

                var color = gradient.GetColor((double)w / resolution);

                for (int h = 0; h < resolution; h++)
                {
                    Map[w, h] = new Tuple<double, Vector>(displacement, color);
                }
            }
        }

        public override void Create(List<Entity> entities, List<Light> lights, int animateStep = 0)
        {
            // Setup rotation
            var angle = (Constants.PI2 / Settings.AnimateMaxSteps) * animateStep;
            var rotate = CartesianUtils.Rotate(new Vector(0, 1, 0).Unit(), angle);

            entities.Add(WorldUtils.Torus(EntityType.Preview, new Vector(0, 0, 0), 90, 50, Map.Reduce(PreviewResolution)).Transform(rotate));
            entities.Add(WorldUtils.Torus(EntityType.World, new Vector(0, 0, 0), 90, 50, Map).Transform(rotate));

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
