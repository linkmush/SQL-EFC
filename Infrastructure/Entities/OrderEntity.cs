using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Dtos;

namespace Infrastructure.Entities;

public class OrderEntity
{
    [Key]     // talar om att det är en unik nyckel
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Customer))]     // implementerar Foreign Key
    public int? CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;

    public static implicit operator OrderEntity(CustomerDto customer)
    {
        var orderEntity = new OrderEntity
        {
            CustomerId = customer.CustomerId
        };

        return orderEntity;
    }
}
