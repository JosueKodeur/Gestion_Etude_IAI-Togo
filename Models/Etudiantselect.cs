namespace GestEtude.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Etudiantselect")]
    public partial class Etudiantselect
    {
        [Key]
        public int NoEtudiant { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Tel { get; set; }

        public string Mail { get; set; }

        [StringLength(4000)]
        public string AnneeEntre { get; set; }
    }
}
