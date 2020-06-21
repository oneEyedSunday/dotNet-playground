using System.ComponentModel.DataAnnotations;

namespace BasicWithAuth
{
    public class Auth
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}

