namespace Lightcore.Common.Spherical.Models
{
    using Lightcore.Common.Models;
    using Lightcore.Common.Spherical.Extensions;
    using System.Linq;

    public class SphericalTriangle : Indexable<Vector>
    {
        public SphericalTriangle(params Vector[] elements) : base(elements.Select(element => new Vector(1, element[Axis.Theta], element[Axis.Phi])).ToArray())
        {
            Orthodromes = this.ToOrthodromes();
        }

        public Orthodrome[] Orthodromes { get; private set; }
    }


}
