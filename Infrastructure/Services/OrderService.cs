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
                var customerinfoEntity = _customerInfoRepository.GetOne(x => x.FirstName == customer.FirstName);
                customerinfoEntity ??= _customerInfoRepository.Create(new CustomerInfoEntity { FirstName = customer.FirstName });

                var customerEntity = new CustomerEntity
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    StreetName = customer.StreetName,
                    PostalCode = customer.PostalCode,
                    City = customer.City
                };

                var result = _customerRepository.Create(customerEntity);
                if (result == null)
                    return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }
}
