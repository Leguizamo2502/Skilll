using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class CreatePedidoDto : BaseDto
    {
        public int ClienteId { get; set; }
        //public Cliente Cliente { get; set; }

        public int PizzaId { get; set; }
        
    }
}
