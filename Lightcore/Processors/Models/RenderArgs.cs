namespace Lightcore.Processors.Models
{
    using Lightcore.Common.Models;
    using System;
    using System.Threading;

    public class RenderArgs
    {
        public RenderArgs(World world, World camera, RenderMode renderMode, CancellationToken cancellationToken, Action<string> status)
        {
            World = world;
            Camera = camera;
            RenderMetadata = new RenderMetadata();
            RenderMode = renderMode;
            CancellationToken = cancellationToken;
            Status = status;
        }

        public World Camera { get; set; }

        public World World { get; set; }

        public RenderMetadata RenderMetadata { get; set; }

        public RenderMode RenderMode { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public Action<string> Status { get; set; }
    }
}
