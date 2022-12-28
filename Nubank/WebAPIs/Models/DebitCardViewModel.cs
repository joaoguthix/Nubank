using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Amqp.Framing;

namespace WebAPIs.Models
{
    public class DebitCardViewModel
    {
        public int Id { get; set; }

        [Required]
        public string NameDebitCard { get; set; }

        [Required]
        public string NumberDebitCard { get; set; }

        [Required]
        public string SecurityNumber { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime AlteredDate { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}
