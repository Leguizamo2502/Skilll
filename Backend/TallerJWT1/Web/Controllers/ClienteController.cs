using Business.Interfaces;
using Entity.DTOs;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClienteController : BaseController<ClienteDto, ClienteDto, IClientesService>
    {
        public ClienteController(IClientesService service, ILogger<ClienteController> logger) : base(service, logger)
        {
        }

        protected override Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            return _service.GetAllAsync();
        }
        protected override Task<ClienteDto?> GetByIdAsync(int id)
        {
            return _service.GetByIdAsync(id);
        }
        protected override Task AddAsync(ClienteDto dto)
        {
            return _service.CreateAsync(dto);
        }

        protected override Task<bool> UpdateAsync(int id, ClienteDto dto)
        {
            return _service.UpdateAsync(dto);
        }

        protected override async Task<bool> DeleteAsync(int id)
        {
            var form = await _service.GetByIdAsync(id);
            if (form is null) return false;

            await _service.DeleteAsync(id);
            return true;
        }
    }
}
