namespace Lightcore.Common.Models.Transformations
{
    public interface ITransformable<T>
    {
        T Transform(Transformation transformation);
    }
}
