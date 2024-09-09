using AdminProyectos.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProyectos.AccesoADatos
{
    public class ContextoDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = JC-PC;
                                            Initial Catalog = AdminProyectosAPI;
                                            Integrated Security = True; 
                                            encrypt = false; 
                                            trustServerCertificate = True");
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
