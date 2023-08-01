using System.ComponentModel.DataAnnotations;

namespace WebAPIs.Models
{
    public class DebitCardViewModel
    {
        public int Id { get; set; }

        [Required]
        public string NameDebitCard { get; set; }

        [Required]
        public string NumberDebitCard { get; set; }

        
        public string SecurityNumber { get; set; }

        public DateTime CreationDate { get; set; }

       
        public DateTime AlteredDate { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}
