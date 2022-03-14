namespace Lightcore.Common.Models.Transformations
{
    using System;

    public class FuncTransformation : Transformation
    {
        public Func<Vector, Vector> Func { get; set; }

        public FuncTransformation(Func<Vector, Vector> func)
        {
            Func = func;
        }

        public override Vector Transform(Vector vector)
        {
            return Func(vector);
        }
    }
}
