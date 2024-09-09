using AdminProyectos.AccesoADatos;
using AdminProyectos.EntidadesDeNegocio;
using AdminProyectos.LogicaDeNegocio;
using AdminProyectos.WebAPI.Dtos.Tarea;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AdminProyectos.WebAPI.Controllers
{
    [Route("api/tareas")]
    [ApiController]
    [Authorize]
    public class TareaController : ControllerBase
    {
        private TareaBL tareaBL = new TareaBL();
        private IMapper mapper;

        public TareaController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TareaSalida>> Get()
        {
            List<Tarea> tareas = await tareaBL.ObtenerTodosAsync();
            return mapper.Map<List<TareaSalida>>(tareas);
        }

        [HttpGet("{id}")]
        public async Task<TareaSalida> Get(int id)
        {
            Tarea tarea = await tareaBL.ObtenerPorIdAsync(new Tarea { Id = id });
            return mapper.Map<TareaSalida>(tarea);
        }

        [HttpGet("/api/tareas/proyecto/{id}")]
        public async Task<IEnumerable<TareaSalida>> GetByProject(int id)
        {
            List<Tarea> tareas = await tareaBL.ObtenerPorIdProyecto(id);
            return mapper.Map<IEnumerable<TareaSalida>>(tareas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TareaGuardar tareaGuardar)
        {
            try
            {
                Tarea tarea = mapper.Map<Tarea>(tareaGuardar);
                await tareaBL.CrearAsync(tarea);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TareaModificar tareaModificar)
        {

            if (tareaModificar.Id == id)
            {
                Tarea tarea = mapper.Map<Tarea>(tareaModificar);
                await tareaBL.ModificarAsync(tarea);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(TareaCambiarEstado tareaCambiarEstado)
        {
            try
            {
                Tarea tarea = mapper.Map<Tarea>(tareaCambiarEstado);
                await tareaBL.CambiarEstadoAsync(tarea);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await tareaBL.EliminarAsync(new Tarea { Id = id });
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<TareaSalida>> Buscar([FromBody] object pTarea)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strTarea = JsonSerializer.Serialize(pTarea);
            Tarea tarea = JsonSerializer.Deserialize<Tarea>(strTarea, option);
            List<Tarea> tareas = await tareaBL.BuscarIncluirProyectoAsync(tarea);
            return mapper.Map<List<TareaSalida>>(tareas);
        }        
    }
}
