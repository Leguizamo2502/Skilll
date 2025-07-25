using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repository;
using Entity.DTOs;
using Entity.Models;

namespace Business.Interfaces
{
    public interface IPedidoService : IBussines<CreatePedidoDto,PedidoDto>
    {
    }
}
