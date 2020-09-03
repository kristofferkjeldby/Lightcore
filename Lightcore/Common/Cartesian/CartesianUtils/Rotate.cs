namespace Lightcore.Common.Cartesian
{
    using Lightcore.Common.Models;
    using System;

    public static partial class CartesianUtils
    {
        public static Matrix Rotate(Vector axis, double angle)
        {
            double tr = t(angle);
            double cos = c(angle);
            double sin = s(angle);

            return new Matrix(
                new Vector(a1(angle, axis, tr, cos), b1(angle, axis, tr, sin), c1(angle, axis, tr, sin)),
                new Vector(a2(angle, axis, tr, sin), b2(angle, axis, tr, cos), c2(angle, axis, tr, sin)),
                new Vector(a3(angle, axis, tr, sin), b3(angle, axis, tr, sin), c3(angle, axis, tr, cos))
            );
        }

        private static double t(double angle)
        {
            return 1 - Math.Cos(angle);
        }

        private static double c(double angle)
        {
            return Math.Cos(angle);
        }

        private static double s(double angle)
        {
            return Math.Sin(angle);
        }

        private static double a1(double angle, Vector axis, double tr, double cos)
        {
            return (tr * axis[0] * axis[0]) + cos;
        }

        private static double a2(double angle, Vector axis, double tr, double sin)
        {
            return (tr * axis[0] * axis[1]) - (sin * axis[2]);
        }

        private static double a3(double angle, Vector axis, double tr, double sin)
        {
            return (tr * axis[0] * axis[2]) + (sin * axis[1]);
        }

        private static double b1(double angle, Vector axis, double tr, double sin)
        {
            return (tr * axis[0] * axis[1]) + (sin * axis[2]);
        }

        private static double b2(double angle, Vector axis, double tr, double cos)
        {
            return (tr * axis[1] * axis[1]) + cos;
        }

        private static double b3(double angle, Vector axis, double tr, double sin)
        {
            return (tr * axis[1] * axis[2]) - (sin * axis[0]);
        }

        private static double c1(double angle, Vector axis, double tr, double sin)
        {
            return (tr * axis[0] * axis[2]) - (sin * axis[1]);
        }

        private static double c2(double angle, Vector axis, double tr, double sin)
        {
            return (tr * axis[1] * axis[2]) + (sin * axis[0]);
        }

        private static double c3(double angle, Vector axis, double tr, double cos)
        {
            return (tr * axis[2] * axis[2]) + cos;
        }
    }
}
