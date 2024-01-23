using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class OrderService(OrderRepository orderRepository, CustomerRepository customerRepository, CustomerInfoRepository customerInfoRepository, CustomerAddressRepository customerAddressRepository, AddressRepository addressRepository)
{
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly CustomerInfoRepository _customerInfoRepository = customerInfoRepository;
    private readonly CustomerAddressRepository _customerAddressRepository = customerAddressRepository;
    private readonly AddressRepository _addressRepository = addressRepository;

    public bool CreateCustomer(CreateCustomerDto customer)
    {
        try
        {
            if (!_customerRepository.Exists(x => x.Email == customer.Email))
            {
                var customerEntity = _customerRepository.GetOne(x => x.Email == customer.Email);
                customerEntity ??= _customerRepository.Create(new CustomerEntity { Email = customer.Email });

                var addressEntity = new AddressEntity
                {
                    StreetName = customer.StreetName,
                    PostalCode = customer.PostalCode,
                    City = customer.City,
                };
                var addressResult = _addressRepository.Create(addressEntity);

                var customerAddressEntity = new CustomerAddressEntity
                {
                    CustomerId = customer.CustomerId,
                    AddressId = customer.AddressId
                };
                var customerAddressResult = _customerAddressRepository.Create(customerAddressEntity);

                var customerInfoEntity = new CustomerInfoEntity
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                };
                var customerInfoResult = _customerInfoRepository.Create(customerInfoEntity);

                var orderEntity = new OrderEntity
                {
                    CustomerId= customer.CustomerId,
                };
                var orderResult = _orderRepository.Create(orderEntity);



                if (customerInfoResult != null && addressResult != null && customerAddressResult != null && orderResult != null)
                    return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }

    public IEnumerable<CreateCustomerDto> GetAllCustomers()
    {
        var customers = new List<CreateCustomerDto>();

        try
        {
            var result = _customerRepository.GetAll();

            foreach (var item in result)
            {
                customers.Add(new CreateCustomerDto
                {
                    Email = item.Email,
                });
            }

            return customers;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}
