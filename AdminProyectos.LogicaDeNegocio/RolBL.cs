using AdminProyectos.AccesoADatos;
using AdminProyectos.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.LogicaDeNegocio
{
    public class RolBL
    {
        public async Task<int> CrearAsync(Rol rol)
        {
            return await RolDAL.CrearAsync(rol);
        }

        public async Task<int> ModificarAsync(Rol rol)
        {
            return await RolDAL.ModificarAsync(rol);
        }

        public async Task<int> EliminarAsync(Rol rol)
        {
            return await RolDAL.EliminarAsync(rol);
        }

        public async Task<Rol> ObtenerPorIdAsync(Rol rol)
        {
            return await RolDAL.ObtenerPorIdAsync(rol);
        }

        public async Task<List<Rol>> ObtenerTodosAsync()
        {
            return await RolDAL.ObtenerTodosAsync();
        }

        public async Task<List<Rol>> BuscarAsync(Rol rol)
        {
            return await RolDAL.BuscarAsync(rol);
        }
    }
}
