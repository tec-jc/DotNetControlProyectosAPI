using AdminProyectos.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.AccesoADatos
{
    public class UsuarioDAL
    {
        private static void EncriptarMD5(Usuario usuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(usuario.Clave));
                var strEncriptado = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptado += result[i].ToString("x2").ToLower();
                usuario.Clave = strEncriptado;
            }
        }

        private static async Task<bool> ExisteLogin(Usuario usuario, ContextoDb pDbContext)
        {
            bool result = false;
            var loginUsuarioExiste = await pDbContext.Usuarios.FirstOrDefaultAsync(s => s.Login == usuario.Login && s.Id != usuario.Id);
            if (loginUsuarioExiste != null && loginUsuarioExiste.Id > 0 && loginUsuarioExiste.Login == usuario.Login)
                result = true;
            return result;
        }

        public static async Task<int> CrearAsync(Usuario usuario)
        {
            int result = 0;
            using (var contextoDb = new ContextoDb())
            {
                bool existeLogin = await ExisteLogin(usuario, contextoDb);
                if (existeLogin == false)
                {
                    EncriptarMD5(usuario);
                    contextoDb.Add(usuario);
                    result = await contextoDb.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Usuario usuario)
        {
            int result = 0;
            using (var contextoDb = new ContextoDb())
            {
                bool existeLogin = await ExisteLogin(usuario, contextoDb);
                if (existeLogin == false)
                {
                    var usuarioBd = await contextoDb.Usuarios.FirstOrDefaultAsync(s => s.Id == usuario.Id);
                    usuarioBd.IdRol = usuario.IdRol;
                    usuarioBd.Nombre = usuario.Nombre;
                    usuarioBd.Apellido = usuario.Apellido;
                    usuarioBd.Login = usuario.Login;
                    contextoDb.Update(usuario);
                    result = await contextoDb.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Usuario usuario)
        {
            int result = 0;
            using (var contextoDb = new ContextoDb())
            {
                var usuarioBd = await contextoDb.Usuarios.FirstOrDefaultAsync(s => s.Id == usuario.Id);
                contextoDb.Usuarios.Remove(usuarioBd);
                result = await contextoDb.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Usuario> ObtenerPorIdAsync(Usuario usuario)
        {
            var usuarioBd = new Usuario();
            using (var ContextoDb = new ContextoDb())
            {
                usuarioBd = await ContextoDb.Usuarios.FirstOrDefaultAsync(s => s.Id == usuario.Id);
            }
            return usuario;
        }

        public static async Task<List<Usuario>> ObtenerTodosAsync()
        {
            var usuarios = new List<Usuario>();
            using (var ContextoDb = new ContextoDb())
            {
                usuarios = await ContextoDb.Usuarios.ToListAsync();
            }
            return usuarios;
        }

        internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, Usuario usuario)
        {
            if (usuario.Id > 0)
                pQuery = pQuery.Where(u => u.Id == usuario.Id);

            if (usuario.IdRol > 0)
                pQuery = pQuery.Where(u => u.IdRol == usuario.IdRol);

            if (!string.IsNullOrWhiteSpace(usuario.Nombre))
                pQuery = pQuery.Where(u => u.Nombre.Contains(usuario.Nombre));

            if (!string.IsNullOrWhiteSpace(usuario.Apellido))
                pQuery = pQuery.Where(u => u.Apellido.Contains(usuario.Apellido));

            if (!string.IsNullOrWhiteSpace(usuario.Login))
                pQuery = pQuery.Where(u => u.Login.Contains(usuario.Login));

            pQuery = pQuery.OrderByDescending(u => u.Id).AsQueryable();

            if (usuario.Top_Aux > 0)
                pQuery = pQuery.Take(usuario.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Usuario>> BuscarAsync(Usuario usuario)
        {
            var usuarios = new List<Usuario>();
            using (var contextoDb = new ContextoDb())
            {
                var select = contextoDb.Usuarios.AsQueryable();
                select = QuerySelect(select, usuario);
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }

        public static async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario usuario)
        {
            var usuarios = new List<Usuario>();
            using (var contextoDb = new ContextoDb())
            {
                var select = contextoDb.Usuarios.AsQueryable();
                select = QuerySelect(select, usuario).Include(s => s.Rol).AsQueryable();
                usuarios = await select.ToListAsync();
            }

            return usuarios;
        }

        public static async Task<Usuario> LoginAsync(Usuario usuario)
        {
            var usuarioBd = new Usuario();
            using (var contextoDb = new ContextoDb())
            {
                EncriptarMD5(usuario);
                usuarioBd = await contextoDb.Usuarios.FirstOrDefaultAsync(s => s.Login == usuario.Login &&
                s.Clave == usuario.Clave);
            }
            return usuarioBd;
        }

        public static async Task<int> CambiarClaveAsync(Usuario usuario, string pClaveAnt)
        {
            int result = 0;
            var usuarioPassAnt = new Usuario { Clave = pClaveAnt };
            EncriptarMD5(usuarioPassAnt);
            using (var contextoDb = new ContextoDb())
            {
                var usuarioBd = await contextoDb.Usuarios.FirstOrDefaultAsync(s => s.Id == usuario.Id);
                if (usuarioPassAnt.Clave == usuario.Clave)
                {
                    EncriptarMD5(usuario);
                    usuario.Clave = usuario.Clave;
                    contextoDb.Update(usuario);
                    result = await contextoDb.SaveChangesAsync();
                }
                else
                    throw new Exception("La clave actual es incorrecta");
            }
            return result;
        }
    }
}
