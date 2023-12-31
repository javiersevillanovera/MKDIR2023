﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public class BusinessUserDTO
    {
        public int BusinessUserId { get; set; }

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string SureName { get; set; } = null!;

        public bool IsOperator { get; set; }

        public bool IsActive { get; set; }
    }
}
