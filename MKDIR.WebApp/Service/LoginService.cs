using MKDIR.Domain;
using MKDIR.WebApp.Entity;
using MKDIR.WebApp.Interfaces.Repository;
using MKDIR.WebApp.Interfaces.Services;
using MKDIR.WebApp.Repository;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace MKDIR.WebApp.Service
{
    public class LoginService : ILoginService
    {
        private readonly IWebApiRepository _webApiRepository;
        public LoginService(IWebApiRepository webApiRepository)
        {
            this._webApiRepository = webApiRepository;
        }

        private JsonSerializerOptions OpcionesPorDefectoJSON => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<ServiceResponse<AuthenticationResponse>> Login(AuthenticationRequest userinfo)
        {
            try
            {
                AuthenticationResponse? data = null;
                List<ErrorResult>? error = null;

                var respuesta = await this._webApiRepository.Post<AuthenticationRequest, InternalResponse>("Authentication", userinfo);

                if (respuesta.success)
                    data = JsonSerializer.Deserialize<AuthenticationResponse>(respuesta.data.ToString(), OpcionesPorDefectoJSON);
                else
                    error = JsonSerializer.Deserialize<List<ErrorResult>>(respuesta.errors.ToString(), OpcionesPorDefectoJSON);

                return new ServiceResponse<AuthenticationResponse>(respuesta.success, error.FirstOrDefault().Message, data);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<AuthenticationResponse>(false, ex.Message, null);
            }
            
        }
    }
}
