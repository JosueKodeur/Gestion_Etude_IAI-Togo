namespace GestEtude.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Note")]
    public partial class Note
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoEtudiant { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NoCours { get; set; }

        public double NoteControle { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Etudiant Etudiant { get; set; }
    }
}
