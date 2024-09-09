using AdminProyectos.AccesoADatos;
using AdminProyectos.EntidadesDeNegocio;
using AdminProyectos.WebAPI.Dtos.Categoria;
using AdminProyectos.WebAPI.Dtos.Proyecto;
using AdminProyectos.WebAPI.Dtos.Rol;
using AdminProyectos.WebAPI.Dtos.Tarea;
using AdminProyectos.WebAPI.Dtos.Usuario;
using AutoMapper;

namespace AdminProyectos.WebAPI.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RolGuardar, Rol>();
            CreateMap<RolModificar, Rol>();
            CreateMap<Rol, RolSalida>();

            CreateMap<UsuarioGuardar, Usuario>();
            CreateMap<UsuarioModificar, Usuario>();
            CreateMap<UsuarioLogin, Usuario>();
            CreateMap<Usuario, UsuarioSalida>();

            CreateMap<CategoriaGuardar, Categoria>();
            CreateMap<CategoriaModificar, Categoria>();
            CreateMap<Categoria, CategoriaSalida>();

            CreateMap<ProyectoGuardar, Proyecto>()
                .ForMember(p => p.FechaInicio, opt => opt.Ignore())
                .ForMember(p => p.FechaFin, opt => opt.Ignore());
            CreateMap<ProyectoModificar, Proyecto>()
                .ForMember(p => p.FechaInicio, opt => opt.Ignore())
                .ForMember(p => p.FechaFin, opt => opt.Ignore());
            CreateMap<Proyecto, ProyectoSalida>();

            CreateMap<TareaGuardar, Tarea>();
            CreateMap<TareaModificar, Tarea>();
            CreateMap<Tarea, TareaSalida>()
            .ForMember(t => t.Estado, opt => opt.MapFrom(src => ((Estado_Tarea)src.Estado).ToString()));
            CreateMap<TareaCambiarEstado, Tarea>()
            .ForMember(t => t.Estado, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(Estado_Tarea), src.Estado)));
        }
    }
}
