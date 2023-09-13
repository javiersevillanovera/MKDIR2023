using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public interface IAuthenticationService
    {
        Task<AuthenticationSignInResult> SignInAsync(string username, string password, bool lockoutOnFailure = false);
    }
}
