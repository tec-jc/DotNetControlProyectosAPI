using AdminProyectos.AccesoADatos;
using AdminProyectos.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.LogicaDeNegocio
{
    public class CategoriaBL
    {
        public async Task<int> CrearAsync(Categoria categoria)
        {
            return await CategoriaDAL.CrearAsync(categoria);
        }

        public async Task<int> ModificarAsync(Categoria categoria)
        {
            return await CategoriaDAL.ModificarAsync(categoria);
        }

        public async Task<int> EliminarAsync(Categoria categoria)
        {
            return await CategoriaDAL.EliminarAsync(categoria);
        }

        public async Task<List<Categoria>> ObtenerTodosAsync()
        {
            return await CategoriaDAL.ObtenerTodosAsync();
        }

        public async Task<Categoria> ObtenerPorIdAsync(Categoria categoria)
        {
            return await CategoriaDAL.ObtenerPorIdAsync(categoria);
        }

        public async Task<List<Categoria>> BuscarAsync(Categoria categoria)
        {
            return await CategoriaDAL.BuscarAsync(categoria);
        }
    }
}
