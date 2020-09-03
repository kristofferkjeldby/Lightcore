namespace Lightcore.Common.Models
{
    public abstract class Indexable<T>
    {
        public Indexable(params T[] elements)
        {
            Elements = elements;
        }

        public T[] Elements { get; set; }

        public int Count()
        {
            return Elements.Length;
        }

        public T this[int index]
        {
            get
            {
                return Elements[index];
            }

            set
            {
                Elements[index] = value;
            }
        }

        public T this[object index]
        {
            get
            {
                return Elements[(int)index];
            }

            set
            {
                Elements[(int)index] = value;
            }
        }
    }
}
