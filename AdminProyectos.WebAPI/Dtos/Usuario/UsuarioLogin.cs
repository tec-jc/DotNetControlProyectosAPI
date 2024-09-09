using System.ComponentModel.DataAnnotations;

namespace AdminProyectos.WebAPI.Dtos.Usuario
{
    public class UsuarioLogin
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}
