namespace Lightcore.Worlds.Models
{
    using Lightcore.Common.Models;
    using System.Collections.Generic;
    using Lightcore.Lighting.Models;

    public class WorldBuilder
    {
        public WorldBuilder()
        {
            Entities = new List<Entity>();
            Lights = new List<Light>();
        }

        public List<Entity> Entities{ get; set; }
        public List<Light> Lights { get; set; }

        public World ToWorld()
        {
            return new World
            (
                Settings.WorldReferenceFrame,
                Entities,
                Lights
            );
        }

    }
}
