using AdminProyectos.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.AccesoADatos
{
    public class TareaDAL
    {
        public static async Task<int> CrearAsync(Tarea tarea)
        {
            int result = 0;
            using (var bdContexto = new ContextoDb())
            {
                tarea.Estado = (byte)Estado_Tarea.APROBADA;
                bdContexto.Add(tarea);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Tarea tarea)
        {
            var result = 0;
            using (var bdContexto = new ContextoDb())
            {
                var tareaBd = await bdContexto.Tareas.FirstOrDefaultAsync(c => c.Id == tarea.Id);
                if (tareaBd != null)
                {
                    tareaBd.IdProyecto = tarea.IdProyecto;
                    tareaBd.Nombre = tarea.Nombre;
                    tareaBd.Descripcion = tarea.Descripcion;
                    tareaBd.Duracion = tarea.Duracion;

                    bdContexto.Update(tareaBd);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<int> CambiarEstadoAsync(Tarea tarea)
        {
            var result = 0;
            using (var bdContexto = new ContextoDb())
            {
                var tareaBd = await bdContexto.Tareas.FirstOrDefaultAsync(c => c.Id == tarea.Id);
                if (tareaBd != null)
                {
                    tareaBd.Estado = tarea.Estado;
                    bdContexto.Update(tareaBd);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<int> EliminarAsync(Tarea tarea)
        {
            var result = 0;
            using (var bdContexto = new ContextoDb())
            {
                var tareaBd = await bdContexto.Tareas.FirstOrDefaultAsync(c => c.Id == tarea.Id);
                if (tareaBd != null)
                {
                    bdContexto.Tareas.Remove(tareaBd);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<List<Tarea>> ObtenerTodosAsync()
        {
            var tareas = new List<Tarea>();
            using (var bdContexto = new ContextoDb())
            {
                tareas = await bdContexto.Tareas.ToListAsync();
            }
            return tareas;
        }

        public static async Task<Tarea> ObtenerPorIdAsync(Tarea tarea)
        {
            var tareaDb = new Tarea();
            using (var bdContexto = new ContextoDb())
            {
                tareaDb = await bdContexto.Tareas.Include(t => t.Proyecto).FirstOrDefaultAsync(c => c.Id == tarea.Id);
                if (tareaDb != null)
                    return tareaDb;
            }
            return tareaDb;
        }

        public static async Task<List<Tarea>> ObtenerPorIdProyectoAsync(int id)
        {
            var tareas = new List<Tarea>();
            using (var bdContexto = new ContextoDb())
            {
                tareas = await bdContexto.Tareas.Where(t => t.IdProyecto == id).ToListAsync();
            }
            return tareas;
        }

        internal static IQueryable<Tarea> QuerySelect(IQueryable<Tarea> query, Tarea tarea)
        {
            if (tarea.Id > 0)
                query = query.Where(t => t.Id == tarea.Id);

            if (!string.IsNullOrWhiteSpace(tarea.Nombre))
                query = query.Where(t => t.Nombre.Contains(tarea.Nombre));

            if (!string.IsNullOrWhiteSpace(tarea.Descripcion))
                query = query.Where(t => t.Descripcion.Contains(tarea.Descripcion));

            if (!string.IsNullOrWhiteSpace(tarea.Duracion))
                query = query.Where(t => t.Duracion.Contains(tarea.Duracion));

            if(tarea.Estado > 0)
                query = query.Where(t => t.Estado == tarea.Estado);

            query = query.OrderByDescending(t => t.Id).AsQueryable();

            if (tarea.Top_Aux > 0)
                query = query.Take(tarea.Top_Aux).AsQueryable();

            return query;
        }

        public static async Task<List<Tarea>> BuscarAsync(Tarea tarea)
        {
            var tareas = new List<Tarea>();
            using (var bdContexto = new ContextoDb())
            {
                var select = bdContexto.Tareas.AsQueryable();
                select = QuerySelect(select, tarea);
                tareas = await select.ToListAsync();
            }
            return tareas;
        }

        public static async Task<List<Tarea>> BuscarIncluirProyectoAsync(Tarea tarea)
        {
            var tareas = new List<Tarea>();
            using (var bdContexto = new ContextoDb())
            {
                var select = bdContexto.Tareas.AsQueryable();
                select = QuerySelect(select, tarea).Include(t => t.Proyecto).AsQueryable();
                tareas = await select.ToListAsync();
            }
            return tareas;
        }
    }
}
