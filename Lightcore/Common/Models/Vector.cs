namespace Lightcore.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Vector : Indexable<float>, IClonable<Vector>, ITransformable<Vector>, IIdentifiable, IEquatable<Vector>
    {
        public Vector(float[] elements, Guid? id = null) : base (elements)
        {
            Id = id.GetValueOrDefault(Guid.NewGuid());
        }

        public Vector(IEnumerable<float> elements, Guid? id = null) : this(elements.ToArray(), id)
        {

        }

        public Vector(int rows, Guid? id = null) : this(Enumerable.Repeat(default(float), rows).ToArray(), id)
        {

        }

        public Vector(params float[] elements) : base(elements)
        {

        }

        public int N => Elements.Length;

        // Vector addition
        public static Vector operator +(Vector a, Vector b) => new Vector(a.Elements.Select((e, row) => e + b[row]));
        public static Vector operator -(Vector a, Vector b) => new Vector(a.Elements.Select((e, row) => e - b[row]));

        public static Vector operator -(Vector a) => -1f * a;

        // Scalar multiplication (commutative)
        public static Vector operator *(float a, Vector b) => new Vector(b.Elements.Select((e, row) => a * e));
        public static Vector operator *(Vector a, float b) => b * a;

        // Scalar division (commutative)
        public static Vector operator /(float a, Vector b) => (1f / a) * b;
        public static Vector operator /(Vector a, float b) => a * (1f / b);

        // Dot product
        public static float operator *(Vector a, Vector b) => a.Elements.Select((e, row) => e * b[row]).Sum();

        // Weight one vector with another (dot product of vectors as a row and a column matrix)
        public static Vector operator &(Vector a, Vector b) => new Vector(a[0] * b[0], a[1] * b[1], a[2] * b[2]);

        // Cross product (only defined in tree dimensions)
        public static Vector operator %(Vector a, Vector b) => new Vector((a[1] * b[2]) - (b[1] * a[2]), (a[2] * b[0]) - (b[2] * a[0]), (a[0] * b[1]) - (b[0] * a[1]));

        public override string ToString() => $"({string.Join(", ", Elements.Select(element => element.ToString(Constants.DeltaFormat)))})";

        public Guid Id { get; set; }

        public Vector Clone()
        {
            return new Vector(Elements, Id);
        }

        public Vector Transform(Func<Vector, Vector> transformation)
        {
            return transformation(this);
        }

        public Vector Transform(Matrix transformation)
        {
            return Transform(v => transformation * v);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(Vector other)
        {
            for (int row = 0; row < this.N; row++)
            {
                if (Math.Abs(this[row] - other[row]) >= Constants.Delta)
                    return false;
            }

            return true;
        }
    }
}
