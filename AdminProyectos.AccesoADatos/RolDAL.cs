using AdminProyectos.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.AccesoADatos
{
    public class RolDAL
    {
        public static async Task<int> CrearAsync(Rol rol)
        {
            int result = 0;
            using (var bdContexto = new ContextoDb())
            {
                bdContexto.Add(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Rol rol)
        {
            int result = 0;
            using (var bdContexto = new ContextoDb())
            {
                var rolBd = await bdContexto.Roles.FirstOrDefaultAsync(r => r.Id == rol.Id);
                rolBd.Nombre = rol.Nombre;
                bdContexto.Update(rolBd);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Rol rol)
        {
            int result = 0;
            using (var bdContexto = new ContextoDb())
            {
                var rolBd = await bdContexto.Roles.FirstOrDefaultAsync(r => r.Id == rol.Id);
                bdContexto.Roles.Remove(rolBd);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Rol> ObtenerPorIdAsync(Rol rol)
        {
            var rolBd = new Rol();
            using (var bdContexto = new ContextoDb())
            {
                rolBd = await bdContexto.Roles.FirstOrDefaultAsync(r => r.Id == rol.Id);
            }
            return rolBd;
        }

        public static async Task<List<Rol>> ObtenerTodosAsync()
        {
            var roles = new List<Rol>();
            using (var bdContexto = new ContextoDb())
            {
                roles = await bdContexto.Roles.ToListAsync();
            }
            return roles;
        }

        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> query, Rol rol)
        {
            if (rol.Id > 0)
                query = query.Where(r => r.Id == rol.Id);

            if (!string.IsNullOrWhiteSpace(rol.Nombre))
                query = query.Where(r => r.Nombre.Contains(rol.Nombre));

            query = query.OrderByDescending(r => r.Id).AsQueryable();

            if (rol.Top_Aux > 0)
                query = query.Take(rol.Top_Aux).AsQueryable();

            return query;
        }

        public static async Task<List<Rol>> BuscarAsync(Rol rol)
        {
            var roles = new List<Rol>();
            using (var bdContexto = new ContextoDb())
            {
                var select = bdContexto.Roles.AsQueryable();
                select = QuerySelect(select, rol);
                roles = await select.ToListAsync();
            }
            return roles;
        }
    }
}
