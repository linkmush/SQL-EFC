using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    [Required]     // Required betyder att jag måste fylla i fältet.
    [Column(TypeName = "nvarchar(100)")] // talar om vilken datatyp och längd
    public string Email { get; set; } = null!;

    public virtual ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>(); // en till många relation
    public virtual ICollection<CustomerAddressEntity> CustomerAdress { get; set; } = new HashSet<CustomerAddressEntity>();
}