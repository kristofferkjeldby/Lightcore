namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical.Extensions;
    using Lightcore.Common.Spherical.Models;
    using Lightcore.Textures.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LightMapElement : SphericalTriangle
    {
        public LightMapElement(Light light, Polygon polygon) : base(polygon.Elements)
        {
            LightId = light.Id;
            PolygonId = polygon.Id;
            if ((polygon.Texture as ColorTexture).Color.Equals(new Vector(1, 1, 1)))
                Debug = true;
            Theta = new AngleSpan(Elements.Min(vector => vector[Axis.Theta]), Elements.Max(vector => vector[Axis.Theta]));
            Phi = new AngleSpan(Elements.Min(vector => vector[Axis.Phi]), Elements.Max(vector => vector[Axis.Phi]));
            Visibility = 1;
        }

        public AngleSpan Theta { get; set; }
        public AngleSpan Phi { get; set; }
        public Guid PolygonId { get; }
        public Guid LightId { get; }
        public float Visibility { get; set; }
        public float Hidden => 1 - Visibility;
        public bool Debug { get; set; }
    }
}
