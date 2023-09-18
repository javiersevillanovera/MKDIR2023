

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MKDIR.WebApp.Helpers;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using MKDIR.WebApp.Interfaces.Services;

namespace MKDIR.WebApp.Authentication
{
    public class ProveedorAutenticacionJWT : AuthenticationStateProvider, ILoginServiceJwT
    {
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;

        public ProveedorAutenticacionJWT(IJSRuntime js, HttpClient httpClient)
        {
            this.js = js;
            this.httpClient = httpClient;
        }

        public static readonly string TOKENKEY = "TOKENKEY";
        private AuthenticationState Anonimo =>
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));


        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.ObtenerDeLocalStorage(TOKENKEY);

            if (token is null)
            {
                return Anonimo;
            }

            return ConstruirAuthenticationState(token.ToString()!);
        }

        private AuthenticationState ConstruirAuthenticationState(string token)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", token);

                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var tokenDeserializado = jwtSecurityTokenHandler.ReadJwtToken(token);

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(tokenDeserializado.Claims, "jwt")));

            }
            catch (Exception)
            {

                return Anonimo;
            }
                    
        }

        public async Task Login(string token)
        {
            await js.GuardarEnLocalStorage(TOKENKEY, token);
            var authState = ConstruirAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await js.RemoverDelLocalStorage(TOKENKEY);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }
    }
}
