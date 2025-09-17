using System;
using System.Linq;
using System.Security.Claims;

namespace api.Extensions
{
    public static class ClaimsExtensions
    {
public static string? GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims
                .FirstOrDefault(x => 
                    x.Type == "given_name" || 
                    x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"
                )?.Value;
        }
    }
}
