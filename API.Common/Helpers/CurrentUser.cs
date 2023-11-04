using Core.Constants;
using System.Security.Claims;

namespace API.Common.Helpers
{
    public static class CurrentUser
    {
        public static JWTCurrentUser GetCurrentUser(ClaimsPrincipal? claimsPrincipal)
        {
            if (claimsPrincipal == null) return null;
            return new JWTCurrentUser
            {
                Name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value,
                UserId = int.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                RoleId = int.Parse(claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value)
            };
        }
    }
}

public class JWTCurrentUser
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public int RoleId { get; set; }

    public bool IsAdmin { 
        get {
            if(RoleId == (int)RoleEnum.Admin)
                return true;

            return false;
        } 
    }

    public bool IsCustomer
    {
        get
        {
            if (RoleId == (int)RoleEnum.Customer)
                return true;

            return false;
        }
    }
}
