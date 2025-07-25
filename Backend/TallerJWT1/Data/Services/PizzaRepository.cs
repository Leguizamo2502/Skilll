using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Repository;
using Entity.Context;
using Entity.Models;

namespace Data.Services
{
    public class PizzaRepository : DataGeneric<Pizza>, IPizzaRepository
    {
        public PizzaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
