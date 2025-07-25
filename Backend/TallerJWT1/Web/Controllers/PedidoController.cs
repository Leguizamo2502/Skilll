using Business.Interfaces;
using Entity.DTOs;
using Entity.Models;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class PedidoController : BaseController<CreatePedidoDto, PedidoDto, IPedidoService>
    {
        public PedidoController(IPedidoService service, ILogger<PedidoController> logger) : base(service, logger)
        {
        }

        protected override Task<IEnumerable<PedidoDto>> GetAllAsync()
        {
            return _service.GetAllAsync();
        }
        protected override Task<PedidoDto?> GetByIdAsync(int id)
        {
            return _service.GetByIdAsync(id);
        }
        protected override Task AddAsync(CreatePedidoDto dto)
        {
            return _service.CreateAsync(dto);
        }

        protected override Task<bool> UpdateAsync(int id, CreatePedidoDto dto)
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
