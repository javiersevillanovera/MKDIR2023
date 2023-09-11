using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Common
{
    public static class ClaimsExtensions
    {
        public static long UserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.Sid);
            long.TryParse(claim?.Value ?? null, out long userId);
            return userId;
        }
    }

    //public static class CustomClaimTypes
    //{
    //    public const string ConsultantId = "consultantid";
    //}
}
