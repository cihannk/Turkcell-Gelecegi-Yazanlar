using MarketApp.Dtos.Models;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Abstract
{
    public interface IUserService
    {
        Task<User> Login(UserLoginModel model);
        Task<User> Register(UserRegisterModel model);
    }
}
