using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Product
{
    public int ArticleNumber { get; set; }

    public string Title { get; set; } = null!;

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    public string? Preamble { get; set; }

    public string? Description { get; set; }

    public string? Specification { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
