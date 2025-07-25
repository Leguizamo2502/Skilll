using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs;
using Entity.Models;

namespace Data.Interfaces
{
    public interface IUser : IData<User>
    {
        Task<User> ValidateUser(LoginDto login);

    }
}
