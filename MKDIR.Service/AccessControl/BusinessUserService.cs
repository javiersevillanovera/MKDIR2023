using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Service
{
    public class BusinessUserService : BaseService<BusinessUser>, IBusinessUserService
    {
        public BusinessUserService(IBusinessUserRepository repository) : base(repository)
        {
        }
    }
}
