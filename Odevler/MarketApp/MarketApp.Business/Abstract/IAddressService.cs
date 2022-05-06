using MarketApp.Dtos.Request;
using MarketApp.Dtos.Response;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Abstract
{
    public interface IAddressService
    {
        Task<int> AddAddress(AddAddressRequest address);
        Task<IList<GetAddressResponse>> GetUserAddressesWithUserId(int userId);
        Task DeleteAddress(int addressId);
    }
}
