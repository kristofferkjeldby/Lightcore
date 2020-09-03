namespace Lightcore.UI.Textures
{
    using Lightcore.Common.Models;
    using Lightcore.Processors.Models;
    using Lightcore.Textures.Models;
    using System;

    public class TextureProcessor : Processor
    {
        public override ProcessorMetadata Metadata => new ProcessorMetadata("Texture processor", false, EntityType.Preview, EntityType.World);

        //public override Polygon PolygonProcessor(Polygon polygon, ProcessorArgs args)
        //{
        //    if (polygon.Texture is ImageTexture imageTexture)
        //    {
        //        imageTexture.ImageTextureType.MaxLength =
        //        Math.Max(
        //            imageTexture.ImageTextureType.MaxLength,
        //            polygon.LongAxis.Length
        //        );
        //    }

        //    return polygon;
        //}
    }
}

