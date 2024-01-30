namespace Infrastructure.Services;

public class CustomerService
{
    private readonly OrderService _orderService;

    private readonly AddressService _addressService;

    public CustomerService(OrderService orderService, AddressService addressService)
    {
        _orderService = orderService;
        _addressService = addressService;
    }
}
