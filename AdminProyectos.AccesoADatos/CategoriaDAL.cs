using AdminProyectos.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.AccesoADatos
{
    public class CategoriaDAL
    {
        public static async Task<int> CrearAsync(Categoria categoria)
        {
            int result = 0;
            using(var bdContexto = new ContextoDb())
            {
                bdContexto.Add(categoria);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Categoria categoria)
        {
            var result = 0;
            using (var bdContexto = new ContextoDb())
            {
                var categoriaBd = await bdContexto.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);
                if (categoriaBd != null)
                {
                    categoriaBd.Nombre = categoria.Nombre;
                    bdContexto.Update(categoriaBd);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<int> EliminarAsync(Categoria categoria)
        {
            var result = 0;
            using (var bdContexto = new ContextoDb())
            {
                var categoriaBd = await bdContexto.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);
                if (categoriaBd != null)
                {
                    bdContexto.Categorias.Remove(categoriaBd);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }

        public static async Task<List<Categoria>> ObtenerTodosAsync()
        {
            var categorias = new List<Categoria>();
            using (var bdContexto = new ContextoDb())
            {
                categorias = await bdContexto.Categorias.ToListAsync();
            }
            return categorias;
        }

        public static async Task<Categoria> ObtenerPorIdAsync(Categoria categoria)
        {
            var categoriaDb = new Categoria();
            using(var bdContexto = new ContextoDb())
            {
                categoriaDb = await bdContexto.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);
                if (categoriaDb != null)
                    return categoriaDb;
            }
            return categoriaDb;
        }

        internal static IQueryable<Categoria> QuerySelect(IQueryable<Categoria> query, Categoria categoria)
        {
            if(categoria.Id > 0)
                query = query.Where(c => c.Id == categoria.Id);

            if (!string.IsNullOrWhiteSpace(categoria.Nombre))
                query = query.Where(c => c.Nombre.Contains(categoria.Nombre));

            query = query.OrderByDescending(c => c.Id).AsQueryable();

            if(categoria.Top_Aux > 0)
                query = query.Take(categoria.Top_Aux).AsQueryable();

            return query;
        }

        public static async Task<List<Categoria>> BuscarAsync(Categoria categoria)
        {
            var categorias = new List<Categoria>();
            using(var bdContexto = new ContextoDb())
            {
                var select = bdContexto.Categorias.AsQueryable();
                select = QuerySelect(select, categoria);
                categorias = await select.ToListAsync();
            }
            return categorias;
        }
    }
}
