using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IBussines<TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
