namespace Lightcore.Textures.Extensions
{
    using Lightcore.Common;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Common.Cartesian.Models;
    using Lightcore.Common.Models;
    using Lightcore.Textures.Models;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;

    public static class BitmapExtensions
    {
        public static Bitmap Crop(this Bitmap image, RectangleF rect)
        {
            return image.Clone(rect, image.PixelFormat);
        }

        public static Bitmap Colorize(this Bitmap image, Vector color, float transparency)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] {color[0], 0, 0, 0, 0},
                    new float[] {0, color[1], 0, 0, 0},
                    new float[] {0, 0, color[2], 0, 0},
                    new float[] {0, 0, 0, 1-transparency, 0},
                    new float[] {0, 0, 0, 0, 1},
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            Point[] points =
            {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };

            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the resulting bitmap
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
            }

            return bm;
        }

        public static Bitmap ForwardMap(this Bitmap image, PointF[] points)
        {
            var maxX = points.Max(point => (int)point.X);
            var maxY = points.Max(point => (int)point.Y);
            var minX = points.Min(point => (int)point.X);
            var minY = points.Min(point => (int)point.Y);

            var destinationImage = new Bitmap(maxX - minX , maxY - minY);

            var transformation = ImageUtils.SquareToQuadrilateralTransformation(points.Select(p => p.ToVector()).ToArray());

            for (int u = 0; u < image.Width; u++)
            {
                for (int v = 0; v < image.Height; v++)
                {
                    var color = image.GetPixel(u, v);

                    var uv = new Vector((float)u / image.Width, (float)v / image.Height, 0);

                    var xy = transformation(uv);

                    if (xy[Axis.X] < destinationImage.Width && xy[Axis.Y] < destinationImage.Height && xy[Axis.X] >= 0 && xy[Axis.Y] >= 0)
                        destinationImage.SetPixel((int)xy[Axis.X], (int)xy[Axis.Y], color);
                }
            }

            return destinationImage;
        }

        public static Bitmap InverseMap(this Bitmap image, PointF[] points)
        {
            // Prepare source and destination images
            double sourceWidth = image.Width;
            double sourceHeight = image.Height;
            var destinationWidth = points.Max(point => (int)point.X);
            var destinationHeight = points.Max(point => (int)point.Y);
            var destinationImage = new Bitmap(destinationWidth, destinationHeight);
            UnsafeImageWrapper unsafeDestinationImage = new UnsafeImageWrapper(destinationImage);
            UnsafeImageWrapper unsafeSourceImage = new UnsafeImageWrapper(image);
            unsafeDestinationImage.LockImage();
            unsafeSourceImage.LockImage();

            // Destination image metrics
            var destinationTopLeft = points[0].ToVector();
            var destinationTopRight = points[1].ToVector();
            var destinationBottomRight = points[2].ToVector();
            var destinationBottomLeft = points[3].ToVector();
            var destinationTopSlope = (destinationTopRight - destinationTopLeft).Slope2D();
            var destinationLeftSlope = (destinationBottomLeft - destinationTopLeft).Slope2D();
            var destinationBottomSlope = (destinationBottomLeft - destinationBottomRight).Slope2D();
            var destinationRightSlope = (destinationTopRight - destinationBottomRight).Slope2D();
            var destinationTopLine = new Line(destinationTopLeft, destinationTopRight);
            var destinationRightLine = new Line(destinationTopRight, destinationBottomRight);
            var destinationBottomLine = new Line(destinationBottomRight, destinationBottomLeft);
            var destinationLeftLine = new Line(destinationBottomLeft, destinationTopLeft);

            //Get corner intersections
            var horizontalIntersection = Intersection(destinationTopLine, destinationBottomLine);
            var verticalIntersection = Intersection(destinationLeftLine, destinationRightLine);

            var interpolation = Settings.TextureInterpolation;
            int middleX = (int)(interpolation / 2.0);

            //Array of surronding pixels used for interpolation
            double[,,] sourceInterpolationArea = new double[interpolation, interpolation, 4];

            for (int y = 0; y < destinationHeight; y++)
            {
                for (int x = 0; x < destinationWidth; x++)
                {
                    var destinationPoint = new Vector(x, y);

                    var mPO = (horizontalIntersection != null) ? (destinationPoint - horizontalIntersection).Slope2D() : destinationTopSlope;
                    var mPN = (verticalIntersection != null) ? (destinationPoint - verticalIntersection).Slope2D() : destinationRightSlope;

                    //Get intersections
                    var L = Intersection(Line.Create2D(destinationPoint, mPO), destinationLeftLine) ?? destinationTopLeft;

                    var M = Intersection(Line.Create2D(destinationPoint, mPO), destinationRightLine) ?? destinationBottomRight;

                    var J = Intersection(Line.Create2D(destinationPoint, mPN), destinationTopLine) ?? destinationTopRight;

                    var K = Intersection(Line.Create2D(destinationPoint, mPN), destinationBottomLine) ?? destinationBottomLeft;

                    var dJP = (destinationPoint - J).Length();
                    var dLP = (destinationPoint - L).Length();

                    var dJK = (K - J).Length();
                    var dLM = (M - L).Length();


                    //set direction
                    if (dLM < (destinationPoint - M).Length()) dLP = -dLP;
                    if (dJK < (destinationPoint - K).Length()) dJP = -dJP;

                    ////interpolation

                    //find the pixels which surround the point
                    double yP0 = sourceHeight * dJP / dJK;
                    double xP0 = sourceWidth * dLP / dLM;

                    //top left coordinates of surrounding pixels
                    if (xP0 < 0) xP0--;
                    if (yP0 < 0) yP0--;

                    var left = (int)xP0;
                    var top = (int)yP0;

                    if ((left < -1 || left > sourceWidth) && (top < -1 || top > sourceHeight))
                    {
                        //if outside of source image just move on
                        continue;
                    }

                    //weights
                    var xFrac = xP0 - left;
                    var xFracRec = 1.0 - xFrac;
                    var yFrac = yP0 - top;
                    var yFracRec = 1.0 - yFrac;

                    //get source pixel colors, or white if out of range (to interpolate into the background color)
                    int x0;
                    int y0;
                    Color c;

                    for (int sx = 0; sx < interpolation; sx++)
                    {
                        for (int sy = 0; sy < interpolation; sy++)
                        {
                            x0 = left + sx;
                            y0 = top + sy;

                            if (x0 > 0 && y0 > 0 &&
                                x0 < sourceWidth && y0 < sourceHeight)
                            {
                                c = unsafeSourceImage.GetPixel(x0, y0);

                                sourceInterpolationArea[sx, sy, 0] = c.R;
                                sourceInterpolationArea[sx, sy, 1] = c.G;
                                sourceInterpolationArea[sx, sy, 2] = c.B;
                                sourceInterpolationArea[sx, sy, 3] = 255.0f;
                            }
                            else
                            {
                                // set full transparency in this case
                                sourceInterpolationArea[sx, sy, 0] = 0;
                                sourceInterpolationArea[sx, sy, 1] = 0;
                                sourceInterpolationArea[sx, sy, 2] = 0;
                                sourceInterpolationArea[sx, sy, 3] = 0;
                            }
                        }
                    }

                    //interpolate on x
                    for (int sy = 0; sy < interpolation; sy++)
                    {
                        //check transparency
                        if (sourceInterpolationArea[middleX, sy, 3] != 0 && sourceInterpolationArea[0, sy, 3] == 0)
                        {
                            //copy colors from 1, sy
                            sourceInterpolationArea[0, sy, 0] = sourceInterpolationArea[1, sy, 0];
                            sourceInterpolationArea[0, sy, 1] = sourceInterpolationArea[1, sy, 1];
                            sourceInterpolationArea[0, sy, 2] = sourceInterpolationArea[1, sy, 2];
                            sourceInterpolationArea[0, sy, 3] = sourceInterpolationArea[1, sy, 3];
                        }
                        else
                        {
                            //compute colors by interpolation
                            sourceInterpolationArea[0, sy, 0] = sourceInterpolationArea[0, sy, 0] * xFracRec + sourceInterpolationArea[middleX, sy, 0] * xFrac;
                            sourceInterpolationArea[0, sy, 1] = sourceInterpolationArea[0, sy, 1] * xFracRec + sourceInterpolationArea[middleX, sy, 1] * xFrac;
                            sourceInterpolationArea[0, sy, 2] = sourceInterpolationArea[0, sy, 2] * xFracRec + sourceInterpolationArea[middleX, sy, 2] * xFrac;
                            sourceInterpolationArea[0, sy, 3] = sourceInterpolationArea[0, sy, 3] * xFracRec + sourceInterpolationArea[middleX, sy, 3] * xFrac;
                        }

                        //interpolate transparency
                        sourceInterpolationArea[0, sy, 3] = sourceInterpolationArea[0, sy, 3] * xFracRec + sourceInterpolationArea[middleX, sy, 3] * xFrac;
                    }

                    //now interpolate on y

                    //check transparency
                    if (sourceInterpolationArea[0, middleX, 3] != 0 && sourceInterpolationArea[0, 0, 3] == 0)
                    {
                        //copy colors from 0, 1
                        sourceInterpolationArea[0, 0, 0] = sourceInterpolationArea[0, middleX, 0];
                        sourceInterpolationArea[0, 0, 1] = sourceInterpolationArea[0, middleX, 1];
                        sourceInterpolationArea[0, 0, 2] = sourceInterpolationArea[0, middleX, 2];
                        sourceInterpolationArea[0, 0, 3] = sourceInterpolationArea[0, middleX, 3];
                    }
                    else
                    {
                        sourceInterpolationArea[0, 0, 0] = sourceInterpolationArea[0, 0, 0] * yFracRec + sourceInterpolationArea[0, middleX, 0] * yFrac;
                        sourceInterpolationArea[0, 0, 1] = sourceInterpolationArea[0, 0, 1] * yFracRec + sourceInterpolationArea[0, middleX, 1] * yFrac;
                        sourceInterpolationArea[0, 0, 2] = sourceInterpolationArea[0, 0, 2] * yFracRec + sourceInterpolationArea[0, middleX, 2] * yFrac;
                        sourceInterpolationArea[0, 0, 3] = sourceInterpolationArea[0, 0, 3] * yFracRec + sourceInterpolationArea[0, middleX, 3] * yFrac;
                    }

                    //interpolate transparency
                    sourceInterpolationArea[0, 0, 3] = sourceInterpolationArea[0, 0, 3] * yFracRec + sourceInterpolationArea[0, middleX, 3] * yFrac;

                    //store to bitmap
                    if (sourceInterpolationArea[0, 0, 3] != 0) //pixel has color
                        unsafeDestinationImage.SetPixel(x, y, 
                            Color.FromArgb(
                                (int)CommonUtils.Limit(sourceInterpolationArea[0, 0, 3], byte.MinValue, byte.MaxValue), 
                                (int)CommonUtils.Limit(sourceInterpolationArea[0, 0, 0], byte.MinValue, byte.MaxValue),
                                (int)CommonUtils.Limit(sourceInterpolationArea[0, 0, 1], byte.MinValue, byte.MaxValue),
                                (int)CommonUtils.Limit(sourceInterpolationArea[0, 0, 2], byte.MinValue, byte.MaxValue)
                                )
                            );
                }
            }

            unsafeSourceImage.UnlockImage();
            unsafeDestinationImage.UnlockImage();

            return destinationImage;
        }

        private static Vector Intersection(Line line1, Line line2)
        {
            var line1Slope = line1.Slope2D();
            var line2Slope = line2.Slope2D();

            if (line1.Origin[Axis.X] == line2.Origin[Axis.X] && line1.Origin[Axis.Y] == line2.Origin[Axis.Y])
                return line1.Origin;

            if (line1Slope == line2Slope)
                return null; 

            float x = float.Epsilon;
            float y = float.Epsilon;

            if (float.IsInfinity(line1Slope))
            {
                x = line1.Origin[Axis.X];
                y = line2.Origin[Axis.Y] + line2Slope * (-line2.Origin[Axis.X] + line1.Origin[Axis.X]);
            }

            if (float.IsInfinity(line2Slope))
            {
                x = line2.Origin[Axis.X];
                y = line1.Origin[Axis.Y] + line1Slope * (-line1.Origin[Axis.X] + line2.Origin[Axis.X]);
            }

            if (x == float.Epsilon)
            {
                float q1 = line1.Origin[Axis.Y] - line1Slope * line1.Origin[Axis.X];
                float q2 = line2.Origin[Axis.Y] - line2Slope * line2.Origin[Axis.X];
                x = (q1 - q2) / (line2Slope - line1Slope);
                y = line1Slope * x + q1;
            }

            if (float.IsInfinity(x) || float.IsInfinity(y))
                return null;
            else
            {
                return new Vector(x, y);
            }
        }
    }
}
