using AdminProyectos.EntidadesDeNegocio;
using AdminProyectos.LogicaDeNegocio;
using AdminProyectos.WebAPI.Dtos.Proyecto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AdminProyectos.WebAPI.Controllers
{
    [Route("api/proyectos")]
    [ApiController]
    [Authorize]
    public class ProyectoController : ControllerBase
    {
        private ProyectoBL proyectoBL = new ProyectoBL();
        private IMapper mapper;

        public ProyectoController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProyectoSalida>> Get()
        {
            List<Proyecto> proyectos = await proyectoBL.ObtenerTodosAsync();
            return mapper.Map<IEnumerable<ProyectoSalida>>(proyectos);
        }

        [HttpGet("{id}")]
        public async Task<ProyectoSalida> Get(int id)
        {
            Proyecto proyecto = await proyectoBL.ObtenerPorIdAsync(new Proyecto { Id = id });
            return mapper.Map<ProyectoSalida>(proyecto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProyectoGuardar proyectoGuardar)
        {
            try
            {
                DateOnly fechaInicio = DateOnly.Parse(proyectoGuardar.FechaInicio);
                DateOnly fechaFin = DateOnly.Parse(proyectoGuardar.FechaFin);
                Proyecto proyecto = mapper.Map<Proyecto>(proyectoGuardar);
                proyecto.FechaInicio = fechaInicio;
                proyecto.FechaFin = fechaFin;
                await proyectoBL.CrearAsync(proyecto);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProyectoModificar proyectoModificar)
        {

            if (proyectoModificar.Id == id)
            {
                DateOnly fechaInicio = DateOnly.Parse(proyectoModificar.FechaInicio);
                DateOnly fechaFin = DateOnly.Parse(proyectoModificar.FechaFin);
                Proyecto proyecto = mapper.Map<Proyecto>(proyectoModificar);
                proyecto.FechaInicio = fechaInicio;
                proyecto.FechaFin = fechaFin;
                await proyectoBL.ModificarAsync(proyecto);
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
                await proyectoBL.EliminarAsync(new Proyecto { Id = id });
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<ProyectoSalida>> Buscar([FromBody] object pProyecto)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strProyecto = JsonSerializer.Serialize(pProyecto);
            Proyecto proyecto = JsonSerializer.Deserialize<Proyecto>(strProyecto, option);
            List<Proyecto> proyectos = await proyectoBL.BuscarIncluirCategoriaAsync(proyecto);
            return mapper.Map<List<ProyectoSalida>>(proyectos);
        }
    }
}
