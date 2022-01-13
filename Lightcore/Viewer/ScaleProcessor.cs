namespace Lightcore.Viewer
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;

    public class ScaleProcessor : Processor
    {
        public override ProcessorMetadata Metadata => new ProcessorMetadata("Scale processor", false, EntityType.Debug, EntityType.World);

        public override Vector VectorProcessor(Vector vector, RenderArgs args)
        {
            // Projection onto a flat screen result in the same scale factor in all places of image
            // and tend to distort image in the borders.
            var scale = Settings.DistanceFromScreen / (Settings.DistanceFromScreen + vector[2]);
            return new Vector(vector[0] * scale, vector[1] * scale, vector[2]);
        }
    }
}
