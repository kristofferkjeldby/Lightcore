namespace Lightcore
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Textures.Models;
    using System;
    using System.Drawing;

    public static class Settings 
    {
        public static ReferenceFrame WorldReferenceFrame = new ReferenceFrame
        (
            Unit,
            Origon
        );

        public static ReferenceFrame CameraRererenceFrame = new ReferenceFrame
        (
            new Matrix(
                new Vector(1, 0, 0),
                new Vector(0, 1, 0),
                new Vector(0, 0, -1)
            ),
            new Vector(0, 0, 200)
        );

        public static Matrix Unit => new Matrix
        (
            new Vector(1, 0, 0),
            new Vector(0, 1, 0),
            new Vector(0, 0, 1)
        );

        public static Vector Origon => new Vector(0, 0, 0);

        public static double R = Math.PI/100;

        public static double DistanceFromScreen => 100;

        public static double ViewDistance => 500;

        public static double ReflectionTreadshold => 0f;

        public static double ReflectionAngle => Math.PI / 6d;

        public static int ViewMargin => 5000;

        public static Texture DebugTexture => new SimpleTexture(Color.Gray.ToVector());

        public static bool Debug = false;

        public static bool StorePreprocessed = false;

        public static int AnimateMaxSteps = 10;

        public static string AnimateFilename = "animate_";
    }
}
