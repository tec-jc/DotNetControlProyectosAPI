using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.EntidadesDeNegocio
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Proyecto")]
        public int IdProyecto { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public string? Duracion { get; set; }

        public byte Estado { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public Proyecto? Proyecto { get; set; }
    }

    public enum Estado_Tarea
    {
        APROBADA = 1,
        PROCESO = 2,
        REVISION = 3,
        FINALIZADA = 4
    }
}
