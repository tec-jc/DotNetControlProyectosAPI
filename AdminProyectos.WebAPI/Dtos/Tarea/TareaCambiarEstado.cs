using System.ComponentModel.DataAnnotations;

namespace AdminProyectos.WebAPI.Dtos.Tarea
{
    public class TareaCambiarEstado
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Estado { get; set; }
    }
}
