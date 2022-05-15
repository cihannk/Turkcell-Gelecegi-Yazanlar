using System.Security.Claims;
using System.Security.Principal;

namespace MarketApp.API.Extensions
{
    public static class IdentityExtenisons
    {
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
