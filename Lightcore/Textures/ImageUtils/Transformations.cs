using Lightcore.Common.Extensions;
using Lightcore.Common.Models;
using Lightcore.Common.Models.Transformations;

namespace Lightcore.Textures
{
    public static partial class ImageUtils
    {
        public static PerspectiveTransformation SquareToQuadrilateralTransformation(Vector[] destination)
        {
            var xy0 = destination[0];
            var xy1 = destination[1];
            var xy2 = destination[2];
            var xy3 = destination[3];

            var d01 = xy0 - xy1;
            var d23 = xy2 - xy3;
            var d12 = xy1 - xy2;
            var d32 = xy3 - xy2;
            var d0123 = d01 + d23;

            float a11, a21, a31, a12, a22, a32, a13, a23, a33;

            // Affine
            if (d0123[Axis.X] == 0 && d0123[Axis.Y] == 0)
            {
                var d10 = xy1 - xy0;
                var d21 = xy2 - xy1;

                a11 = d10[Axis.X];
                a12 = d21[Axis.X];
                a13 = xy0[Axis.X];
                a21 = d10[Axis.Y];
                a22 = d21[Axis.Y];
                a23 = xy0[Axis.Y];
                a31 = 0;
                a32 = 0;
                a33 = 1;
            }
            else
            {
                a31 = new Matrix(d0123, d32).Determinant() / new Matrix(d12, d32).Determinant();
                a32 = new Matrix(d12, d0123).Determinant() / new Matrix(d12, d32).Determinant();
                var a1x = (xy1 - xy0) + (a31 * xy1);
                var a2x = (xy3 - xy0) + (a32 * xy3);
                a11 = a1x[Axis.X];
                a12 = a2x[Axis.X];
                a21 = a1x[Axis.Y];
                a22 = a2x[Axis.Y];
                a13 = xy0[Axis.X];
                a23 = xy0[Axis.Y];
                a33 = 1;
            }

            var transformation = new Matrix(
                new Vector(a11, a21, a31),
                new Vector(a12, a22, a32),
                new Vector(a13, a23, a33)
            );

            return new PerspectiveTransformation(transformation, 1);
        }
    }
}
