using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class OrderService(OrderRepository orderRepository, CustomerRepository customerRepository, CustomerInfoRepository customerInfoRepository, CustomerAddressRepository customerAddressRepository, AddressRepository addressRepository)
{
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly CustomerInfoRepository _customerInfoRepository = customerInfoRepository;
    private readonly CustomerAddressRepository _customerAddressRepository = customerAddressRepository;
    private readonly AddressRepository _addressRepository = addressRepository;

    public async Task<bool> CreateCustomerAsync(CustomerDto customer, AddressDto address)
    {
        try
        {
            if (!await _customerRepository.ExistsAsync(x => x.Email == customer.Email))
            {
                var customerEntity = await _customerRepository.GetOneAsync(x => x.Email == customer.Email);
                if (customerEntity == null)
                {
                    customerEntity = await _customerRepository.CreateAsync(new CustomerEntity { Email = customer.Email });

                    var addressEntity = await _addressRepository.CreateAsync(new AddressEntity
                    {
                        StreetName = address.StreetName,
                        PostalCode = address.PostalCode,
                        City = address.City,
                    });

                    var customerAddressEntity = await _customerAddressRepository.CreateAsync(new CustomerAddressEntity
                    {
                        CustomerId = customerEntity.Id,
                        AddressId = addressEntity.Id
                    });

                    var customerInfoEntity = await _customerInfoRepository.CreateAsync(new CustomerInfoEntity
                    {
                        CustomerId = customerEntity.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                    });

                    var orderEntity = await _orderRepository.CreateAsync(new OrderEntity
                    {
                        CustomerId = customerEntity.Id,
                    });

                    if (addressEntity != null && customerAddressEntity != null && customerInfoEntity != null && orderEntity != null)
                        return true;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }
    public async Task<CustomerDto> GetOneAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        try
        {
            var customerEntity = await _customerRepository.GetOneAsync(predicate);

            if (customerEntity != null)
            {

                var customerDto = new CustomerDto
                {
                    Email = customerEntity.Email,
                    FirstName = customerEntity.CustomerInfo.FirstName,
                    LastName = customerEntity.CustomerInfo.LastName,
                    CustomerId = customerEntity.Id,
                    AddressId = customerEntity.Id,
                };

                foreach (var customerRecord in customerEntity.CustomerAddress)
                {
                    var addressDto = new AddressDto
                    {
                        StreetName = customerRecord.Address.StreetName,
                        PostalCode = customerRecord.Address.PostalCode,
                        City = customerRecord.Address.City,
                    };

                    customerDto.Addresses.Add(addressDto);
                }

                return customerDto;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customers = new List<CustomerDto>();

        try
        {
            var customerEntities = await _customerRepository.GetAllAsync();

            foreach (var item in customerEntities)
            {
                var customerDto = new CustomerDto
                {
                    Email = item.Email,
                    FirstName = item.CustomerInfo.FirstName,
                    LastName = item.CustomerInfo.LastName,
                    CustomerId = item.CustomerInfo.CustomerId
                };

                if (customerDto != null)
                {
                    foreach (var customerAddress in item.CustomerAddress)
                    {
                        var addressEntity = await _addressRepository.GetOneAsync(x => x.Id == customerAddress.AddressId);
                        customerDto.Addresses.Add(new AddressDto
                        {
                            StreetName = addressEntity.StreetName,
                            PostalCode = addressEntity.PostalCode,
                            City = addressEntity.City
                        });

                        customers.Add(customerDto);
                    }
                }
            }

            return customers;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<CustomerDto> UpdateAsync(CustomerDto customer)
    {
        try
        {
            if (!await _customerRepository.ExistsAsync(x => x.Email == customer.Email))
            {
                var updateCustomer = await _customerRepository.GetOneAsync(x => x.Email == customer.Email);

                if (updateCustomer != null)
                {
                    updateCustomer.Email = customer.Email;
                    updateCustomer.CustomerInfo.CustomerId = customer.CustomerId;
                    updateCustomer.CustomerInfo.FirstName = customer.FirstName;
                    updateCustomer.CustomerInfo.LastName = customer.LastName;

                    await _customerRepository.UpdateAsync(x => x.Email == customer.Email, updateCustomer);

                    var updatedCustomerDto = new CustomerDto
                    {
                        Email = updateCustomer.Email,
                        FirstName = updateCustomer.CustomerInfo.FirstName,
                        LastName = updateCustomer.CustomerInfo.LastName,
                        CustomerId = updateCustomer.Id,
                    };

                    return updatedCustomerDto;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteAsync(CustomerDto customer)
    {
        try
        {
            var customerEntity = await _customerRepository.GetOneAsync(x => x.Email == customer.Email);

            if (customerEntity != null)
            {
                await _customerAddressRepository.DeleteAsync(x => x.CustomerId == customerEntity.Id);
                await _customerInfoRepository.DeleteAsync(x => x.CustomerId == customerEntity.Id);
                await _orderRepository.DeleteAsync(x => x.CustomerId == customerEntity.Id);
                await _customerRepository.DeleteAsync(x => x.Email == customer.Email);

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return false;
    }
}