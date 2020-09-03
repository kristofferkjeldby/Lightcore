namespace Lightcore.Common.Models
{
    public class Angle
    {
        public Angle(double value)
        {
            Value = ToCyclicAngle(value);
        }

        public static Angle operator +(Angle a, Angle b) => new Angle(a.Value + b.Value);
        public static Angle operator -(Angle a, Angle b) => new Angle(a.Value - b.Value);
        public static bool operator <(Angle a, Angle b) => a.Value < b.Value;
        public static bool operator >(Angle a, Angle b) => a.Value > b.Value;
        public static bool operator <=(Angle a, Angle b) => a.Value <= b.Value;
        public static bool operator >=(Angle a, Angle b) => a.Value >= b.Value;
        public static bool operator ==(Angle a, Angle b) => a.Value == b.Value;
        public static bool operator !=(Angle a, Angle b) => a.Value != b.Value;

        public static implicit operator double(Angle a) => a.Value;

        public double Value { get; }

        public Angle Opposite => new Angle(Constants.PI2 - Value);

        public bool IsReflect => IsReflectAngle(Value);

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Angle otherAngle)
            {
                return Value.Equals(otherAngle.Value);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }

        public static bool IsReflectAngle(double angle)
        {
            return angle > Constants.PI;
        }

        public static double ToCyclicAngle(double angle)
        {
            if (angle >= 0)
                return angle % Constants.PI2;
            else
                return Constants.PI2 + angle % Constants.PI2;
        }
    }
}
