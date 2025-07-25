using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class PizzaDto : BaseDto
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }
    }
}
