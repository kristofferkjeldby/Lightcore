namespace Lightcore.Viewer
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using System.Diagnostics;

    public class ScaleProcessor : Processor
    {
        public override ProcessorMetadata Metadata => new ProcessorMetadata("Scale processor", false, EntityType.Debug, EntityType.Preview, EntityType.World);

        public override Vector VectorProcessor(Vector vector, RenderArgs args)
        {
            var distanceFromViewer = Settings.DistanceFromScreen + vector[2];
            var scale = Settings.DistanceFromScreen / distanceFromViewer;
            return new Vector(vector[0] * scale, vector[1] * scale, vector[2]);
        }
    }
}
