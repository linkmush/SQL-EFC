using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class OrderEntity
{
    [Key]   
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Customer))]   
    public int? CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;
}
