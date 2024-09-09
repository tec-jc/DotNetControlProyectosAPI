using System.ComponentModel.DataAnnotations;

namespace AdminProyectos.WebAPI.Dtos.Tarea
{
    public class TareaGuardar
    {
        [Required(ErrorMessage = "El id de proyecto es requerido")]
        public int IdProyecto { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La duración es requerida")]
        public string Duracion { get; set; }
    }
}
