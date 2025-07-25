

using AutoMapper;
using Business.Repository;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Models;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Business
{
    public class RolBusiness : BusinessBasic<RolDto,Rol>
    {
        private IData<Rol> _data;
        private ILogger<RolBusiness> _logger;

        public RolBusiness(IData<Rol> data, ILogger<RolBusiness> logger, IMapper mapper): base(data, mapper)
        {
            _data = data;
            _logger = logger;
        }
       

        protected override void ValidateDto(RolDto dto)
        {
            if (dto == null)
            {
                throw new ValidationException("El objeto rol no debe ser nulo");
            }
            if (string.IsNullOrWhiteSpace(dto.name))
            {
                _logger.LogWarning("Se intentó crear/actualizar un rol con Name vacío");
                throw new ValidationException("rol_name", "El Name del rol es obligatorio");
            }
        }

        protected override async Task ValidateIdAsync(int id)
        {
            var rol = await _data.GetByIdAsync(id);
            if (rol == null)
            {
                _logger.LogWarning($"se intento obtener un rol con id {id} que no existe");
                throw new EntityNotFoundException($"El rol con id {id} no existe");
            }
        }

    }
}
