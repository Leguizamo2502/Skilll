using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Repository;
using Data.Interfaces;
using Data.Services;
using Entity.DTOs;
using Entity.Models;
using Microsoft.Extensions.Logging;
using Utilities.Custom;
using Utilities.Exceptions;

namespace Business.Business
{
    public class UserBusiness : BusinessBasic<UserDto, User>
    {
        private readonly IUser _data;
        private readonly ILogger<UserBusiness> _logger;
        private readonly EncriptePassword _utilities;

        public UserBusiness(IUser data, ILogger<UserBusiness> logger, IMapper mapper, EncriptePassword utilities) :base(data, mapper)
        {
            _data = data;
            _logger = logger;
            _utilities = utilities;
        }
        

        protected override void ValidateDto(UserDto dto)
        {
            if (dto == null)
            {
                throw new ValidationException("El objeto user no debe ser nulo");
            }
            if (string.IsNullOrWhiteSpace(dto.name))
            {
                _logger.LogWarning("Se intentó crear/actualizar un user con Name vacío");
                throw new ValidationException("user_name", "El Name del user es obligatorio");
            }
        }
        protected override async Task ValidateIdAsync(int id)
        {
            var user = await _data.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"se intento obtener un user con id {id} que no existe");
                throw new EntityNotFoundException($"El user con id {id} no existe");
            }
        }


        //Nuevo metodo 

        public async Task<UserDto> CreateAsyncUser(UserDto dto)
        {
            ValidateDto(dto);

            // Mapeamos primero
            var userEntity = _mapper.Map<User>(dto);

            // Luego encripto la contraseña antes de guardar
            userEntity.password = _utilities.EncryptSHA256(userEntity.password);

            var createdEntity = await _data.CreateAsync(userEntity);
            return _mapper.Map<UserDto>(createdEntity);
        }

        //Actalizar
        public async Task<bool> UpdateAsyncUser(UserDto dto)
        {
            ValidateDto(dto);
            var entity = _mapper.Map<User>(dto);
            entity.password = _utilities.EncryptSHA256(entity.password);

            return await _data.UpdateAsync(entity);
        }
    }
    
}
