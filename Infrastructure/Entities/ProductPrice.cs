using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

[Keyless]
public partial class ProductPrice
{
    public int ArticleNumber { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyCode { get; set; } = null!;

    [ForeignKey("ArticleNumber")]
    public virtual Product ArticleNumberNavigation { get; set; } = null!;

    [ForeignKey("CurrencyCode")]
    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;
}
