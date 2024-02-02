using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

public partial class Product
{
    [Key]
    public int ArticleNumber { get; set; }

    [StringLength(250)]
    public string Title { get; set; } = null!;

    public int ManufacturerId { get; set; }

    public int CategoryId { get; set; }

    [StringLength(200)]
    public string? Preamble { get; set; }

    public string? Description { get; set; }

    public string? Specification { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("ManufacturerId")]
    [InverseProperty("Products")]
    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
