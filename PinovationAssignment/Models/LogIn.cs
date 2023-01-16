using Microsoft.Build.Framework;

namespace PinovationAssignment.Models
{
    public class LogIn
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
