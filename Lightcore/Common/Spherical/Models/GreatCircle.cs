namespace Lightcore.Common.Spherical.Models
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using System.Linq;

    public class GreatCircle : Indexable<Vector>
    {
        public GreatCircle(params Vector[] elements) : base(elements.Select(element => new Vector(1, element[Axis.Theta], element[Axis.Phi])).ToArray())
        {
            Normal = Elements[0].ToCartesian() % Elements[1].ToCartesian();
        }

        public Vector[] Vectors { get; set; }

        public Vector Normal { get; private set; }
    }
}
