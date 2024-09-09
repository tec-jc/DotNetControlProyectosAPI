
using AdminProyectos.WebAPI.Dtos.Proyecto;

namespace AdminProyectos.WebAPI.Dtos.Tarea
{
    public class TareaSalida
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Duracion { get; set; }

        public string Estado { get; set; }

        public ProyectoSalida Proyecto { get; set; }
    }
}
