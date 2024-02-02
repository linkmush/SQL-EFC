using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

[Index("Manufacture", Name = "UQ__Manufact__C624340F6FE259DC", IsUnique = true)]
public partial class Manufacturer
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Manufacture { get; set; } = null!;

    [InverseProperty("Manufacturer")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
