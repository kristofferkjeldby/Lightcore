namespace Lightcore.Common
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using Lightcore.Common.Models.Transformations;
    using System;

    public static partial class CommonUtils
    {
        public static FuncTransformation ReferenceFrameTransformation(ReferenceFrame source, ReferenceFrame destination)
        {
            var translate = source.Origon - destination.Origon;

            if (source.ReferenceFrameType == ReferenceFrameType.Cartesian && destination.ReferenceFrameType == ReferenceFrameType.Cartesian)
                return new FuncTransformation((Vector vector) =>
                {
                    return (Normalize(source, vector) - destination.Origon) * destination.Passive;
                });

            if (source.ReferenceFrameType == ReferenceFrameType.Cartesian && destination.ReferenceFrameType == ReferenceFrameType.Spherical)
                return new FuncTransformation((Vector vector) =>
                {
                    return (Normalize(source, vector) - destination.Origon * destination.Passive).ToSpherical();
                });

            if (source.ReferenceFrameType == ReferenceFrameType.Spherical && destination.ReferenceFrameType == ReferenceFrameType.Cartesian)
                return new FuncTransformation((Vector vector) =>
                {
                    return (Normalize(source, vector).ToCartesian() - destination.Origon * destination.Passive);
                });

            if (source.ReferenceFrameType == ReferenceFrameType.Spherical && destination.ReferenceFrameType == ReferenceFrameType.Spherical)
                return new FuncTransformation((Vector vector) =>
                {
                    return (Normalize(source, vector).ToCartesian() - destination.Origon * destination.Passive).ToSpherical();
                });

            throw new Exception();
        }
    }
}
