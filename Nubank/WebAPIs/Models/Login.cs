using System.ComponentModel.DataAnnotations;

namespace WebAPIs.Models
{
    public class Login
    {
        [Required()]
        [EmailAddress()]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }

        [Required]
        public string CPF { get; set; }

    }
}
