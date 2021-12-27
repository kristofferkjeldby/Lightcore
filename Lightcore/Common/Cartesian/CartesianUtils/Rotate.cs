namespace Lightcore.Common.Cartesian
{
    using Lightcore.Common.Models;
    using System;

    public static partial class CartesianUtils
    {
        public static Matrix Rotate(Vector axis, float angle)
        {
            var tr = t(angle);
            var cos = c(angle);
            var sin = s(angle);

            return new Matrix(
                new Vector(a1(angle, axis, tr, cos), b1(angle, axis, tr, sin), c1(angle, axis, tr, sin)),
                new Vector(a2(angle, axis, tr, sin), b2(angle, axis, tr, cos), c2(angle, axis, tr, sin)),
                new Vector(a3(angle, axis, tr, sin), b3(angle, axis, tr, sin), c3(angle, axis, tr, cos))
            );
        }

        private static float t(float angle)
        {
            return 1 - (float)Math.Cos(angle);
        }

        private static float c(float angle)
        {
            return (float)Math.Cos(angle);
        }

        private static float s(float angle)
        {
            return (float)Math.Sin(angle);
        }

        private static float a1(float angle, Vector axis, float tr, float cos)
        {
            return (tr * axis[0] * axis[0]) + cos;
        }

        private static float a2(float angle, Vector axis, float tr, float sin)
        {
            return (tr * axis[0] * axis[1]) - (sin * axis[2]);
        }

        private static float a3(float angle, Vector axis, float tr, float sin)
        {
            return (tr * axis[0] * axis[2]) + (sin * axis[1]);
        }

        private static float b1(float angle, Vector axis, float tr, float sin)
        {
            return (tr * axis[0] * axis[1]) + (sin * axis[2]);
        }

        private static float b2(float angle, Vector axis, float tr, float cos)
        {
            return (tr * axis[1] * axis[1]) + cos;
        }

        private static float b3(float angle, Vector axis, float tr, float sin)
        {
            return (tr * axis[1] * axis[2]) - (sin * axis[0]);
        }

        private static float c1(float angle, Vector axis, float tr, float sin)
        {
            return (tr * axis[0] * axis[2]) - (sin * axis[1]);
        }

        private static float c2(float angle, Vector axis, float tr, float sin)
        {
            return (tr * axis[1] * axis[2]) + (sin * axis[0]);
        }

        private static float c3(float angle, Vector axis, float tr, float cos)
        {
            return (tr * axis[2] * axis[2]) + cos;
        }
    }
}
