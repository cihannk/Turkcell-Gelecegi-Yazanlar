using AutoMapper;
using MarketApp.Business.Abstract;
using MarketApp.DataAccess.Repositories;
using MarketApp.Dtos.Models;
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
                throw new InvalidOperationException("An account is already using this email");
            }

            byte[] salt = _hashingService.ProduceSalt(128);
            byte[] hashedPassword = _hashingService.Hash(model.Password, salt);

            var userEntity = _mapper.Map<User>(model);
            userEntity.Salt = salt;
            userEntity.Password = hashedPassword;
            userEntity.RoleId = 3;

            await _userRepository.Add(userEntity);
            // For getting role
            return await _userRepository.GetEntityById(userEntity.Id);
        }
    }
}
