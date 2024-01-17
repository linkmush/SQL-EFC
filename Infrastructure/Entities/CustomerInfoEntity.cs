using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CustomerInfoEntity
{
    [Key]
    [ForeignKey(nameof(Customer))]
    public int? CustomerId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string LastName { get; set; } = null!;

    public virtual CustomerEntity Customer { get; set; } = null!;
}