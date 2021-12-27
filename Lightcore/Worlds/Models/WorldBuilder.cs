namespace Lightcore.Worlds.Models
{
    using Lightcore.Common.Models;
    using System.Collections.Generic;
    using Lightcore.Lighting.Models;

    public abstract class WorldBuilder
    {
        public WorldBuilder()
        {

        }

        public abstract void Create(List<Entity> entities, List<Light> lights, int animateStep = 0);

        public World ToWorld(int animateStep = 0)
        {
            var entities = new List<Entity>();
            var lights = new List<Light>();

            Create(entities, lights, animateStep);

            return new World
            (
                Settings.WorldReferenceFrame,
                entities,
                lights
            );
        }
    }
}
