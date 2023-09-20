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

        public async Task<ServiceResponse<AuthenticationResponse>> Login(AuthenticationRequest userinfo)
        {
            try
            {
                return await this._webApiRepository.Post<AuthenticationRequest, AuthenticationResponse>("Authentication", userinfo);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<AuthenticationResponse>(false, ex.Message, null);
            }
            
        }

        public async Task<ServiceResponse<AuthenticationResponse>> Renovar()
        {
            try
            {
                var res = await this._webApiRepository.Get<AuthenticationResponse>("Authentication/renovarToken"); ;
                return res;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<AuthenticationResponse>(false, ex.Message, null);
            }
        }
    }
}
