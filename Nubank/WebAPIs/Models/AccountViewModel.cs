using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIs.Models
{
    public class AccountViewModel
    {
        
        public int Id { get; set; }

        [Required]
        public string? Account { get; set; }

        [Required]
        public long Balance { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}
