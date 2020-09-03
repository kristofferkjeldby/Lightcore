namespace Lightcore.Processors
{
    using Lightcore.Processors.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProcessorStack
    {
        List<Processor> processors = new List<Processor>();

        public void Process(RenderArgs args)
        {
            foreach (var processor in processors.Where(processor => ProcessorPredicate(processor, args.RenderMode)))
            {
                processor.Process(args);

                if (args.CancellationToken.IsCancellationRequested)
                    return;
            }
        }

        public void Add(Processor processor)
        {
            processors.Add(processor);
        }

        private bool ProcessorPredicate(Processor processor, RenderMode renderMode)
        {
            if (processor.Disabled)
                return false;

            if (!renderMode.Debug && processor.Metadata.Debug)
                return false;

            return true;
        }
    }
}
