namespace Lightcore.Lighting.Models
{
    using Lightcore.Common.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class LightMapSearchList 
    {
        private SortedDictionary<AngleSpan, List<LightMapElement>> segments = new SortedDictionary<AngleSpan, List<LightMapElement>>(new AngleSpanComparer());
        private int j = 0;
        private int threadshold;
        private int cleanupInterval = 5000;
        private int maxSegments = 1000;

        public LightMapSearchList(int resolution = 100000, int threadshold = 50)
        {
            this.threadshold = threadshold;
            var stepSize = (Constants.PI2) / resolution;

            for (int i = 0; i < resolution; i ++)
            {
                segments.Add(new AngleSpan(i * stepSize, (i + 1) * stepSize, AngleSpanDirection.CounterClockwise), new List<LightMapElement>());
            }
        }

        public void Add(LightMapElement lightMapElement)
        {
            foreach (var segment in GetSegments(lightMapElement))
                    segments[segment].Add(lightMapElement);

            j++;

            if (j > cleanupInterval - 1 && segments.Count() < maxSegments)
            {
                Cleanup();
                j = 0;
            }
        }

        public IEnumerable<LightMapElement> Get(LightMapElement lightMapElement)
        {
            var s = GetSegments(lightMapElement);

            if (s.Count == 1)
                return segments[s[0]];

            var result = new List<LightMapElement>();

            foreach (var segment in s)
                if (segment.ContainsSome(lightMapElement.Phi))
                    result.AddRange(segments[segment]);

            return result.Distinct().ToList();
        }

        private void Cleanup()
        {
            foreach (var segment in segments.Keys.ToArray())
            {
                if (segments.Count() > maxSegments)
                    return;

                if (segments[segment].Count() > threadshold)
                {
                    var splitAngle = segment.StartAngle + segment.Width / 2;
                    var elements = segments[segment].ToList();


                    var angle0 = new AngleSpan(segment.StartAngle, splitAngle, AngleSpanDirection.CounterClockwise);
                    var angle0elements = elements.Where(element => angle0.ContainsSome(element.Phi)).ToList();

                    if (angle0elements.Count() == segments[segment].Count)
                        continue;

                    var angle1 = new AngleSpan(splitAngle, segment.EndAngle, AngleSpanDirection.CounterClockwise);
                    var angle1elements = elements.Where(element => angle1.ContainsSome(element.Phi)).ToList();

                    segments.Remove(segment);
                    segments.Add(angle0, angle0elements);
                    segments.Add(angle1, angle1elements);
                }
            }
        }

        private List<AngleSpan> GetSegments(LightMapElement lightMapElement)
        {
            var result = new List<AngleSpan>();

            var zeroCross = lightMapElement.Phi.Includes(0);
            var found = false;

            foreach (var segment in segments.Keys)
            { 
                if (segment.ContainsSome(lightMapElement.Phi))
                {
                    found = true;
                    result.Add(segment);
                }
                else
                {
                    if (!zeroCross && found)
                        return result;
                }
            }

            return result;
        }


        public IEnumerable<LightMapElement> GetAll()
        {
            var result = new List<LightMapElement>();

            foreach(var segment in segments.Keys)
                    result.AddRange(segments[segment]);

            return result.Distinct();
        }
    }

    class AngleSpanComparer : IComparer<AngleSpan>
    {
        public int Compare(AngleSpan x, AngleSpan y)
        {
            if (x.StartAngle < y.StartAngle)
                return -1;
            if (x.StartAngle > y.StartAngle)
                return 1;
            return 0;
        }
    }
}
