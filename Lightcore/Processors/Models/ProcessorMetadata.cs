using Lightcore.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lightcore.Processors.Models
{
    public class ProcessorMetadata
    {
        public ProcessorMetadata(string name, bool debug = false, params EntityType[] entityTypes)
        {
            Name = name;
            Debug = debug;
            EntityTypeFilter = entityTypes.ToList();
        }

        public string Name { get; set; }

        public bool Debug { get; set; }

        public List<EntityType> EntityTypeFilter { get; set; }
    }
}
