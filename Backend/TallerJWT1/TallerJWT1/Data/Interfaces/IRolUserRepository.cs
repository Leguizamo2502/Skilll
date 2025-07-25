
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs;
using Entity.Models;

namespace Data.Interfaces
{
    public interface IRolUserRepository : IData<RolUser>
    {
        Task<IEnumerable<RolUser>> GetAllJoinAsync();
        Task<RolUser?> GetByIdJoinAsync(int id);
        Task<IEnumerable<string>> GetRolUserAsync(int idUser);


    }
}
