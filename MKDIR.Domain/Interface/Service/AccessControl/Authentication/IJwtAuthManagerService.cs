using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public interface IJwtAuthManagerService
    {
        Task<AuthenticationResponse> GetTokenAsync(BusinessUser user);
    }
}
