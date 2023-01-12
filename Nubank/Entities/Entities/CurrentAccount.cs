using Entities.Enums;
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
        public int? Account { get; set; }

        [Column("BALANCE")]
        public long Balance { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        [Column("CREATE_ACCOUNT_DATE")]
        public DateTime CreateAccountDate { get; set; } = DateTime.Now;

        public TipoAccount? Tipo { get; set; }


        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string? UserId { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
