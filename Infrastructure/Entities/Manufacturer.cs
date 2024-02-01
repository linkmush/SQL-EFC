using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Manufacture { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
