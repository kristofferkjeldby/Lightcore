﻿namespace Lightcore.Textures.Models
{
    using Lightcore.Common.Models;
    using Lightcore.Textures.Extensions;
    using System.Drawing;

    public class ImageTexture : ColorTexture, IImageTexture
    {
        public Bitmap Image { get; set; }

        public RectangleF? Crop { get; set; }

        public ImageTexture(Vector color, Bitmap image, RectangleF? crop, float reflectance, float transparency, float metallicity, float shiny, Vector processedColor = null) : base(color, reflectance, transparency, metallicity, shiny, processedColor)
        {
            Image = image;
            Crop = crop;
        }

        public override Brush GetBrush(Polygon polygon, ref PointF[] points)
        {
            if (points.Length != 4)
                return new TextureBrush(Image);

            // A common problem is the joint between texture. To avoid black lines, all 
            // polygons are expanded to apply overscan
            points = points.ApplyOverscan(Settings.TextureOverscan);

            var croppedImage = (Crop.HasValue) ? Image.Crop(Crop.Value) : Image;

            var colorizedImage = croppedImage.Colorize(ProcessedColor, Transparency);

            var mappedImage = colorizedImage.ForwardMap(points);

            var brush = new TextureBrush(mappedImage);

            return brush;
        }

        public override Texture Clone()
        {
            return new ImageTexture(Color, Image, Crop, Reflectance, Transparency, Metallicity, Shiny, ProcessedColor);
        }
    }
}
