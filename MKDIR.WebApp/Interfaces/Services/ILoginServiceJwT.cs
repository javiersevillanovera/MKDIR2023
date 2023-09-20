using MKDIR.Domain;

namespace MKDIR.WebApp.Interfaces.Services
{
    public interface ILoginServiceJwT
    {
        Task Login(AuthenticationResponse authResponse);
        Task Logout();
        Task ManejarRenovacionToken();
    }
}
