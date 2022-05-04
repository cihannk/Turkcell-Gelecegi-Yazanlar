using System.Security.Claims;
using System.Security.Principal;

namespace MarketApp.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetEmail(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }
        public static int GetId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.Claims?.FirstOrDefault(x => x.Type == "id");
            if (claim == null)
            {
                return 0;
            }
            return (int)Convert.ToInt64(claim.Value);
        }
    }
}
