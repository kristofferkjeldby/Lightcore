﻿namespace Lightcore.Textures.Gradients.Models
{
    using Lightcore.Common.Models;

    public class ColorPoint
    {
        public Vector Color { get; set; }

        public double Value { get; set; }

        public bool Locked { get; set; }

        public ColorPoint(Vector color, double value, bool locked)
        {
            Color = color;
            Value = value;
            Locked = locked;
        }
    }
}
