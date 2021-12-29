namespace Lightcore.Debug
{
    using Lightcore.Processors.Models;
    using System;
 
    public class StatisticsProcessor : Processor
    {
        public override ProcessorMetadata Metadata => new ProcessorMetadata("Statistics processor", true);

        public override void PostProcessor(RenderArgs args)
        {
            foreach (var statistic in args.RenderMetadata.Statistics)
            {
                System.Diagnostics.Debug.Write($"{statistic}{Environment.NewLine}");
            }
        }
    }
}
