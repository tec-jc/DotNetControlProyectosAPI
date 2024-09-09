using AdminProyectos.AccesoADatos;
using AdminProyectos.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.LogicaDeNegocio
{
    public class TareaBL
    {
        public async Task<int> CrearAsync(Tarea tarea)
        {
            return await TareaDAL.CrearAsync(tarea);
        }

        public async Task<int> ModificarAsync(Tarea tarea)
        {
            return await TareaDAL.ModificarAsync(tarea);
        }

        public async Task<int> CambiarEstadoAsync(Tarea tarea)
        {
            return await TareaDAL.CambiarEstadoAsync(tarea);
        }

        public async Task<int> EliminarAsync(Tarea tarea)
        {
            return await TareaDAL.EliminarAsync(tarea);
        }

        public async Task<List<Tarea>> ObtenerTodosAsync()
        {
            return await TareaDAL.ObtenerTodosAsync();
        }

        public async Task<Tarea> ObtenerPorIdAsync(Tarea tarea)
        {
            return await TareaDAL.ObtenerPorIdAsync(tarea);
        }

        public async Task<List<Tarea>> ObtenerPorIdProyecto(int id)
        {
            return await TareaDAL.ObtenerPorIdProyectoAsync(id);
        }

        public async Task<List<Tarea>> BuscarAsync(Tarea tarea)
        {
            return await TareaDAL.BuscarAsync(tarea);
        }

        public async Task<List<Tarea>> BuscarIncluirProyectoAsync(Tarea tarea)
        {
            return await TareaDAL.BuscarIncluirProyectoAsync(tarea);
        }
    }
}
