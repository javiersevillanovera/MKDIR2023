using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        //public string TokenType { get { return string.IsNullOrEmpty(AccessToken) ? string.Empty : "Bearer"; } }
        //public bool IsLockedOut { get; set; } = false;
        //public bool IsNotAllowed { get; set; } = false;
        //public bool RequiresTwoFactor { get; set; } = false;


        //public BusinessUserDTO BusinessUser { get; set; }
    }
}
