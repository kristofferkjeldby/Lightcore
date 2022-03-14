using Lightcore.Common.Extensions;

namespace Lightcore.Common.Models.Transformations
{
    public class PerspectiveTransformation : Transformation
    {
        public Matrix Matrix { get; set; }

        public float Plane { get; set; }

        public PerspectiveTransformation(Matrix matrix, float plane)
        {
            Matrix = matrix;
            Plane = plane;
        }

        public override Vector Transform(Vector vector)
        {
            return (Matrix * vector.ToHomogeneous(Plane)).FromHomogeneous();
        }
    }
}
