namespace Lightcore.Common.Models
{
    using System;

    public interface ITransformable<T>
    {
        T Transform(Func<Vector, Vector> transformation);
    }

    public interface ITransformable
    {
        void Transform(Func<Vector, Vector> transformation);
    }
}
