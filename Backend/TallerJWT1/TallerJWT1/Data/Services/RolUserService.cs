using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Repository;
using Entity.Context;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class RolUserService : DataGeneric<RolUser>, IRolUserRepository
    {
        public RolUserService(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<RolUser>> GetAllJoinAsync()
        {
            return await _dbSet
                .Include(ru => ru.user)
                .Include(ru => ru.rol)
                .ToListAsync();
        }

        public async Task<RolUser?> GetByIdJoinAsync(int id)
        {
            return await _dbSet
                .Include(ru => ru.user)
                .Include(ru => ru.rol)
                .Where(ru => ru.id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetRolUserAsync(int idUser)
        {
            // Usar el _dbSet genérico para hacer la consulta
            var roleAssignments = await _dbSet
                .Include(ru => ru.rol)  // Incluir la relación con la entidad 'rol'
                .Where(ru => ru.userId == idUser)  // Filtros adecuados
                .ToListAsync();  // Ejecutar la consulta de manera asincrónica

            // Extraer los nombres de los roles, asegurándose de que no sean vacíos ni nulos
            var roles = roleAssignments
                            .Select(ru => ru.rol.name)
                            .Where(name => !string.IsNullOrWhiteSpace(name))  // Filtrar nombres no vacíos
                            .Distinct()  // Eliminar duplicados
                            .ToList();  // Convertir el resultado en una lista

            return roles;

        }




    }
}
