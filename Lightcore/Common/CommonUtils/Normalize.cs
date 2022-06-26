﻿namespace Lightcore.Common
{
    using Lightcore.Common.Extensions;
    using Lightcore.Common.Models;
    using System;

    public static partial class CommonUtils
    {
        public static Vector Normalize(ReferenceFrame source, Vector vector)
        {
            if (source.ReferenceFrameType == ReferenceFrameType.Cartesian)
                return (vector * source.Base.Transpose()) + source.Origon;

            if (source.ReferenceFrameType == ReferenceFrameType.Spherical)
                    return ((vector.ToCartesian() * source.Base.Transpose()) + source.Origon).ToSpherical();

            throw new Exception();
        }

    }
}
