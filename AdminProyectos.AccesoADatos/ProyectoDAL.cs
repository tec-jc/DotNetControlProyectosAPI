using AdminProyectos.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.AccesoADatos
{
    public class ProyectoDAL
    {
        public static async Task<int> CrearAsync(Proyecto proyecto)
        {
            int result = 0;
            using (var bdContexto = new ContextoDb())
            {
                bdContexto.Add(proyecto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Proyecto proyecto)
        {
            var result = 0;
            using (var bdContexto = new ContextoDb())
            {
                var proyectoBd = await bdContexto.Proyectos.FirstOrDefaultAsync(c => c.Id == proyecto.Id);
                if (proyectoBd != null)
                {
                    proyectoBd.IdCategoria = proyecto.IdCategoria;
                    proyectoBd.Titulo = proyecto.Titulo;
                    proyectoBd.Descripcion = proyecto.Descripcion;
                    proyectoBd.FechaInicio = proyecto.FechaInicio;
                    proyectoBd.FechaFin = proyecto.FechaFin;
                    bdContexto.Update(proyectoBd);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<int> EliminarAsync(Proyecto proyecto)
        {
            var result = 0;
            using (var bdContexto = new ContextoDb())
            {
                var proyectoBd = await bdContexto.Proyectos.FirstOrDefaultAsync(c => c.Id == proyecto.Id);
                if (proyectoBd != null)
                {
                    bdContexto.Proyectos.Remove(proyectoBd);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<List<Proyecto>> ObtenerTodosAsync()
        {
            var proyectos = new List<Proyecto>();
            using (var bdContexto = new ContextoDb())
            {
                proyectos = await bdContexto.Proyectos.ToListAsync();
            }
            return proyectos;
        }

        public static async Task<Proyecto> ObtenerPorIdAsync(Proyecto proyecto)
        {
            var proyectoDb = new Proyecto();
            using (var bdContexto = new ContextoDb())
            {
                proyectoDb = await bdContexto.Proyectos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == proyecto.Id);
                if (proyectoDb != null)
                    return proyectoDb;
            }
            return proyectoDb;
        }

        internal static IQueryable<Proyecto> QuerySelect(IQueryable<Proyecto> query, Proyecto proyecto)
        {
            if (proyecto.Id > 0)
                query = query.Where(p => p.Id == proyecto.Id);

            if (!string.IsNullOrWhiteSpace(proyecto.Titulo))
                query = query.Where(p => p.Titulo.Contains(proyecto.Titulo));

            if (!string.IsNullOrWhiteSpace(proyecto.Descripcion))
                query = query.Where(p => p.Descripcion.Contains(proyecto.Descripcion));

            if(proyecto.FechaInicio.Year > 1900)
                query = query.Where(p => p.FechaInicio == proyecto.FechaInicio);

            if (proyecto.FechaFin.Year > 1900)
                query = query.Where(p => p.FechaFin == proyecto.FechaFin);

            query = query.OrderByDescending(p => p.Id).AsQueryable();

            if (proyecto.Top_Aux > 0)
                query = query.Take(proyecto.Top_Aux).AsQueryable();

            return query;
        }

        public static async Task<List<Proyecto>> BuscarAsync(Proyecto proyecto)
        {
            var proyectos = new List<Proyecto>();
            using (var bdContexto = new ContextoDb())
            {
                var select = bdContexto.Proyectos.AsQueryable();
                select = QuerySelect(select, proyecto);
                proyectos = await select.ToListAsync();
            }
            return proyectos;
        }

        public static async Task<List<Proyecto>> BuscarIncluirCategoriaAsync(Proyecto proyecto)
        {
            var proyectos = new List<Proyecto>();
            using (var bdContexto = new ContextoDb())
            {
                var select = bdContexto.Proyectos.AsQueryable();
                select = QuerySelect(select, proyecto).Include(p => p.Categoria).AsQueryable();
                proyectos = await select.ToListAsync();
            }
            return proyectos;
        }
    }
}
