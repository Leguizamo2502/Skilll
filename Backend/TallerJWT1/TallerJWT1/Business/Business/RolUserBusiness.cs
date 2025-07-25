
using AutoMapper;
using Business.Interfaces;
using Business.Repository;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Models;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Business
{
    public class RolUserBusiness : BusinessGeneric<RolUserDto, RolUserUpdateDto, RolUser>, IBusinessExtend<RolUserDto, RolUserUpdateDto>
    {
        private IRolUserRepository _data;
        private ILogger<RolUserBusiness> _logger;

        public RolUserBusiness(IRolUserRepository data, ILogger<RolUserBusiness> logger,IMapper mapper):base(data, mapper)
        {
            _data = data;
            _logger = logger;
        }
        
        public async Task<IEnumerable<RolUserDto>> GetAllJoinAsync()
        {
            var entities = await _data.GetAllJoinAsync();
            return _mapper.Map<IEnumerable<RolUserDto>>(entities);
        }

        public async Task<RolUserDto?> GetByIdJoinAsync(int id)
        {
            var entity = await _data.GetByIdJoinAsync(id);
            return entity == null ? default : _mapper.Map<RolUserDto>(entity);
        }

        public async Task<IEnumerable<string>> GetRolUserAsync(int idUser)
        {
            var roles = await _data.GetRolUserAsync(idUser);
            return roles;
        }

        protected override void ValidateDto(RolUserUpdateDto dto)
        {
            if (dto == null)
            {
                throw new ValidationException("El objeto Rol no puede ser nulo");
            }
        }

        protected async override Task ValidateIdAsync(int id)
        {
            var entity = await _data.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning($"Se intentó operar con un ID inválido: {id}");
                throw new EntityNotFoundException($"No se encontró una Rol con el ID {id}");
            }
        }
    }
}
