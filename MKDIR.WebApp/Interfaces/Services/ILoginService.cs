using MKDIR.Domain;
using MKDIR.WebApp.Interfaces.Repository;

namespace MKDIR.WebApp.Interfaces.Services
{
    public interface ILoginService
    {
        Task<ServiceResponse<AuthenticationResponse>> Login(AuthenticationRequest userinfo);
    }
}
