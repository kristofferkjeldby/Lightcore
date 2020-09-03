namespace Lightcore.Common.Models
{
    using System;
    using System.Collections.Generic;

    public class Vector : Indexable<double>, IClonable<Vector>, ITransformable<Vector>, IIdentifiable, IEquatable<Vector>
    {
        public Vector(double[] elements, Guid? id = null) : base (elements)
        {
            Id = id.GetValueOrDefault(Guid.NewGuid());
        }

        public Vector(double a, double b, double c, Guid? id = null) : base(a, b, c)
        {
            Id = id.GetValueOrDefault(Guid.NewGuid());
        }

        public static Vector operator +(Vector a, Vector b) => new Vector(a[0] + b[0], a[1] + b[1], a[2] + b[2]);
        public static Vector operator -(Vector a, Vector b) => new Vector(a[0] - b[0], a[1] - b[1], a[2] - b[2]);
        public static Vector operator -(Vector a) => new Vector(-a[0], -a[1], -a[2]);
        public static Vector operator *(double a, Vector b) => new Vector(a * b[0], a * b[1], a * b[2]);
        public static Vector operator *(Vector a, double b) => b * a;
        public static Vector operator /(Vector a, double b) => new Vector(a[0] / b, a[1] / b, a[2] / b);
        public static double operator *(Vector a, Vector b) => a[0] * b[0] + a[1] * b[1] + a[2] * b[2];
        public static Vector operator &(Vector a, Vector b) => new Vector(a[0] * b[0], a[1] * b[1], a[2] * b[2]);
        public static Vector operator %(Vector a, Vector b) => new Vector((a[1] * b[2]) - (b[1] * a[2]), (a[2] * b[0]) - (b[2] * a[0]), (a[0] * b[1]) - (b[0] * a[1]));

        public override string ToString()
        {
            return $"({Elements[0].ToString(Constants.DeltaFormat)}, {Elements[1].ToString(Constants.DeltaFormat)}, {Elements[2].ToString(Constants.DeltaFormat)})";
        }

        public Guid Id { get; set; }

        public Vector Clone()
        {
            return new Vector(Elements, Id);
        }

        public Vector Transform(Func<Vector, Vector> transformation)
        {
            return transformation(this);
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
            for (int i = 0; i < this.Count(); i++)
            {
                if (Math.Abs(this[i] - other[i]) >= Constants.Delta)
                    return false;
            }

            return true;
        }
    }
}
