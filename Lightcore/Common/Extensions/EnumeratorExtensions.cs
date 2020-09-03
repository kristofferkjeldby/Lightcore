namespace Lightcore.Common.Extensions
{
    using System.Collections.Generic;

    public static class EnumeratorExtensions
    {

        public static T Get<T>(this IEnumerator<T> enumerator)
        {
            enumerator.MoveNext();
            return enumerator.Current;
        }
    }
}
