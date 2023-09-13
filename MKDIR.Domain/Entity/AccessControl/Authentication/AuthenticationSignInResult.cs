using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public class AuthenticationSignInResult
    {
        private BusinessUser _businessUser;
        public BusinessUser BusinessUser
        {
            get
            {
                if (_businessUser != null)
                {
                    _businessUser.BusinessUserPass = null;
                }
                return _businessUser;
            }
            set => _businessUser = value;
        }
        public bool Succeeded { get; set; } = false;
    }
}
