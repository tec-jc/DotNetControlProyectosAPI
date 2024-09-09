using AdminProyectos.EntidadesDeNegocio;

namespace AdminProyectos.WebAPI.Auth
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(Usuario usuario);
    }
}
