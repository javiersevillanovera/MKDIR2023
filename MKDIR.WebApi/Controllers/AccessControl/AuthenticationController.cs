using Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MKDIR.Domain;
using MKDIR.WebApi.Controllers;

namespace MKDIR.WebApi
{
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthenticationService _service;
        private readonly IJwtAuthManagerService _jwtManager;

        public AuthenticationController(IAuthenticationService service, IJwtAuthManagerService jwtManager)
        {
            _service = service;
            _jwtManager = jwtManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> HandlePostAsync(AuthenticationRequest authRequest)
        {
            var signInResult = await _service.SignInAsync(authRequest.Username, authRequest.Password);

            //var response = new AuthenticationResponse();

            if (signInResult.Succeeded)
            {
                //var empresasUsuario = _usuarioEmpresaService.GetCompaniesByUser(signInResult.User.IdUsuario);

                //var empresas = new List<Empresa>();
                //foreach (var empresaUsuario in empresasUsuario)
                //{
                //    var empresaId = empresaUsuario.IdEmpresa;
                //    var empresaResult = _empresaService.Get(x => x.Establecimientos).Where(x => x.IdEmpresa == empresaId).FirstOrDefault();
                //    if (empresaResult != null)
                //        empresas.Add(empresaResult);
                //}
                //signInResult.User.Empresas = empresas;

                //var jwt = await _jwtManager.GetTokenAsync(signInResult.BusinessUser);
                //response.AccessToken = jwt;
                //response.User = signInResult.User
                //                                .ConvertToDto(isFromLogin: true)
                //                                .ToView<UserDto>(x => x.Id,
                //                                                 x => x.Name,
                //                                                 x => x.Email,
                //                                                 x => x.IsActive,
                //                                                 x => x.ExpiredAt,
                //                                                 x => x.ExpiredMessage,
                //                                                 x => x.Empresas
                //                                                 );

                return Response(await _jwtManager.GetTokenAsync(signInResult.BusinessUser));
            }
            else            
                return ResponseNotOk(new ErrorResult(Constants.INVALID_USER));            
        }

        [HttpGet("renovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Renovar()
        {
            var userInfo = new BusinessUser()
            {
                FirstName = HttpContext.User.Identity!.Name!,
                Email = HttpContext.User.Identity!.Name
            };

            var res = await _jwtManager.GetTokenAsync(userInfo);
            return Response(data: res);
        }
    }
}
