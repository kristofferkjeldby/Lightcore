namespace Lightcore.Processors.Models
{
    using System.Collections.Generic;
    using Lightcore.Common.Models;

    public class RenderMode
    {
        public RenderMode()
        {
            EntityTypeFilter = new List<EntityType>();
        }

        public List<EntityType> EntityTypeFilter { get; }

        public bool Debug { get; set; }
    }
}
