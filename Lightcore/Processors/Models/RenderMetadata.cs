namespace Lightcore.Processors.Models
{
    using System;
    using System.Collections.Generic;

    public class RenderMetadata
    {
        public RenderMetadata()
        {
            Statistics = new List<RenderStatistic>();
            Metadata = new Dictionary<string, object>();
        }

        public List<RenderStatistic> Statistics { get; set; }

        public Dictionary<string, object> Metadata { get; set; }

        public String Filename { get; set; }
    }
}
