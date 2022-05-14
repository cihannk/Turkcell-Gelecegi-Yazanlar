using AutoMapper;
using MarketApp.Business.Abstract;
using MarketApp.DataAccess.Repositories;
using MarketApp.Dtos.Models;
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
    public class UserService : IUserService
    {
        private readonly IHashingService _hashingService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,IMapper mapper, IHashingService hashingService)
        {
            _hashingService = hashingService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task ChangePassword(string email, string newPassword)
        {
            var user = await _userRepository.GetEntityByEmail(email);
            if (user != null)
            {
                var salt = _hashingService.ProduceSalt(128);
                var hash = _hashingService.Hash(newPassword, salt);
                user.Password = hash;
                user.Salt = salt;
                await _userRepository.Update(user);
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı bulunamadı");
            }
        }

        public async Task DeleteUser(int id)
        {
            if (! await _userRepository.IsExist(id))
            {
                await _userRepository.Delete(id);
            }
            throw new InvalidOperationException("Silinmek istenen user dbde yok");
        }

        public async Task<IList<GetUsersResponse>> GetAllUsers()
        {
            var entities = await _userRepository.GetAllEntities();
            if (entities != null)
            {
                return _mapper.Map<List<GetUsersResponse>>(entities);
            }
            throw new InvalidOperationException("There is no user in db");
        }

        public async Task<GetUserResponse> GetUser(int userId)
        {
            var user = await _userRepository.GetEntityById(userId);
            if (user != null)
            {
                return _mapper.Map<GetUserResponse>(user);
            }
            throw new InvalidOperationException("User not exist with given userId");
        }

        public async Task<GetUserResponse> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetEntityByUsername(username);
            if (user != null)
            {
                return _mapper.Map<GetUserResponse>(user);
            }
            throw new InvalidOperationException("User not exist with given username");
        }

        public async Task<User> Login(UserLoginModel model)
        {
            if (!await _userRepository.IsEmailExist(model.Email))
            {
                throw new InvalidOperationException("Email is not exist");
            }
            var user = await _userRepository.GetEntityByEmail(model.Email);
            if (_hashingService.CompareHash(model.Password, user.Password, user.Salt))
            {
                return user;
            }
            throw new InvalidOperationException("Email or password is wrong");
        }

        public async Task<User> Register(UserRegisterModel model)
        {
            if (await _userRepository.IsEmailExist(model.Email))
            {
                throw new InvalidOperationException("Aynı email'i kullanan bir kullanıcı zaten var");
            }
            if (await _userRepository.IsUsernameExist(model.Username))
            {
                throw new InvalidOperationException("Aynı username'i kullanan bir kullanıcı zaten var");
            }

            byte[] salt = _hashingService.ProduceSalt(128);
            byte[] hashedPassword = _hashingService.Hash(model.Password, salt);

            var userEntity = _mapper.Map<User>(model);
            userEntity.Salt = salt;
            userEntity.Password = hashedPassword;
            userEntity.RoleId = 3;
            userEntity.RegisterDate = DateTime.Now;

            await _userRepository.Add(userEntity);
            // For getting role
            return await _userRepository.GetEntityById(userEntity.Id);
        }

        public async Task UpdateUser(GetUserResponse user)
        {
            if (! await _userRepository.IsExist(user.Id))
            {
                throw new InvalidOperationException("User doesn't exist");
            }
            var dbEntity = await _userRepository.GetEntityById(user.Id);
            var entity = _mapper.Map<User>(user);

            entity.Password = dbEntity.Password;
            entity.Salt = dbEntity.Salt;
            entity.RoleId = dbEntity.RoleId;
            entity.RegisterDate = dbEntity.RegisterDate;

            await _userRepository.Update(entity);
        }

        public async Task UpdateUser(UpdateUserRequest user)
        {
            var dbEntity = await _userRepository.GetEntityById(user.Id);
            if (await _userRepository.IsExist(user.Id))
            {
                // don't check user has same email in db if update request is same with updating entity

                if (user.Email != dbEntity.Email)
                {
                    if (await _userRepository.IsEmailExist(user.Email))
                    {
                        throw new InvalidOperationException("Bu emaile sahip başka bir kullanıcı var");
                    }
                }
                if (user.Username != dbEntity.Username)
                {
                    if (await _userRepository.IsUsernameExist(user.Username))
                    {
                        throw new InvalidOperationException("Bu kullanıcı adına sahip başka bir kullanıcı var");
                    }
                }             

                var salt = _hashingService.ProduceSalt(128);
                var hashedPassword = _hashingService.Hash(user.Password, salt);
                var userEntity = _mapper.Map<User>(user);

                userEntity.Password = hashedPassword;
                userEntity.Salt = salt;
                userEntity.RegisterDate = dbEntity.RegisterDate;

                await _userRepository.Update(userEntity);
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı veritabanında yok");
            }
         
        }
    }
}
