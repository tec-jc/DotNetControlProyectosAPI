using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.EntidadesDeNegocio
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public DateOnly FechaInicio { get; set; }

        public DateOnly FechaFin { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public Categoria? Categoria { get; set; }

        public List<Tarea>? Tareas { get; set; }
    }
}
