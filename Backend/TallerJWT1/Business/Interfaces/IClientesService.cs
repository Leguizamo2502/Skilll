using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs;
using Entity.Models;

namespace Business.Interfaces
{
    public interface IClientesService : IBussines<ClienteDto,ClienteDto>
    {
    }
}
