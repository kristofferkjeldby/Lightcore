namespace Lightcore.Common.Models
{
    public class ReferenceFrame
    {
        public ReferenceFrame(Matrix unit, Vector origon, ReferenceFrameType referenceFrameType = ReferenceFrameType.Cartesian)
        {
            ReferenceFrameType = referenceFrameType;
            Passive = unit;
            Origon = origon;
        }

        public ReferenceFrameType ReferenceFrameType { get; }

        public Matrix Passive { get; set; }

        public Matrix Active => Passive.Transpose();

        public Vector Origon { get; set; }
    }
}
