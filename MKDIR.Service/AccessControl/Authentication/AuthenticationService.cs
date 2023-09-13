using Common;
using Microsoft.EntityFrameworkCore;
using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IBusinessUserService _service;

        public AuthenticationService(IBusinessUserService service)
        {
            _service = service;
        }


        public async Task<AuthenticationSignInResult> SignInAsync(string username, string password, bool lockoutOnFailure = false)
        {
            var result = new AuthenticationSignInResult();

            var BusinessUser = await _service.Get()
                                .Where(x => x.Email == username)
                                .FirstOrDefaultAsync();

            if (BusinessUser == null)
                return result;

            if (!BusinessUser.IsActive)
                return result;

            var pass = password.Encriptar();
            var checkPassword = await _service.Get().Where(x => x.Email == username && x.BusinessUserPass.Pass == pass).FirstOrDefaultAsync();

            if (checkPassword == null)
                return result;
            else
            {
                result.BusinessUser = checkPassword;
                result.Succeeded = true;
                return result;
            }
        }

    }
}
