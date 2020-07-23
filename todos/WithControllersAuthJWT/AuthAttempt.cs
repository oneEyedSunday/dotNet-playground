using System.ComponentModel.DataAnnotations;

namespace WithControllersAuthJWT
{
    public class AuthAttempt
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
