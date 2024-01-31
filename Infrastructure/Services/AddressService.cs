using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class AddressService(AddressRepository addressRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;

    public async Task<bool> CreateCustomerAsync(AddressDto address)
    {
        try
        {
            if (!await _addressRepository.ExistsAsync(x => x.Id == address.Id))
            {
                var customerEntity = await _addressRepository.GetOneAsync(x => x.Id == address.Id);
                if (customerEntity == null)
                {

                    var addressEntity = await _addressRepository.CreateAsync(new AddressEntity
                    {
                        StreetName = address.StreetName,
                        PostalCode = address.PostalCode,
                        City = address.City,
                    });

                    if (addressEntity != null)
                        return true;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }

    public async Task<AddressDto> GetOneAsync(Expression<Func<AddressEntity, bool>> predicate)
    {
        try
        {
            var addressEntity = await _addressRepository.GetOneAsync(predicate);

            if (addressEntity != null)
            {

                var addressDto = new AddressDto
                {
                    StreetName = addressEntity.StreetName,
                    PostalCode = addressEntity.PostalCode,
                    City = addressEntity.City
                };

                if (addressEntity != null)
                {
                    return addressDto;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<AddressDto>> GetAllAsync()
    {
        var addresses = new List<AddressDto>();

        try
        {
            var addressEntities = await _addressRepository.GetAllAsync();

            foreach (var addressEntity in addressEntities)
            {
                var addressDto = new AddressDto
                {
                    StreetName = addressEntity.StreetName,
                    PostalCode = addressEntity.PostalCode,
                    City = addressEntity.City
                };

                addresses.Add(addressDto);
            }

            return addresses;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<AddressDto> UpdateAsync(AddressDto address)
    {
        try
        {
            if (!await _addressRepository.ExistsAsync(x => x.Id == address.Id))
            {
                var updateAddress = await _addressRepository.GetOneAsync(x => x.Id == address.Id);

                if (updateAddress != null)
                {
                    updateAddress.StreetName = address.StreetName;
                    updateAddress.PostalCode = address.PostalCode;
                    updateAddress.City = address.City;

                    await _addressRepository.UpdateAsync(x => x.Id == address.Id, updateAddress);

                    if (updateAddress != null)
                    {
                        var updatedAddressDto = new AddressDto
                        {
                            StreetName = updateAddress.StreetName,
                            PostalCode = updateAddress.PostalCode,
                            City = updateAddress.City
                        };

                        return updatedAddressDto;
                    }
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteAsync(AddressDto address)
    {
        try
        {
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return false;
    }
}
