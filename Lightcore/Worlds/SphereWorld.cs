﻿namespace Lightcore.Worlds
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures;
    using Lightcore.Textures.Extensions;
    using Lightcore.Worlds.Models;
    using System.Collections.Generic;
    using System.Drawing;

    public class SphereWorld : WorldBuilder
    {
        public int Resolution { get; set; }

        public int PreviewResolution { get; set; }

        public SphereWorld(int resolution = 200, int previewResolution = 40) : base()
        {
            Resolution = resolution;
            PreviewResolution = previewResolution;
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            entities.Add(Shapes.SimpleSphere(renderMode, Color.Red.ToVector(), new Vector(0, 0, 0), 150, Resolution, ColorTextureStore.ShinyTexture));

            lights.Add
            (
                new AmbientLight
                (
                    Color.White.ToVector(),
                    new Vector(100, 100, 400),
                    400
                )
            );

        }
    }
}
