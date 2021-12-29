namespace Lightcore.Viewer
{
    using Lightcore.Common;
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using System;

    public class ProjectProcessor : Processor
    {
        private Func<Vector, Vector> transformation;

        public override void PreProcessor(RenderArgs args)
        {
            transformation = CommonUtils.Transformation(args.World.ReferenceFrame, args.Camera.ReferenceFrame);
        }

        public override Vector VectorProcessor(Vector vector, RenderArgs args)
        {
            return transformation(vector);
        }

        public override ProcessorMetadata Metadata => new ProcessorMetadata("Project processor", false, EntityType.Debug, EntityType.World);
    }
}

