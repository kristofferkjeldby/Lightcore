namespace Lightcore.Common
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using System;

    public static partial class CommonUtils
    {
        public static Func<Vector, Vector> ReferenceFrameTransformation(ReferenceFrame source, ReferenceFrame destination)
        {
            if (source.ReferenceFrameType == ReferenceFrameType.Cartesian && destination.ReferenceFrameType == ReferenceFrameType.Cartesian)
                return (Vector vector) =>
                {
                    return (Normalize(source, vector) - destination.Origon) * destination.Passive;
                };

            if (source.ReferenceFrameType == ReferenceFrameType.Cartesian && destination.ReferenceFrameType == ReferenceFrameType.Spherical)
                return (Vector vector) =>
                {
                    return (Normalize(source, vector) - destination.Origon * destination.Passive).ToSpherical();
                };

            if (source.ReferenceFrameType == ReferenceFrameType.Spherical && destination.ReferenceFrameType == ReferenceFrameType.Cartesian)
                return (Vector vector) =>
                {
                    return (Normalize(source, vector).ToCartesian() - destination.Origon * destination.Passive);
                };

            if (source.ReferenceFrameType == ReferenceFrameType.Spherical && destination.ReferenceFrameType == ReferenceFrameType.Spherical)
                return (Vector vector) =>
                {
                    return (Normalize(source, vector).ToCartesian() - destination.Origon * destination.Passive).ToSpherical();
                };

            throw new Exception();
        }
    }
}
