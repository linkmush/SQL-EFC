using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Dtos;

namespace Infrastructure.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    [Required] 
    [Column(TypeName = "nvarchar(100)")]
    public string Email { get; set; } = null!;

    public virtual CustomerInfoEntity CustomerInfo { get; set; } = null!;
    public virtual ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>();
    public virtual ICollection<CustomerAddressEntity> CustomerAddress { get; set; } = new List<CustomerAddressEntity>();

    public static implicit operator CustomerEntity(CustomerDto customer)
    {
        var customerEntity = new CustomerEntity
        {
            Email = customer.Email
        };

        return customerEntity;
    }
}