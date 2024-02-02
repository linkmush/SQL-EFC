using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;

public partial class Currency
{
    [Key]
    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column("Currency")]
    [StringLength(20)]
    public string Currency1 { get; set; } = null!;
}
