using Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("TB_DEBIT_CARD")]
    public class DebitCard : Notifies
    {
        [Column("MSN_ID")]
        public int Id { get; set; }
        
        [Column("NAME_CARD")]
        [MaxLength(35)]
        public string? NameDebitCard { get; set; }

        [Column("Number_CARD")]
        public string? NumberDebitCard { get; set; }

        [Column("SECURITY_NUMBER_CARD")]
        public string? SecurityNumber { get; set; }

        [Column("CREATION_DATE_CARD")]
        public DateTime CreationDate { get; set; }

        [Column("ALTERED_DATE_CARD")]
        public DateTime AlteredDate { get; set; }

        [Column("MSN_ATIVO")]
        public bool Ativo { get; set; }

        [Column("TIPO")]
        public TipoCard? Tipo { get; set; }


        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string? UserId { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual CurrentAccount? CurrentAccount { get; set; }

    }
}
/*        [Column("MSN_IMAGE")]
        public ImageFileMachine Images { get; set; }

        [Column("IMAGE")]
        public FileOptions Image { get; set; }*/