using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPIs.Models
{
    public class DebitCardViewModel
    {
        public int Id { get; set; }

        public string NameDebitCard { get; set; }

        public string NumberDebitCard { get; set; }

        public string SecurityNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime AlteredDate { get; set; }

        public string UserId { get; set; }

    }
}
