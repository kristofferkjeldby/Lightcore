namespace Lightcore.Common.Models
{
    using System;

    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
