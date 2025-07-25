using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Repository;
using Entity.Context;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class PedidoRepository : DataGeneric<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _dbSet
                .Include(p=>p.Pizza)
                .Include(p=>p.Cliente)
                .ToListAsync();
        }
    }
}
