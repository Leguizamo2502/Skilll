using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IBusinessExtend<TDtoGet,TDto> : IBussines<TDto>
    {
        Task<IEnumerable<TDtoGet>> GetAllJoinAsync();
        Task<TDtoGet?> GetByIdJoinAsync(int id);
        Task<IEnumerable<string>> GetRolUserAsync(int idUser);
    }
    
}
