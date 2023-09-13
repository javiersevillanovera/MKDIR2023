using Microsoft.EntityFrameworkCore;
using MKDIR.Domain;
using Common;
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
