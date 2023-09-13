using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public class BaseFilter
    {
        public long? Id { get; set; }

        [DefaultValue(1)]
        public int page { get; set; } = 1;

        [DefaultValue(10)]
        public int pageSize { get; set; } = 10;

        [DefaultValue(true)]
        public bool isActive { get; set; } = true;

        public string Search { get; set; }
        public string[] SearchFilter { get; set; }
        public string key { get; set; }
        public string Code { get; set; }
        public long Environment { get; set; }
    }
}
