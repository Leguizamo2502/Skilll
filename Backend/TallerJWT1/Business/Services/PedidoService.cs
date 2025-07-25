using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Business.Repository;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Models;

namespace Business.Business
{
    public class PedidoService : BusinessBasic<CreatePedidoDto,PedidoDto,Pedido>, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        public PedidoService(IData<Pedido> data, IMapper mapper, IPedidoRepository pedidoRepository) : base(data, mapper)
        {
            _pedidoRepository = pedidoRepository;
        }

        public override async Task<IEnumerable<PedidoDto>> GetAllAsync()
        {
            var entities = await _pedidoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PedidoDto>>(entities);
        }

        protected override void ValidateDto(CreatePedidoDto dto)
        {
            if (dto.ClienteId <= 0)
                throw new ArgumentException("ClienteId inválido.");

            if (dto.PizzaId <= 0)
                throw new ArgumentException("PizzaId inválido.");

        }

        protected override async Task ValidateIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero");

            var entity = await Data.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("No se encontró la entidad con ese ID");
        }
    }
}
