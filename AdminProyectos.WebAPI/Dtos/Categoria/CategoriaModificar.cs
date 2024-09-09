using System.ComponentModel.DataAnnotations;

namespace AdminProyectos.WebAPI.Dtos.Categoria
{
    public class CategoriaModificar
    {
        [Required(ErrorMessage = "El id de la categoría es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es requerido")]
        public string Nombre { get; set; }
    }
}
