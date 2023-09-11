using MKDIR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Infrastructure
{
    public class BusinessUserRepository : BaseRepository<BusinessUser>, IBusinessUserRepository
    {
        public BusinessUserRepository(AppDBContext context) : base(context)
        {
        }
    }
}
