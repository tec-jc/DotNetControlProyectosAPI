using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.EntidadesDeNegocio
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        public string? Nombre { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        [NotMapped]
        public List<Usuario>? Usuarios { get; set; }
    }
}
