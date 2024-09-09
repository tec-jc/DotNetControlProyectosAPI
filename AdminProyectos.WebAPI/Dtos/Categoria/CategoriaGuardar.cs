using System.ComponentModel.DataAnnotations;

namespace AdminProyectos.WebAPI.Dtos.Categoria
{
    public class CategoriaGuardar
    {
        [Required(ErrorMessage = "El nombre de la categoría es requerido")]
        public string Nombre { get; set; }
    }
}
