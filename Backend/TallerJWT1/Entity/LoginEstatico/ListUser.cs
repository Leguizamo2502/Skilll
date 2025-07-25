using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.LoginEstatico
{
    public static class ListUser
    {
        public static List<StaticUser> Users = new List<StaticUser>
        {
        new StaticUser { Username = "admin", Password = "admin1", Rol = "Administrador" },
        new StaticUser { Username = "admin1", Password = "user1", Rol = "Asistente" },
        new StaticUser { Username = "pizzero", Password = "pizzero1", Rol = "Pizzero" }
        };
    }
}
