using System.ComponentModel.DataAnnotations;

namespace AdminProyectos.WebAPI.Dtos.Proyecto
{
    public class ProyectoGuardar
    {
        [Required(ErrorMessage = "El Id de la categoría es requerido")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El título es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public string FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de finalización es requerida")]
        public string FechaFin { get; set; }
    }
}
