using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;

namespace Entity.DTOs
{
    public class PedidoDto :BaseDto
    {
        public string ClienteNombre { get; set; }
        public int ClienteId { get; set; }
        //public Cliente Cliente { get; set; }

        public int PizzaId { get; set; }
        public string PizzaNombre { get; set; }
        //public Pizza Pizza { get; set; }

        public string Estado { get; set; }
    }
}
