using AdminProyectos.AccesoADatos;
using AdminProyectos.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.LogicaDeNegocio
{
    public class UsuarioBL
    {
        public async Task<int> CrearAsync(Usuario usuario)
        {
            return await UsuarioDAL.CrearAsync(usuario);
        }

        public async Task<int> ModificarAsync(Usuario usuario)
        {
            return await UsuarioDAL.ModificarAsync(usuario);
        }

        public async Task<int> EliminarAsync(Usuario usuario)
        {
            return await UsuarioDAL.EliminarAsync(usuario);
        }

        public async Task<Usuario> ObtenerPorIdAsync(Usuario usuario)
        {
            return await UsuarioDAL.ObtenerPorIdAsync(usuario);
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await UsuarioDAL.ObtenerTodosAsync();
        }

        public async Task<List<Usuario>> BuscarAsync(Usuario usuario)
        {
            return await UsuarioDAL.BuscarAsync(usuario);
        }

        public async Task<Usuario> LoginAsync(Usuario usuario)
        {
            return await UsuarioDAL.LoginAsync(usuario);
        }

        public async Task<int> CambiarClaveAsync(Usuario usuario, string pPasswordAnt)
        {
            return await UsuarioDAL.CambiarClaveAsync(usuario, pPasswordAnt);
        }

        public async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario usuario)
        {
            return await UsuarioDAL.BuscarIncluirRolesAsync(usuario);
        }
    }
}
