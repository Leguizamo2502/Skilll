using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs;

namespace Business.Interfaces
{
    public interface IToken
    {
        Task<string> CreateToken(LoginDto login);
        bool validarToken(string token);
    }
}
