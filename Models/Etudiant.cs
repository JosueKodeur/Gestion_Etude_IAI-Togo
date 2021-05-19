namespace GestEtude.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Etudiant")]
    public partial class Etudiant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Etudiant()
        {
            Note = new HashSet<Note>();
        }

        [Key]
        public int NoEtudiant { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Tel { get; set; }

        public string Mail { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AnneeEntre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Note { get; set; }
    }
}
