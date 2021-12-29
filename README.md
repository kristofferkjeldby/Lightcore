# Lightcore

![Example](https://raw.githubusercontent.com/kristofferkjeldby/Lightcore/master/Examples/1.png)

Lightcore is a 3D engine build with pure C#, without any frameworks. My motivation for writing Lightcore was not to make something usable, as the lack of hardware acceleration makes Lightcore inherently slow. It was however to explore the math behind 3D engines and make it available in a form that most C# developers would understand.

![Mars rendered with exaggerated heightmap](https://raw.githubusercontent.com/kristofferkjeldby/Lightcore/master/Examples/Mars.jpg)

Initial it started as a game engine, and you will find leftovers from this in e.g., the keyboard controls and the options to prerendering static lightning.

To get started, I suggest downloading the application. You define your world in a WorldBuilder:
```
    namespace Lightcore.Worlds
    {
        using Lightcore.Common.Models;
        using Lightcore.Lighting.Models;
        using Lightcore.Processors.Models;
        using Lightcore.Textures;
        using Lightcore.Textures.Extensions;
        using Lightcore.Worlds.Models;
        using System.Collections.Generic;
        using System.Drawing;
    
        public class TestWorld : WorldBuilder
        {
            public override void Create(
	            List<Entity> entities, 
	            List<Light> lights, 
	            RenderMode renderMode, 
	            int animateStep = 0)
            {
                entities.Add(
                Shapes.SimpleSphere(renderMode, 
                Color.Red.ToVector(), 
                new Vector(0, 0, 0), 150, 200, 
                ColorTextureStore.ShinyTexture));
    
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
```

This WorldBuilder simply places a red sphere in the middle of the screen. You can add more advanced shapes using the available library, or define you own. Lightcore supports both Cartesian and Spherical coodinates.

You will need to register your world in Lightcore.cs and then start the application. The application support a low resolution preview mode. It also supports creating animations by calling the Create function increasing the animateStep. In that way, you can move stuff around in your world.

