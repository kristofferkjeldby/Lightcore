
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

**The main coordinate system and perspective settings**

The main coordinate system (Settings.WorldReferenceFrame) is placed in the center of the screen, with the x axis pointing to the right, the y axis pointing to the top, and the z-axis pointing _out of the screen_. 

The viewer (camera) is placed at (0, 0, 200) looking into the screen (Settings.CameraRererenceFrame). 

The scale is set to 100 (Settings.MaxX). This means that the x axis goes from -100 to 100. To calculate the perspective, another parameter is needed, that is the distance of the viewer to the screen (Settings.DistanceFromScreen). This determines the angle visible "through" the screen, and hence the rate in which objects are getting smaller. This is currently set to 300, estimating that the viewer sits a one and a half screen width from the screen. 

Hence, if you screen is 40 cm wide, and you sit 60 cm from your screen a 100x100x100 box placed in origin would look like a 40 cubic centimeter box, placed 80 cm behind your screen. 

The DistanceFromScreen settings might be a little tricky to understand: If your imaginary viewer is moved closer to the screen, objects in the imaginary world is getting smaller. This is because the angle visible though the screen increases, meaning that objects gets smaller faster with distance.

**Other coordinate systems**

Internally, Lightcore users ordinary three dimensional Cartesian coordinates, and both the WorldReferenceFrame and the CameraRererenceFrame are three dimensional Cartesian coordinate systems. However, for some operations, it might be helpful to use other coordinate systems (spherical and homogenous), and then transform the coordinates into the WorldReferenceFrame. An example is when defining a entity like a sphere:

```

    public partial class Shapes
    {
        public static Entity SimpleSphere(Vector color, Vector origon, float radius, int segments, Func<Vector, Texture> texture, RenderMode renderMode = null)
        {
            if (renderMode?.Preview ?? false)
                segments = Settings.PreviewResolution;

            var polygons = new List<Polygon>();

            var tStepSize = Constants.PI / segments; 
            var pStepSize = Constants.PI2 / segments;

            for (int t = 0; t < segments; t++)
            {
                var t1 = t + 1;

                for (int p = 0; p < segments; p++)
                {
                    var p1 = p + 1;

                    var vector00 = new Vector(radius, t * tStepSize, p * pStepSize);
                    var vector11 = new Vector(radius, t1 * tStepSize, p1 * pStepSize);
                    var vector01 = new Vector(radius, t * tStepSize, p1 * pStepSize) ;
                    var vector10 = new Vector(radius, t1 * tStepSize, p * pStepSize);

                    polygons.Add
                    (
                        new Polygon
                        (
                            texture(color),
                            new Vector[]
                            {
                                vector00,
                                vector10,
                                vector01
                            }
                        )
                    );

                    polygons.Add
                    (
                        new Polygon
                        (
                            texture(color),
                            new Vector[]
                            {
                                vector11,
                                vector01,
                                vector10

                            }
                        )
                    );
                }
            }

            var source = new ReferenceFrame(
                new Matrix(
                    new Vector(0, 0, 1),
                    new Vector(1, 0, 0),
                    new Vector(0, 1, 0)
                    ),
                Settings.Origon,
                ReferenceFrameType.Spherical);

            var destination = new ReferenceFrame(Settings.Unit, -origon, ReferenceFrameType.Cartesian);
            var transformation = CommonUtils.ReferenceFrameTransformation(source, destination);

            polygons.ForEach(polygon => polygon.Transform(transformation));

            return new Entity(EntityType.World, polygons.ToArray());
        }
    }
}
```

The sphere is defined in a spherical coordinate system (r, θ, φ). You will notice that the unit vectors of this reference frame has been swapped (z, x, y), so that the the top of the sphere (r, 0, 0), which for a unit-sphere would normally correspond to (0, 0, 1) is aligned with the y-axis (to avoid having the top of the sphere facing the viewer), and the front-facing side of the sphere (r, pi/2, 0), which would normally be (1, 0, 0) for a unit-sphere, is pointing in the z direction, towards the viewer. The reference frame is simply place in origon (0, 0, 0), and then transformed to a Cartesian reference frame with the origon oppositite the center of the sphere (-center), effectively transposing the sphere to the desired center. 


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

**Transformations**

Lightcore has helper methods for all the standard affine transformations you will need to move objects around, scale and rotate. Ligthcore also support perspective transformation using homogenous coordinates (see Lightcore.Common.Models.Transformations for the different type of supported transformations):

```
	var box = Shapes.SimpleBox(Color.Red.ToVector(), new Vector(0, 0, 0), new Vector(100, 0, 0), new Vector(0, 100, 0), new Vector(0, 0, 100), 40, ColorTextureStore.ShinyTexture);

	// Move box (translate transformation)
	box.Transform(v => v += new Vector(-50, -50, 0));

	// Scale box to 0.8 size
	box.Transform(v => v * 0.8f);

	// Rotate box PI/4 around the axis 1, 1, 1
	box.Transform(CartesianUtils.Rotate(new Vector(1, 1, 1).Unit(), Constants.PI / 4));
```

**Textures**

Lightcore support simple forward texture mapping as well as combined color and displacement maps:

![Textures](https://raw.githubusercontent.com/kristofferkjeldby/Lightcore/master/Examples/Earth.png)

The color/displacement maps (left) can produce beautiful results for round objects, but texture mapping is way faster to render for flat surfaces.
