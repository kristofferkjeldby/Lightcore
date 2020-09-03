
namespace Lightcore.Processors.Models
{
    using System;

    public class RenderStatistic
    {
        public string Name { get; set; }
        public TimeSpan Time => Ended - Started;
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }
        public int Polygons { get; set; }
        public int Vectors { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Time.TotalMilliseconds}ms ({Polygons} polygons, {Vectors} vectors)";
        }
    }
}
