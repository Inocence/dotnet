using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUerName(this ClaimsPrincipal user) {
            return user.Claims.SingleOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
        }
    }
}