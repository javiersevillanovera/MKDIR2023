using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public class BusinessModuleDTO
    {
        public int BusinessModuleId { get; set; }

        public int Sequence { get; set; }

        public string Name { get; set; } = null!;

        public string Icon { get; set; } = null!;

        public bool IsOperator { get; set; }

        public bool IsActive { get; set; }
    }
}
