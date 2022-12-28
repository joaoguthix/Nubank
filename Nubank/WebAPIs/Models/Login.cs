using System.ComponentModel.DataAnnotations;

namespace WebAPIs.Models
{
    public class Login
    {
        [EmailAddress()]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string CPF { get; set; }

    }
}
