

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MKDIR.WebApp.Helpers;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using MKDIR.WebApp.Interfaces.Services;
using MKDIR.Domain;
using MKDIR.WebApp.Service;

namespace MKDIR.WebApp.Authentication
{
    public class ProveedorAutenticacionJWT : AuthenticationStateProvider, ILoginServiceJwT
    {
        private readonly IJSRuntime js;
        private readonly ILoginService loginService;
        private readonly HttpClient httpClient;

        public ProveedorAutenticacionJWT(IJSRuntime js, ILoginService loginService, HttpClient httpClient)
        {
            this.js = js;
            this.loginService = loginService;
            this.httpClient = httpClient;
        }

        public static readonly string TOKENKEY = "TOKENKEY";
        public static readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
        private AuthenticationState Anonimo =>
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));


        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.ObtenerDeLocalStorage(TOKENKEY);

            if (token is null)
            {
                return Anonimo;
            }

            var tiempoExpiracionObject = await js.ObtenerDeLocalStorage(EXPIRATIONTOKENKEY);
            DateTime tiempoExpiracion;

            if (tiempoExpiracionObject is null)
            {
                await Limpiar();
                return Anonimo;
            }

            if (DateTime.TryParse(tiempoExpiracionObject.ToString(), out tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Limpiar();
                    return Anonimo;
                }

                if (DebeRenovarToken(tiempoExpiracion))
                {
                    token = await RenovarToken(token.ToString()!);
                }
            }

            return ConstruirAuthenticationState(token.ToString()!);
        }

        private bool TokenExpirado(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion <= DateTime.UtcNow;
        }

        private bool DebeRenovarToken(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(5);
        }

        private async Task<string> RenovarToken(string token)
        {
            Console.WriteLine("Renovando el token...");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);

            var respuesta = await loginService.Renovar();

            if (respuesta.Success)
            {
                await js.GuardarEnLocalStorage(TOKENKEY, respuesta.Response!.AccessToken);
                await js.GuardarEnLocalStorage(EXPIRATIONTOKENKEY, respuesta.Response.Expiration.ToString());
                return respuesta.Response!.AccessToken;
            }

            return token;

            
        }

        public async Task ManejarRenovacionToken()
        {
            var tiempoExpiracionObject = await js.ObtenerDeLocalStorage(EXPIRATIONTOKENKEY);
            DateTime tiempoExpiracion;

            if (DateTime.TryParse(tiempoExpiracionObject.ToString(), out tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Logout();
                }

                if (DebeRenovarToken(tiempoExpiracion))
                {
                    var token = await js.ObtenerDeLocalStorage(TOKENKEY);
                    var nuevoToken = await RenovarToken(token.ToString()!);
                    var authState = ConstruirAuthenticationState(nuevoToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }

        private AuthenticationState ConstruirAuthenticationState(string token)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", token);

                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var tokenDeserializado = jwtSecurityTokenHandler.ReadJwtToken(token);
                var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(tokenDeserializado.Claims, "jwt"));

                return new AuthenticationState(claimPrincipal);

            }
            catch (Exception)
            {

                return Anonimo;
            }
                    
        }

        public async Task Login(AuthenticationResponse auth)
        {
            await js.GuardarEnLocalStorage(TOKENKEY, auth.AccessToken);
            await js.GuardarEnLocalStorage(EXPIRATIONTOKENKEY, auth.Expiration.ToString());
            var authState = ConstruirAuthenticationState(auth.AccessToken);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await Limpiar();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }

        private async Task Limpiar()
        {
            await js.RemoverDelLocalStorage(TOKENKEY);
            await js.RemoverDelLocalStorage(EXPIRATIONTOKENKEY);
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
