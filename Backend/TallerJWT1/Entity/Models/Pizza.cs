using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.BaseModel;

namespace Entity.Models
{
    public class Pizza : ModelGeneric
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }
    }
}
