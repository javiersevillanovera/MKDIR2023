using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDIR.Domain
{
    public class GeneralConfiguration
    {
        public string JwtSecret { get; set; }
        public int JwtTokenExpiration { get; set; }
        public string JwtDomain { get; set; }
        public string JwtAudience { get; set; }

        public string CaptchaClientKey { get; set; }
        public string CaptchaServerKey { get; set; }
        public string CaptchaUrlValidation { get; set; }

        public string SendGridUser { get; set; }
        public string SendGridUserEmail { get; set; }
        public string SendGridKey { get; set; }

        public SendGridTemplate SendGridTemplate { get; set; }
    }

    public class SendGridTemplate
    {
        public string RecoveryPassword { get; set; }
    }
}
