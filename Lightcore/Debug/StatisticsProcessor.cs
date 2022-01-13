namespace Lightcore.Debug
{
    using Lightcore.Processors.Models;

    public class StatisticsProcessor : Processor
    {
        public override ProcessorMetadata Metadata => new ProcessorMetadata("Statistics processor", true);

        public override void PostProcessor(RenderArgs args)
        {
            args.RenderMetadata.Statistics.ForEach(s => System.Diagnostics.Debug.WriteLine(s));
        }
    }
}
