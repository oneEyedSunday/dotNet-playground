using Microsoft.AspNetCore.Identity;

namespace BasicWithAuth
{
    public class BasicWithAuthUser: IdentityUser
    {
        public bool isAdmin { get; set; }
    }
}
