namespace Lightcore.Common.Models
{
    public class ReferenceFrame
    {
        public ReferenceFrame(Matrix @base, Vector origon, ReferenceFrameType referenceFrameType = ReferenceFrameType.Cartesian)
        {
            ReferenceFrameType = referenceFrameType;
            Base = @base;
            Origon = origon;
        }

        public ReferenceFrameType ReferenceFrameType { get; }

        public Matrix Base { get; set; }

        public Vector Origon { get; set; }
    }
}
