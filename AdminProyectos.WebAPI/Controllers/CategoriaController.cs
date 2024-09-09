using AdminProyectos.AccesoADatos;
using AdminProyectos.EntidadesDeNegocio;
using AdminProyectos.LogicaDeNegocio;
using AdminProyectos.WebAPI.Dtos.Categoria;
using AdminProyectos.WebAPI.Dtos.Rol;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AdminProyectos.WebAPI.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private CategoriaBL categoriaBL = new CategoriaBL();
        private IMapper mapper;

        public CategoriaController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaSalida>> Get()
        {
            List<Categoria> categorias = await categoriaBL.ObtenerTodosAsync();
            return mapper.Map<IEnumerable<CategoriaSalida>>(categorias);
        }

        [HttpGet("{id}")]
        public async Task<CategoriaSalida> Get(int id)
        {
            Categoria categoria = await categoriaBL.ObtenerPorIdAsync(new Categoria { Id = id });
            return mapper.Map<CategoriaSalida>(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriaGuardar categoriaGuardar)
        {
            try
            {
                Categoria categoria = mapper.Map<Categoria>(categoriaGuardar);
                await categoriaBL.CrearAsync(categoria);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriaModificar categoriaModificar)
        {

            if (categoriaModificar.Id == id)
            {
                Categoria categoria = mapper.Map<Categoria>(categoriaModificar);
                await categoriaBL.ModificarAsync(categoria);
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
                await categoriaBL.EliminarAsync(new Categoria { Id = id });
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<CategoriaSalida>> Buscar([FromBody] object pCategoria)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strCategoria = JsonSerializer.Serialize(pCategoria);
            Categoria categoria = JsonSerializer.Deserialize<Categoria>(strCategoria, option);
            List<Categoria> categorias = await categoriaBL.BuscarAsync(categoria);
            return mapper.Map<List<CategoriaSalida>>(categorias);
        }
    }
}
