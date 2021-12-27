namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using System;
    using System.Collections.Generic;

    public class LightMap : SortedDictionary<Tuple<Guid, Guid>, float>
    { 
        public void Add(LightMapSearchList lightMapElements)
        {
            foreach (var lightMapElement in lightMapElements.GetAll())
            {
                Add(new Tuple<Guid, Guid>(lightMapElement.LightId, lightMapElement.PolygonId), lightMapElement.Visibility);
            }
        }

        public float GetVisibility(Light light, Polygon polygon)
        {
            var key = new Tuple<Guid, Guid>(light.Id, polygon.Id);

            if (!ContainsKey(key))
                return 1;
            return this[key];

        }
    }
}
