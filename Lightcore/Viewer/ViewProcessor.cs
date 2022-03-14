namespace Lightcore.View
{
    using Lightcore.Common.Models;
    using Lightcore.View.Models;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Matrix = Common.Models.Matrix;
    using Lightcore.Processors.Models;
    using Lightcore.Common.Cartesian.Extensions;
    using Lightcore.Textures.Models;

    public class ViewProcessor : Processor
    {
        PictureBox pictureBox;
        readonly float maxX;
        float maxY;
        Vector transpose;
        Matrix scale;

        public override ProcessorMetadata Metadata => new ProcessorMetadata("View processor", false, EntityType.Debug, EntityType.World);

        public ViewProcessor(PictureBox pictureBox, float maxX)
        {
            this.pictureBox = pictureBox;
            this.maxX = maxX;
            Clear();
            Resize();
        }

        public void Clear()
        {
            using (Drawer d = new Drawer(pictureBox))
            {

            }
        }

        public void Resize()
        {
            this.maxY = maxX * pictureBox.Height / pictureBox.Width;
            transpose = new Vector(pictureBox.Width / 2, pictureBox.Height / 2, 0);
            scale = new Matrix(new Vector(pictureBox.Width / (2 * maxX), 0, 0), new Vector(0, -pictureBox.Height / (2 * maxY), 0), new Vector(0, 0, 1));
        }

        private PointF Transform(Vector vector)
        {
            var v = (scale * vector) + transpose;
            return new PointF((float)v[0], (float)v[1]);
        }

        private bool IsInMargin(PointF point, int margin)
        {
            return (
                point.X < pictureBox.Width + margin &&
                point.Y < pictureBox.Height + margin &&
                point.X > -margin &&
                point.Y > -margin
                );
        }

        public override void PostProcessor(RenderArgs args)
        {
            Resize();

            using (Drawer d = new Drawer(pictureBox, args.RenderMetadata.Filename))
            {
                var polygons = args.World.Entities.
                    Where(entity => EntityPredicate(entity, args)).
                    SelectMany(entity => entity.Elements).
                    OrderByDescending(polygon => polygon.Distance()).ThenByDescending(polygon => polygon.Midpoint().Length()).
                    ToArray();

                var texturePolygonsCount = polygons.Where(polygon => polygon.Texture is IImageTexture).Count();
                var k = 1;

                for (int i = 0; i < polygons.Count(); i++)
                { 
                    {
                        var points = polygons[i].Elements.Select(v => Transform(v)).Where(point => IsInMargin(point, Settings.ViewMargin))?.ToArray();

                        switch (points.Length)
                        {
                            case 2:
                                d.Graphics.DrawLine(new Pen(polygons[i].Texture.GetBrush(polygons[i], ref points)), points[0], points[1]);
                                break;
                            case 3:
                            case 4:
                                if (polygons[i].Texture is IImageTexture)
                                    args.Status($"{Metadata.Name}: Applying texture to polygon {k++} of {texturePolygonsCount} ...");
                                d.Graphics.FillPolygon(polygons[i].Texture.GetBrush(polygons[i], ref points), points);
                                break;
                        }
                    }
                }
            }
        }
    }
}
