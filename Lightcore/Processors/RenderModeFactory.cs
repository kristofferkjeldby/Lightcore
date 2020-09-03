namespace Lightcore.Processors
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;

    public static class RenderModeFactory
    {
        public static RenderMode Create(bool preview, bool debug)
        {
            var renderMode = new RenderMode();

            if (debug)
                renderMode.Debug = true;

            if (preview)
                renderMode.EntityTypeFilter.Add(EntityType.Preview);
            else
                renderMode.EntityTypeFilter.Add(EntityType.World);

            if (debug)
                renderMode.EntityTypeFilter.Add(EntityType.Debug);

            return renderMode;

        }
    }
}
