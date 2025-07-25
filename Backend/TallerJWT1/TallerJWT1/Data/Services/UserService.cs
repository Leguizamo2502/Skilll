using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Repository;
using Entity.Context;
using Entity.DTOs;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Utilities.Custom;

namespace Data.Services
{
    public class UserService : DataGeneric<User>,IUser
    {
        private readonly EncriptePassword _encriptePass;
        public UserService(ApplicationDbContext context, EncriptePassword encriptePass) : base(context)
        {
            _encriptePass = encriptePass;
        }

        public async Task<User> ValidateUser(LoginDto loginDto)
        {
            bool suceeded = false;

            var user = await _dbSet
                .Where(u =>
                            u.email == loginDto.email &&
                            u.password == _encriptePass.EncryptSHA256(loginDto.password))
                .FirstOrDefaultAsync();

            suceeded = (user != null) ? true : throw new UnauthorizedAccessException("Credenciales inválidas");

            return user;
        }



    }
}
