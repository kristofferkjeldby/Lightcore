namespace Lightcore.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Lightcore.Common.Cartesian.Extensions;

    public class Matrix : Indexable<Vector>, IIdentifiable
    {
        public Matrix(Vector[] elements, Guid? id = null) : base(elements)
        {
            Id = id.GetValueOrDefault(Guid.NewGuid());
        }

        public Matrix(Vector column0, Vector column1, Vector column2, Guid? id = null) : base(column0, column1, column2)
        {
            Id = id.GetValueOrDefault(Guid.NewGuid());
        }

        public Vector Row0 => new Vector(Elements[0][0], Elements[1][0], Elements[2][0]);
        public Vector Row1 => new Vector(Elements[0][1], Elements[1][1], Elements[2][1]);
        public Vector Row2 => new Vector(Elements[0][2], Elements[1][2], Elements[2][2]);

        public Guid Id { get; set; }

        public static Vector operator *(Vector a, Matrix b) => new Vector(a * b.Elements[0], a * b.Elements[1], a * b.Elements[2]);
        public static Vector operator *(Matrix a, Vector b) => b * a.Transpose();
        public static Matrix operator *(Matrix a, Matrix b) => new Matrix(a * b[0], a * b[1], a * b[2]);

        public override string ToString()
        {
            return $"({Elements[0]},{Elements[1]},{Elements[2]})";
        }

        public Matrix Unit()
        {
            return new Matrix(Elements[0].Unit(), Elements[1].Unit(), Elements[2].Unit());
        }

        public Matrix Transpose()
        {
            return new Matrix
            (
                Row0,
                Row1,
                Row2
            );
        }

        public override bool Equals(object obj)
        {
            if (obj is Matrix otherMatrix)
            {
                for (int i = 0; i < this.Count(); i++)
                {
                    if (!this[i].Equals(otherMatrix[i]))
                        return false;
                }

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<Guid>.Default.GetHashCode(Id);
        }
    }
}
