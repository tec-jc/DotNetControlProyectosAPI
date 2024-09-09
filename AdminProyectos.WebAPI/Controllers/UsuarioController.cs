﻿using AdminProyectos.EntidadesDeNegocio;
using AdminProyectos.LogicaDeNegocio;
using AdminProyectos.WebAPI.Auth;
using AdminProyectos.WebAPI.Dtos.Usuario;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AdminProyectos.WebAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private UsuarioBL usuarioBL = new UsuarioBL();

        // Inyección de dependencias para agregar la seguridad JWT
        private readonly IJwtAuthenticationService authService;
        private readonly IMapper mapper;

        public UsuarioController(IJwtAuthenticationService pAuthService, IMapper mapper)
        {
            authService = pAuthService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioSalida>> Get()
        {
            List<Usuario> usuarios = await usuarioBL.ObtenerTodosAsync();
            return mapper.Map<IEnumerable<UsuarioSalida>>(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<UsuarioSalida> Get(int id)
        {
            Usuario usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id =  id });
            return mapper.Map<UsuarioSalida>(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioGuardar usuarioGuardar)
        {
            try
            {
                Usuario usuario = mapper.Map<Usuario>(usuarioGuardar);
                await usuarioBL.CrearAsync(usuario);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioModificar usuarioModificar)
        {
            if (usuarioModificar.Id == id)
            {
                Usuario usuario = mapper.Map<Usuario>(usuarioModificar);
                await usuarioBL.ModificarAsync(usuario);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await usuarioBL.EliminarAsync(new Usuario { Id = id});
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<UsuarioSalida>> Buscar([FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            var usuarios = await usuarioBL.BuscarIncluirRolesAsync(usuario);
            usuarios.ForEach(s => s.Rol.Usuarios = null); // Evitar la redundacia de datos
            return mapper.Map<List<UsuarioSalida>>(usuarios);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UsuarioLogin usuarioLogin)
        {
            Usuario usuario = mapper.Map<Usuario>(usuarioLogin);
            // codigo para autorizar el usuario por JWT
            Usuario usuario_auth = await usuarioBL.LoginAsync(usuario);
            if (usuario_auth != null && usuario_auth.Id > 0 && usuario.Login == usuario_auth.Login)
            {
                var token = authService.Authenticate(usuario_auth);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
