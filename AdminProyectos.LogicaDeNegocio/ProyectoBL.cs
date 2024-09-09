using AdminProyectos.AccesoADatos;
using AdminProyectos.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.LogicaDeNegocio
{
    public class ProyectoBL
    {
        public async Task<int> CrearAsync(Proyecto proyecto)
        {
            return await ProyectoDAL.CrearAsync(proyecto);
        }

        public async Task<int> ModificarAsync(Proyecto proyecto)
        {
            return await ProyectoDAL.ModificarAsync(proyecto);
        }

        public async Task<int> EliminarAsync(Proyecto proyecto)
        {
            return await ProyectoDAL.EliminarAsync(proyecto);
        }

        public async Task<List<Proyecto>> ObtenerTodosAsync()
        {
            return await ProyectoDAL.ObtenerTodosAsync();
        }

        public async Task<Proyecto> ObtenerPorIdAsync(Proyecto proyecto)
        {
            return await ProyectoDAL.ObtenerPorIdAsync(proyecto);
        }

        public async Task<List<Proyecto>> BuscarAsync(Proyecto proyecto)
        {
            return await ProyectoDAL.BuscarAsync(proyecto);
        }

        public async Task<List<Proyecto>> BuscarIncluirCategoriaAsync(Proyecto proyecto)
        {
            return await ProyectoDAL.BuscarIncluirCategoriaAsync(proyecto);
        }
    }
}
