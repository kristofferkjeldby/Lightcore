namespace Lightcore.Common.Models.Transformations
{
    public class LinearTransformation : Transformation
    {
        public Matrix Matrix { get; set; }

        public LinearTransformation(Matrix matrix)
        {
            Matrix = matrix;
        }

        public override Vector Transform(Vector vector)
        {
            return Matrix * vector;
        }
    }
}
