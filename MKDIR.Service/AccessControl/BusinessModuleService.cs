using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Service
{
    public class BusinessModuleService : BaseService<BusinessModule>, IBusinessModuleService
    {
        public BusinessModuleService(IBusinessModuleRepository repository) : base(repository)
        {
        }
    }
}
