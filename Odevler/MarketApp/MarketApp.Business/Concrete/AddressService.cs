using AutoMapper;
using MarketApp.Business.Abstract;
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
            var entity = _mapper.Map<Address>(address);
            return await _addressRepository.Add(entity);
        }

        public async Task DeleteAddress(int addressId)
        {
            await _addressRepository.Delete(addressId);
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            if (await _addressRepository.IsExist(addressId)){
                var address = await _addressRepository.GetEntityById(addressId);
                return address;
            }
              throw new InvalidOperationException("Address is not exist with given id");
        }

        public async Task<IList<Address>> GetAllAddresses()
        {
            var addresses = await _addressRepository.GetAllEntities();
            if (addresses.Count> 0)
                return addresses;
            throw new InvalidOperationException("There is no address in db");
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
                throw new InvalidOperationException("There is no address with given userId");
            }
            else
            {
                throw new InvalidOperationException("There is no user with given userId");
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
                throw new InvalidOperationException("There is no address with given username");
            }
            throw new InvalidOperationException("There is no user with given username");
        }

        public async Task<int> UpdateAddress(UpdateAddressRequest address)
        {
            var entity = _mapper.Map<Address>(address);
            return await _addressRepository.Update(entity);
        }
    }
}
