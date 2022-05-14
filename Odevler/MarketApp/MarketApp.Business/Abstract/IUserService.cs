using MarketApp.Dtos.Models;
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
    public interface IUserService
    {
        Task<User> Login(UserLoginModel model);
        Task<User> Register(UserRegisterModel model);
        Task<IList<GetUsersResponse>> GetAllUsers();
        Task<GetUserResponse> GetUser(int userId);
        Task<GetUserResponse> GetUserByUsername(string username);
        Task UpdateUser(GetUserResponse user);
        Task UpdateUser(UpdateUserRequest user);
        Task ChangePassword(string email, string newPassword);
        Task DeleteUser(int id);
    }
}
