## Lightcore

Lightcore is a 3D engine build in pure C#, without any frameworks. My motivation for writing Lightcore was not to make something strictly usable, as the lack of hardware acceleration makes Lightcore inherently slow. It was however to explore the math behind 3D engines and make it available in a form that most C# developers would understand.

![Mars rendered with exaggerated heightmap](https://raw.githubusercontent.com/kristofferkjeldby/Lightcore/master/Examples/Mars.jpg)

Please note that Lightcore is *not* a raytracer - initial it started as a game engine, and you will find leftovers from this in e.g., the keyboard controls and the options to prerendering static lightning. It defines a world using polygons, which are processed for light, shadow an perspective and then displayed on the screen.

To get started, I suggest downloading the application. 

**Creating a world**

You define your world via a WorldBuilder:

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
        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            entities.Add(Shapes.SimpleSphere(Color.Red.ToVector(), new Vector(0, 0, 0), 150, 200, ColorTextureStore.ShinyTexture, renderMode));

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

This worldbuilder simply places a red sphere in the middle of the screen.

You will need to register your world in Lightcore.cs and then start the application. The application support a low resolution preview mode. 

![Test world](https://raw.githubusercontent.com/kristofferkjeldby/Lightcore/master/Examples/Testworld.png)

**The coordinate system and perspective settings**

The coordinate system is placed in the center of the screen, with the x axis pointing to the right, the y axis pointing to the top, and the z-axis pointing _out of the screen_. 

The viewer (camera) is placed at (0, 0, 200) looking into the screen (Settings.CameraRererenceFrame). 

The scale is set to 100 (Settings.MaxX). This means that the x axis goes from -100 to 100. To calculate the perspective, another parameter is needed, that is the distance of the viewer to the screen (Settings.DistanceFromScreen). This determines the angle visible "through" the screen, and hence the rate in which objects are getting smaller. This is currently set to 300, estimating that the viewer sits a one and a half screen width from the screen. 

Hence, if you screen is 40 cm wide, and you sit 60 cm from your screen a 100x100x100 box placed in origin would look like a 40 cubic centimeter box, placed 80 cm behind your screen. 

The DistanceFromScreen settings might be a little tricky to understand: If your imaginary viewer is moved closer to the screen, objects in the imaginary world is getting smaller. This is because the angle visible though the screen increases, meaning that objects gets smaller faster with distance.

**The processor stacks**

Before you dive into further into Ligthcore, you will probably benefit from understanding the two processor stacks, and how they are used.

Lightcore contains two processor stacks - a preprocessor stack and a processor stack. Both stacks takes a world and processes it. The stacks consists of individual processors, and each processor will mutate the world in some way. Hence the stacks are inheritly destructive in nature. So when you create e.g. animations, the Create method of the WorldBuilder will be called before each frame, and basically recreate the world from scratch. The benefit of this that you will only have a single world in the memory - the drawback is speed.

Lightcore offers several options to optimize this process. Imagine we would create some kind of game, allowing the player to move around in this simple world. 

Surely the perspective would change, but light and shadows would be static. As the light and shadows are processed in the preprocessor stack, Lightcore offers an option to reuse the preprocessed world, and only call Create world for the first frame. To turn on this option, change StorePreprocessed to true in Settings.cs - you might also want to turn ShowStatus off in the same file.

You will now be able to move around in the world using:

 - 1 - Rotate up 
 - 2 - Rotate down 
 - 3 - Rotate left 
 - 4 - Rotate right
 - 5 - Spin left 
 - 6 - Spin right 
 - D - move right 
 - A - move left 
 - W - move up 
 - S - move down 
 - O - move forward 
 - L - move backwards

The big drawback is that you will now have two worlds in memory, the one being processed and the stored preprocessed world containing static lightning.

Another option is to create the entiries when the world is instantiated, and clone them for each frame:

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
        Entity sphere;

        public TestWorld()
        {
            sphere = Shapes.SimpleSphere(Color.Red.ToVector(), new Vector(0, 0, 0), 150, 200, ColorTextureStore.ShinyTexture);
        }

        public override void Create(List<Entity> entities, List<Light> lights, RenderMode renderMode, int animateStep = 0)
        {
            entities.Add(sphere.Clone());

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

The benefit here is still speed, the drawback are memory and also that the preview mode will no longer work.