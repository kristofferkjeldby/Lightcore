namespace Lightcore.Lighting
{
    using System.Collections.Generic;
    using Lightcore.Lighting.Models;
    using Lightcore.Common.Spherical;
    using Lightcore.Common.Spherical.Extensions;
    using System.Linq;
    using Lightcore.Common;
    using System;

    public static partial class LightUtils
    {
        public static void Visibility(LightMapElement thisLightMapElement, IEnumerable<LightMapElement> otherLightMapElements)
        {
            foreach(var otherLightMapElement in otherLightMapElements.Where(o => o.Visibility > 0))
            {
                var currentVisibility = thisLightMapElement.Visibility;
                var additionalVisibility = (otherLightMapElement.Visibility * (1 - Visibility(thisLightMapElement, otherLightMapElement)));
                if (additionalVisibility == 0)
                    continue;

                var newVisibilitity = currentVisibility - additionalVisibility;

                if (newVisibilitity >=  currentVisibility)
                    continue;

                thisLightMapElement.Visibility = newVisibilitity;
                thisLightMapElement.Visibility = CommonUtils.Limit(thisLightMapElement.Visibility, 0, 1);
                if (thisLightMapElement.Visibility == 0)
                    return;
            }
        }

        public static float Visibility(LightMapElement thisLightMapElement, LightMapElement otherLightMapElement)
        {
            if (thisLightMapElement.PolygonId.Equals(otherLightMapElement.PolygonId))
                return 1;

            if (!otherLightMapElement.Theta.ContainsSome(thisLightMapElement.Theta) || !otherLightMapElement.Phi.ContainsSome(thisLightMapElement.Phi))
            {
                return 1;
            }


            var intersections = thisLightMapElement.Intersection(otherLightMapElement);

            if (thisLightMapElement.Elements.Distinct().Count() < 3)
                return 0;

            if (intersections.Count() > 2)
            {
                var area = thisLightMapElement.Area();
                var shadowedArea = SphericalUtils.Triangulation(intersections).Sum(triangle => triangle.Area());
           
                // This can result from rounding errors
                if (shadowedArea > area)
                    return 0;

                var factor = 1 - shadowedArea / area;
                return factor;
            }

            return 1;
        }
    }
}
