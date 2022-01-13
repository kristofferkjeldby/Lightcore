namespace Lightcore.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Lightcore.Common.Cartesian.Extensions;

    public class Matrix : Indexable<Vector>, IIdentifiable, IClonable<Matrix>
    {
        public Matrix(Vector[] elements, Guid? id = null) : base(elements)
        {
            Id = id.GetValueOrDefault(Guid.NewGuid());
        }

        public Matrix(IEnumerable<Vector> elements, Guid? id = null) : this(elements.ToArray(), id)
        {

        }

        public Matrix(int rows, int columns, Guid? id = null) : this(Enumerable.Repeat(new Vector(rows), columns), id)
        {

        }

        public Matrix(params Vector[] columns) : base(columns)
        {

        }

        public static Matrix Identity(int dimension)
        {
            var columns = new Vector[dimension];

            for (int m = 0; m < dimension; m++)
            {
                var column = new Vector(dimension);
                column[m] = 1;
                columns[m] = column;
            }

            return new Matrix(columns);

        }

        public static Matrix FromRows(Vector[] rows, Guid? id = null)
        {
            return new Matrix(rows, id).Transpose();
        }

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public int N => Elements.First().N;

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public int M => Elements.Count();

        public bool IsSquare => M.Equals(N);

        public bool IsIdentity
        {
            get
            {
                if (!IsSquare)
                    return false;

                for (var m = 0; m < M; m++)
                {
                    for (var n = 0; n < N; n++)
                    {
                        if (m == n)
                        {
                            if (Elements[m][n] != 1)
                                return false;
                        }
                        else
                        {
                            if (Elements[m][n] != 0)
                                return false;
                        }
                    }

                };

                return true;
            }
        }

        public Vector[] Rows => Enumerable.Range(0, N).Select(n => GetRow(n)).ToArray();

        public Vector[] Columns => Elements;

        public Guid Id { get; set; }

        // Matrix dot product
        public static Matrix operator *(Matrix a, Matrix b) => new Matrix(b.Columns.Select(column => new Vector(a.Rows.Select(row => row * column))));

        public static Vector operator *(Matrix a, Vector b) => new Vector(a.Rows.Select(row => row * b));

        public static Vector operator *(Vector a, Matrix b) => new Vector(b.Columns.Select(column => column * a));

        public float this[int row, int column] 
        {
            get
            {
                return this[column][row];
            }
            set
            {
                this[column][row] = value;
            }

        }

        public Vector GetRow(int n)
        {
            return new Vector(Elements.Select(e => e[n]).ToArray());
        }

        public override string ToString() => $"({string.Join(", ", Elements.Select(element => element.ToString()))})";

        public Matrix Unit()
        {
            return new Matrix(Elements.Select(element => element.Unit()));
        }

        public Matrix Transpose()
        {
            return new Matrix(Rows);
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

        public Matrix Clone()
        {
            return new Matrix(Elements.Select(element => element.Clone()), Id);
        }
    }
}
