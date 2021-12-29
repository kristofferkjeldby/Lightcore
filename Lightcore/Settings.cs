namespace Lightcore
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Extensions;
    using Lightcore.Textures.Models;
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

        public static float R = Constants.PI / 100f;

        public static float DistanceFromScreen => 100;

        public static float ViewDistance => 500;

        public static float ReflectionTreadshold => 0f;

        public static float ReflectionAngle => Constants.PI / 6f;

        public static int ViewMargin => 5000;

        public static Texture DebugTexture => new SimpleTexture(Color.Gray.ToVector());

        public static bool Debug = false;

        public static bool StorePreprocessed = false;

        public static int AnimateMaxSteps = 400;

        public static string AnimateFilename = "animate_";

        public static int PreviewResolution = 40;
    }
}
