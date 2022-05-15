using AutoMapper;
using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.ErrorMessages;
using MarketApp.DataAccess.Repositories;
using MarketApp.Dtos.Request;
using MarketApp.Dtos.Response;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper, IUserRepository userRepository)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAddress(AddAddressRequest address)
        {
            if (!await isDesiredNameExistInUserAddresses(address.UserId, address.DesiredName))
            {
                var entity = _mapper.Map<Address>(address);
                return await _addressRepository.Add(entity);
            }
            throw new InvalidOperationException(ErrorMessages.Address.SameAddressExistWithGivenHeader);
        }

        public async Task<bool> CheckIfUserHasAddress(int userId, int addressId)
        {
            var addressEntity= await _addressRepository.GetEntityById(addressId);
            if (addressEntity.UserId == userId)
            {
                return true;
            }
            return false;
        }

        public async Task DeleteAddress(int addressId)
        {
            if (await _addressRepository.IsExist(addressId))
            {
                await _addressRepository.Delete(addressId);
            }
            else
            {
                throw new InvalidOperationException(ErrorMessages.Address.NotFoundWithGivenAddressId);
            }
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            if (await _addressRepository.IsExist(addressId)){
                var address = await _addressRepository.GetEntityById(addressId);
                return address;
            }
              throw new InvalidOperationException(ErrorMessages.Address.NotFoundWithGivenAddressId);
        }

        public async Task<IList<Address>> GetAllAddresses()
        {
            var addresses = await _addressRepository.GetAllEntities();
            if (addresses.Count> 0)
                return addresses;
            throw new InvalidOperationException(ErrorMessages.Address.NoAddress);
        }

        public async Task<IList<GetAddressResponse>> GetUserAddressesWithUserId(int userId)
        {
            if(await _userRepository.IsExist(userId))
            {
                var addressEntities = await _addressRepository.GetAllEntitiesByUserId(userId);
                if (addressEntities != null)
                {
                    return _mapper.Map<List<GetAddressResponse>>(addressEntities);
                }
                throw new InvalidOperationException(ErrorMessages.Address.NotFoundWithGivenUserId);
            }
            else
            {
                throw new InvalidOperationException(ErrorMessages.User.NotFoundWithGivenUserId);
            }
        }

        public async Task<IList<GetAddressResponse>> GetUserAddressesWithUsername(string username)
        {
            var user = await _userRepository.GetEntityByUsername(username);
            if (user != null)
            {
                var addresses = await _addressRepository.GetAllEntitiesByUserId(user.Id);
                if (addresses != null)
                {
                    return _mapper.Map<List<GetAddressResponse>>(addresses);
                }
                throw new InvalidOperationException(ErrorMessages.Address.NotFoundWithGivenUserId);
            }
            throw new InvalidOperationException(ErrorMessages.User.NotFoundWithGivenUserId);
        }

        public async Task<int> UpdateAddress(UpdateAddressRequest address)
        {

            if (! await isDesiredNameExistInUserAddresses(address.UserId, address.DesiredName))
            {
                var entity = _mapper.Map<Address>(address);
                return await _addressRepository.Update(entity);
            }
            throw new InvalidOperationException(ErrorMessages.Address.SameAddressExistWithGivenHeader);
        }
        private async Task<bool> isDesiredNameExistInUserAddresses(int userId, string desiredName)
        {
            var addresses = await _addressRepository.GetAllEntitiesByUserId(userId);
            if(addresses.Any(x => x.DesiredName == desiredName))
            {
                return true;
            }
            return false;
        }
    }
}
