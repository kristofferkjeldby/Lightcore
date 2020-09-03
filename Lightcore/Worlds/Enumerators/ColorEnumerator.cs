namespace Lightcore.Worlds.Enumerators
{
    using Lightcore.Common.Models;
    using System.Collections;
    using System.Collections.Generic;

    public class ColorEnumerator : IEnumerable<Vector>
    {
        private Vector unit;
        private Vector startColor;
        private int steps;

        public ColorEnumerator(int steps, Vector startColor, Vector unit)
        {
            this.unit = unit/steps;
            this.startColor = startColor;
            this.steps = steps;
        }

        public IEnumerator<Vector> GetEnumerator()
        {
            while (true)
            {
                for (var i = 0; i < steps; i++)
                {
                    yield return startColor + (i * unit);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
