namespace Lightcore.Common.Spherical.Models
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;

    public class Orthodrome : GreatCircle
    {
        public Orthodrome(params Vector[] elements) : base(elements)
        {

        }

        public AngleSpan Theta => new AngleSpan(Elements[0][Axis.Theta], Elements[1][Axis.Theta]);

        public AngleSpan Phi => new AngleSpan(Elements[0][Axis.Phi], Elements[1][Axis.Phi]);

        public double Angle => CartesianUtils.Angle(Elements[0].ToCartesian(), Elements[1].ToCartesian());
    }
}
