using Microsoft.AspNetCore.Identity;


namespace WithControllersAuthJWT
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
