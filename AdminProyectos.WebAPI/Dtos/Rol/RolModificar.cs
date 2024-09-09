using System.ComponentModel.DataAnnotations;

namespace AdminProyectos.WebAPI.Dtos.Rol
{
    public class RolModificar
    {
        [Required(ErrorMessage = "El id del rol es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es requerido")]
        public string Nombre { get; set; }
    }
}
