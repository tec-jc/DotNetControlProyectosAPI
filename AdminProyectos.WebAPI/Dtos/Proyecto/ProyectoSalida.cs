
using AdminProyectos.WebAPI.Dtos.Categoria;

namespace AdminProyectos.WebAPI.Dtos.Proyecto
{
    public class ProyectoSalida
    {
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public DateOnly FechaInicio { get; set; }

        public DateOnly FechaFin { get; set; }

        public CategoriaSalida Categoria { get; set; }
    }
}
