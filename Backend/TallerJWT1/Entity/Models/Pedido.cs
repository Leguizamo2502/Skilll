using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.BaseModel;

namespace Entity.Models
{
    public class Pedido : ModelGeneric
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public string Estado { get; set; } = "Pendiente";
    }
}
