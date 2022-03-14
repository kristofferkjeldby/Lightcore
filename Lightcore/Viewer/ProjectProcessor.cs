namespace Lightcore.Viewer
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Common.Models.Transformations;
    using Lightcore.Processors.Models;

    public class ProjectProcessor : Processor
    {
        private Transformation transformation;

        public override void PreProcessor(RenderArgs args)
        {
            transformation = CommonUtils.ReferenceFrameTransformation(args.World.ReferenceFrame, args.Camera.ReferenceFrame);
        }

        public override Vector VectorProcessor(Vector vector, RenderArgs args)
        {
            return transformation.Transform(vector);
        }

        public override ProcessorMetadata Metadata => new ProcessorMetadata("Project processor", false, EntityType.Debug, EntityType.World);
    }
}

