using Business.Interfaces;
using Entity.DTOs;
using Entity.Models;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class PizzaController : BaseController<PizzaDto, PizzaDto, IPizzasService>
    {
        public PizzaController(IPizzasService service, ILogger<PizzaController> logger) : base(service, logger)
        {
        }


        protected override Task<IEnumerable<PizzaDto>> GetAllAsync()
        {
            return _service.GetAllAsync();
        }
        protected override Task<PizzaDto?> GetByIdAsync(int id)
        {
            return _service.GetByIdAsync(id);
        }
        protected override Task AddAsync(PizzaDto dto)
        {
            return _service.CreateAsync(dto);
        }

        protected override Task<bool> UpdateAsync(int id, PizzaDto dto)
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
