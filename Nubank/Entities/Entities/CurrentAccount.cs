using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("CURRENT_ACCOUNT")]
    public class CurrentAccount
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("ACCOUNT")]
        public string Account { get; set; }

        [Column("BALANCE")]
        public long Balance { get; set; }


        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string? UserId { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
