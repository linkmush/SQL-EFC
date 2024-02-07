﻿using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, ICustomerInfoRepository customerInfoRepository, ICustomerAddressRepository customerAddressRepository, IAddressRepository addressRepository) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ICustomerInfoRepository _customerInfoRepository = customerInfoRepository;
    private readonly ICustomerAddressRepository _customerAddressRepository = customerAddressRepository;
    private readonly IAddressRepository _addressRepository = addressRepository;

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationDto customer)
    {
        try
        {
            if (!await _customerRepository.ExistsAsync(x => x.Email == customer.Email))
            {
                var customerEntity = await _customerRepository.GetOneAsync(x => x.Email == customer.Email);
                if (customerEntity == null)
                {

                    customerEntity = new CustomerEntity
                    {
                        Email = customer.Email,
                        CustomerInfo = new CustomerInfoEntity
                        {
                            FirstName = customer.FirstName,
                            LastName = customer.LastName
                        },
                    };

                    customerEntity.CustomerAddress.Add(new CustomerAddressEntity
                    {
                        Address = new AddressEntity
                        {
                            StreetName = customer.StreetName,
                            PostalCode = customer.PostalCode,
                            City = customer.City,
                        }
                    });

                    customerEntity = await _customerRepository.CreateAsync(customerEntity);


                    var orderEntity = await _orderRepository.CreateAsync(new OrderEntity
                    {
                        CustomerId = customerEntity.Id,
                    });

                    if (orderEntity != null)
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
                    Id = customerEntity.Id,
                    Email = customerEntity.Email,
                    FirstName = customerEntity.CustomerInfo.FirstName,
                    LastName = customerEntity.CustomerInfo.LastName,
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
            var updateCustomer = await _customerRepository.GetOneAsync(x => x.Id == customer.Id);
            if (updateCustomer != null)
            {
                if (updateCustomer.Email != customer.Email)
                {
                    if (!await _customerRepository.ExistsAsync(x => x.Email == customer.Email))
                        updateCustomer.Email = customer.Email;
                }

                updateCustomer.CustomerInfo.FirstName = customer.FirstName;
                updateCustomer.CustomerInfo.LastName = customer.LastName;

                var existingAddresses = updateCustomer.CustomerAddress.Select(ca => ca.Address).ToList();

                foreach (var existingAddress in existingAddresses)
                {
                    if (!customer.Addresses.Any(a => a.Id == existingAddress.Id))
                    {
                        await _addressRepository.DeleteAsync(x => x.Id == existingAddress.Id);
                    }
                }

                foreach (var addressDto in customer.Addresses)
                {
                    var addressEntity = updateCustomer.CustomerAddress.FirstOrDefault(ca => ca.AddressId == addressDto.Id)?.Address;
                    if (addressEntity != null)
                    {
                        addressEntity.StreetName = addressDto.StreetName;
                        addressEntity.PostalCode = addressDto.PostalCode;
                        addressEntity.City = addressDto.City;
                    }
                    else
                    {
                        var newAddress = new AddressEntity
                        {
                            StreetName = addressDto.StreetName,
                            PostalCode = addressDto.PostalCode,
                            City = addressDto.City
                        };
                        updateCustomer.CustomerAddress.Add(new CustomerAddressEntity { Address = newAddress });
                    }
                }

                var updatedCustomerEntity = await _customerRepository.UpdateAsync(x => x.Id == updateCustomer.Id, updateCustomer);

                return customer;

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

                await _customerInfoRepository.DeleteAsync(x => x.CustomerId == customerEntity.Id);
                await _orderRepository.DeleteAsync(x => x.CustomerId == customerEntity.Id);
                await _customerRepository.DeleteAsync(x => x.Email == customer.Email);

                foreach (var addressDto in customerEntity.CustomerAddress)
                {
                    await _addressRepository.DeleteAsync(x => x.Id == addressDto.AddressId);
                }

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