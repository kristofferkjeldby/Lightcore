namespace Lightcore.Common.Models
{
    using System;
    using System.Collections.Generic;

    public class AngleSpan
    {
        private float determinant;

        public AngleSpan(float startAngle, float endAngle, AngleSpanDirection? angleSpanDirection = null) : this (new Angle(startAngle), new Angle(endAngle), angleSpanDirection)
        {

        }

        public AngleSpan(Angle startAngle, Angle endAngle, AngleSpanDirection? angleSpanDirection = null)
        {

            StartAngle = startAngle;
            EndAngle = endAngle;
            determinant = endAngle.Value - startAngle.Value;

            AngleSpanDirection = 
                angleSpanDirection.GetValueOrDefault(Angle.IsReflectAngle(Angle.ToCyclicAngle(determinant)) ? 
                AngleSpanDirection.Clockwise : 
                AngleSpanDirection.CounterClockwise);
        }

        public AngleSpanDirection AngleSpanDirection { get; private set; }

        public AngleSpan Flip()
        {
            return new AngleSpan(EndAngle, StartAngle, AngleSpanDirection == AngleSpanDirection.CounterClockwise ? AngleSpanDirection.Clockwise : AngleSpanDirection.CounterClockwise);
        }

        public static AngleSpan operator +(AngleSpan a, AngleSpan b) 
        {
            return new AngleSpan(Math.Min(a.StartAngle, b.StartAngle), Math.Max(a.EndAngle, b.EndAngle));
        }

        public float Length
        {
            get
            {
                if (determinant == 0 && AngleSpanDirection == AngleSpanDirection.CounterClockwise)
                    return Constants.PI2;
                return (((AngleSpanDirection == AngleSpanDirection.CounterClockwise ? 1 : -1) * Constants.PI2) + determinant) % Constants.PI2;
            }
        }

        public float Width => Math.Abs(Length);

        public Angle StartAngle { get; set; }

        public Angle EndAngle { get; set; }

        public AngleSpan Union(AngleSpan otherAngleSpan)
        {
            var o = (AngleSpanDirection != otherAngleSpan.AngleSpanDirection) ? otherAngleSpan.Flip() : otherAngleSpan;
            if (Includes(o.StartAngle) && !Includes(o.EndAngle))
                return new AngleSpan(StartAngle, o.EndAngle, AngleSpanDirection);
            if (!Includes(o.StartAngle) && Includes(o.EndAngle))
                return new AngleSpan(o.StartAngle, EndAngle, AngleSpanDirection);
            if (Includes(o.StartAngle) && Includes(o.EndAngle))
                return new AngleSpan(StartAngle, EndAngle, AngleSpanDirection);
            if (o.Includes(StartAngle) && o.Includes(EndAngle))
                return new AngleSpan(o.StartAngle, o.EndAngle, AngleSpanDirection);

            return null;
        }

        public bool ContainsAll(AngleSpan otherAngleSpan)
        {
            var o = (AngleSpanDirection != otherAngleSpan.AngleSpanDirection) ? otherAngleSpan.Flip() : otherAngleSpan;
            if (Includes(o.StartAngle) && Includes(o.EndAngle))
                return true;
 
            return false;
        }

        public bool ContainsSome(AngleSpan otherAngleSpan)
        {
            var o = (AngleSpanDirection != otherAngleSpan.AngleSpanDirection) ? otherAngleSpan.Flip() : otherAngleSpan;
            if (Includes(o.StartAngle) && !Includes(o.EndAngle))
                return true;
            if (!Includes(o.StartAngle) && Includes(o.EndAngle))
                return true;
            if (Includes(o.StartAngle) && Includes(o.EndAngle))
                return true;
            if (o.Includes(StartAngle) && o.Includes(EndAngle))
                return true;

            return false;
        }

        public bool Includes(float angle)
        {
            return Includes(new Angle(angle));
        }

        public bool Includes(Angle angle)
        {
            if (angle.Value == StartAngle.Value || angle.Value == EndAngle.Value)
                return true;

            var ls = angle > StartAngle;
            var le = angle > EndAngle;

            if (AngleSpanDirection == AngleSpanDirection.CounterClockwise)
                return (determinant > 0) ? ls && !le : ls || !le;
            return (determinant > 0) ? !ls || le : !ls && le;
        }

        public override string ToString()
        {
            return (AngleSpanDirection == AngleSpanDirection.CounterClockwise) ? $"{StartAngle} -> {EndAngle}" : $"{StartAngle} <- {EndAngle}";
        }

        public override bool Equals(object obj)
        {
            if (obj is AngleSpan otherAngleSpan)
            {
                if (otherAngleSpan.AngleSpanDirection == AngleSpanDirection)
                    return StartAngle.Equals(otherAngleSpan.StartAngle) && EndAngle.Equals(otherAngleSpan.EndAngle);
                return StartAngle.Equals(otherAngleSpan.EndAngle) && EndAngle.Equals(otherAngleSpan.StartAngle);
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1321652047;
            hashCode = hashCode * -1521134295 + EqualityComparer<Angle>.Default.GetHashCode(StartAngle);
            hashCode = hashCode * -1521134295 + EqualityComparer<Angle>.Default.GetHashCode(EndAngle);
            return hashCode;
        }
    }
}
