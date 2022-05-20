using AutoMapper;
using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.ErrorMessages;
using MarketApp.DataAccess.Repositories;
using MarketApp.DataAccess.Repositories.Abstract;
using MarketApp.Dtos.Request;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<int> AddRole(AddRoleRequest role)
        {
            if (! await _roleRepository.IsNameExist(role.Name))
            {
                var roleEntity = _mapper.Map<Role>(role);
                await _roleRepository.Add(roleEntity);
                return roleEntity.Id;
            }
            throw new InvalidOperationException(ErrorMessages.Role.AlreadyExistWithGivenRoleName);
        }

        public async Task DeleteRole(int id)
        {
            if (await _roleRepository.IsExist(id))
            {
                await _roleRepository.Delete(id);
            }
            else
            {
                throw new InvalidOperationException(ErrorMessages.Role.NotFoundWithGivenRoleId);
            }
        }

        public async Task<IList<Role>> GetAllRoles()
        {

            var roles = await _roleRepository.GetAllEntities();
            if (roles.Count > 0)
            {
                return roles;
            }
            throw new InvalidOperationException(ErrorMessages.Role.NoRole);
        }

        public async Task<Role> GetRoleById(int id)
        {
            if (await _roleRepository.IsExist(id))
            {
                return await _roleRepository.GetEntityById(id);
            }
            throw new InvalidOperationException(ErrorMessages.Role.NotFoundWithGivenRoleId);
        }

        public async Task UpdateRole(Role role)
        {
            if (await _roleRepository.IsExist(role.Id))
            {
                if (!await _roleRepository.IsNameExist(role.Name))
                {
                    await _roleRepository.Update(role);
                }
                else
                {
                    throw new InvalidOperationException(ErrorMessages.Role.AlreadyExistWithGivenRoleName);
                }
            }
            else
            {
                throw new InvalidOperationException(ErrorMessages.Role.NotFoundWithGivenRoleId);
            }

        }
    }
}
