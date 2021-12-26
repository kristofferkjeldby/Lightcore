namespace Lightcore.Common.Models
{
    using System;

    public interface ITransformable<T>
    {
        T Transform(Func<Vector, Vector> transformation);
        T Transform(Matrix transformation);
    }
}
