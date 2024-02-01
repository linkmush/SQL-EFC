using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Categoryname { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
