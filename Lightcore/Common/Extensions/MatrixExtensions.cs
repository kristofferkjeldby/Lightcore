namespace Lightcore.Common.Extensions
{
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MatrixExtensions
    {
        public static Matrix ToEchelonForm(this Matrix matrix)
        {
            var rows =
                matrix.Rows.OrderBy(
                    row =>
                    {
                        row.LeadCoefficient(out var m);
                        return m ?? 0;
                    }
                ).ToArray();

            for (int n1 = 0; n1 < rows.Length; n1++)
            {
                var leadCoefficient = rows[n1].LeadCoefficient(out var m);

                if (m == null)
                    break;

                rows[n1] *= 1 / leadCoefficient;

                for (int n2 = 0; n2 < rows.Length; n2++)
                {
                    if (n1 == n2)
                        continue;

                    rows[n2] -= (rows[n2][m] * rows[n1]);
                }
            }

            return Matrix.FromRows(rows);
        }

        public static Matrix Join(this Matrix matrix, Matrix otherMatrix)
        {
            var columns = matrix.Columns.ToList();
            columns.AddRange(otherMatrix.Columns);
            return new Matrix(columns);
        }

        public static Tuple<Matrix, Matrix> Split(this Matrix matrix, int column)
        {
            var matrix1 = new Matrix(matrix.Columns.Take(column));
            var matrix2 = new Matrix(matrix.Columns.Skip(column));
            return new Tuple<Matrix, Matrix>(matrix1, matrix2);
        }

        public static Matrix Invert(this Matrix matrix)
        {
            if (!matrix.IsSquare)
                throw new Exception("Invert of non-square matrix is not defined");

            var augmented = matrix.Join(Matrix.Identity(matrix.N));

            var echolonForm = augmented.ToEchelonForm();

            var splited = echolonForm.Split(matrix.N);

            // Matrix cannot be inverted
            if (!splited.Item1.IsIdentity)
                return null;

            return splited.Item2;
        }

        public static float Determinant(this Matrix matrix)
        {
            if (!matrix.IsSquare)
                throw new Exception("Determinant of non-square matrix is not defined");

            if (!(matrix.N == 2))
                throw new Exception("Determinant of dimensions over 2 is not implemented");

            return (matrix.Columns[0][0] * matrix.Columns[1][1]) - (matrix.Columns[0][1] * matrix.Columns[1][0]);
        }
    }
}
