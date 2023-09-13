using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Infrastructure
{
    public class BusinessModuleRepository : BaseRepository<BusinessModule>, IBusinessModuleRepository
    {
        public BusinessModuleRepository(AppDBContext context) : base(context)
        {
        }
    }
}
