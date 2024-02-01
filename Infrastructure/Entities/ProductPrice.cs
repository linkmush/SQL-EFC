using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class ProductPrice
{
    public int ArticleNumber { get; set; }

    public decimal Price { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public virtual Product ArticleNumberNavigation { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;
}
